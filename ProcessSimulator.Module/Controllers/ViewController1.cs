using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using ProcessSimulator.Module.BusinessMethods;
using ProcessSimulator.Module.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProcessSimulator.Module.Controllers
{
    public partial class ViewController1 : ViewController
    {
        private SimpleAction myTestAction;

        public ViewController1()
        {
            TargetViewType = ViewType.Any;
            TargetViewNesting = Nesting.Any; // Para vistas embebidas también
            TargetObjectType = null; // null significa que se aplica a cualquier tipo de objeto


            myTestAction = new SimpleAction(this, "EjecutarPrueba", PredefinedCategory.View);
            myTestAction.Caption = "Ejecutar Prueba";
            myTestAction.ImageName = "Action_Debug_Start";
            myTestAction.PaintStyle = ActionItemPaintStyle.CaptionAndImage;
            myTestAction.Execute += MyTestAction_Execute;

            Actions.Add(myTestAction); // Asegúrate de agregar la acción
        }

        private void MyTestAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            try
            {
                Session Session = ((XPObjectSpace)ObjectSpace).Session;

                PortfolioClosingMaster portfolioClosingMaster = (from pcm in new XPQuery<PortfolioClosingMaster>(Session)
                                                                           //where pcm.Oid == portfolioClosingMasterView.Oid
                                                                 select pcm).FirstOrDefault();

                PortfolioClosing portfolioClosing = ClosePortfolio(Session, portfolioClosingMaster);

                InventoryCompensationClass.CompensatePortfolioInventory(ObjectSpace, portfolioClosingMaster, portfolioClosing ,1000);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static PortfolioClosing ClosePortfolio(Session unitOfWork, PortfolioClosingMaster portfolioClosingMaster)
        {
            try
            {
                portfolioClosingMaster.IsOpen = true;
                portfolioClosingMaster.IsReOpen = false;
                DateTime newPortfolioClosingDate = UtilitiesFunctions.RelativeDate(portfolioClosingMaster.LastClosingDate, 1, 0);
                PortfolioClosing portfolioClosing = new PortfolioClosing(unitOfWork);
                portfolioClosing.ClosingDate = newPortfolioClosingDate;
                portfolioClosing.IsActive = true;
                if (portfolioClosingMaster.PortfolioOpenings.Count > 0)
                {
                    List<PortfolioOpening> lportfolioOpening = new List<PortfolioOpening>();
                    foreach (PortfolioOpening portfolioOpening in portfolioClosingMaster.PortfolioOpenings)
                    {
                        lportfolioOpening.Add(portfolioOpening);
                    }

                    PortfolioOpening[] orderedPortfolioOpenings = (from po in lportfolioOpening
                                                                         where po.IsActive == true
                                                                         orderby po.OpenedClosingDate ascending
                                                                         select po).ToArray();
                    if (orderedPortfolioOpenings.Length > 0)
                    {
                        orderedPortfolioOpenings[0].IsActive = false;
                        portfolioClosingMaster.IsOpen = false;
                        portfolioClosingMaster.IsReOpen = true;
                    }
                }
                portfolioClosingMaster.LastClosingDate = newPortfolioClosingDate;
                //Log.WriteLog($"(ClosePortfolio) Fecha de Cierre de Portafolio {portfolioClosingMaster.Portfolio.Name}: {newPortfolioClosingDate}");
                portfolioClosingMaster.PortfolioClosings.Add(portfolioClosing);
                portfolioClosing.Save();
                portfolioClosingMaster.Save();
                return portfolioClosing;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
