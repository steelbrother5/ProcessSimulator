using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcessSimulator.Module.Models;

namespace ProcessSimulator.Module.BusinessMethods
{
    public class InventoryCompensationClass
    {

        public static void CompensatePortfolioInventory(IObjectSpace ios, PortfolioClosingMaster portfolioClosingMaster, PortfolioClosing portfolioClosing, int blockSize)
        {
            try
            {
                DateTime lastClosingDate = portfolioClosingMaster.LastClosingDate;
                Portfolio portfolio = portfolioClosingMaster.Portfolio;
                int NumberOfObjectsCreated = 0;

                if (portfolioClosing != null)
                {
                    List<PeriodClosingCommonClassesC.RecordInventory> CIL_RecordInventory = new List<PeriodClosingCommonClassesC.RecordInventory>();

                    List<PortfolioInventoryItem> lPortfolioInventoryItem =
                        (from pii in new XPQuery<PortfolioInventoryItem>(((XPObjectSpace)ios).Session)
                         where (pii.Portfolio != null
                                //&& pii.Portfolio.Name == portfolioClosingMaster.Portfolio.Name
                                && pii.IsInPortfolio
                                && pii.UnitsBalance > 0.0m
                                //&& pii.ConstitutionDate.Date <= lastClosingDate.Date
                                && !pii.IsPortfolioNoveltyGuaranteeConstitution
                                && !pii.IsPortfolioNoveltyGuaranteeRelease
                                && !pii.IsPortfolioNoveltyGuaranteeYield)
                         orderby pii.ID ascending
                         select pii).ToList();

                    int NumberOFlPortfolioInventoryItem = lPortfolioInventoryItem.Select(portfolioInventoryItem => portfolioInventoryItem.AssetSpecies).Distinct().Count();
                    foreach (var item in lPortfolioInventoryItem.Select(portfolioInventoryItem => portfolioInventoryItem.AssetSpecies).Distinct().ToList())
                    {
                        NumberOFlPortfolioInventoryItem--;

                        List<PortfolioInventoryItem> lPortfolioInventoryItemFiltered = lPortfolioInventoryItem.Where(elem => elem.AssetSpecies == item).ToList();

                        CIL_RecordInventory.Clear();
                        int i = 0;
                        foreach (PortfolioInventoryItem portfolioInventoryItem in lPortfolioInventoryItemFiltered)
                        {

                            PeriodClosingCommonClassesC.RecordInventory recordInventory =
                                new PeriodClosingCommonClassesC.RecordInventory(i,
                                                                               portfolioInventoryItem.ItemSign.Multiplier,
                                                                               portfolioInventoryItem.UnitsBalance,
                                                                               (portfolioInventoryItem.ConstitutionValueInLocalCurrency /
                                                                               portfolioInventoryItem.ConstitutionUnits));

                            CIL_RecordInventory.Add(recordInventory);
                            i += 1;
                        }

                        //PeriodClosingCommonClassesC.OutRecordInventoryCompensation[] lRecordInventoryCompensation = InventoriesCompensation(CIL_RecordInventory);
                        var lRecordInventoryCompensation = InventoriesCompensation(CIL_RecordInventory);

                        PortfolioItemType portfolioItemType = (from tf in new XPQuery<PortfolioItemType>(((XPObjectSpace)ios).Session)
                                                                     where tf.Name == "Inventory Compensation"
                                                                     select tf).FirstOrDefault();
                        if (portfolioItemType == null)
                        {
                            throw new Exception("-%-%-%-%-%-%-> (ExternalInvestmentUnitClosingController) Falta portfolioItemType: Inventory Compensation");
                        }


                        foreach (PeriodClosingCommonClassesC.OutRecordInventoryCompensation recordInventoryCompensation in lRecordInventoryCompensation)
                        {
                            NumberOfObjectsCreated += 2;

                            PortfolioInventoryCompensation shortPortfolioInventoryCompensation = ios.CreateObject<PortfolioInventoryCompensation>();

                            FillPortfolioInventoryCompensation(ios,
                                                               shortPortfolioInventoryCompensation,
                                                               portfolio,
                                                               lPortfolioInventoryItemFiltered,
                                                               recordInventoryCompensation.InMovementNumber,
                                                               recordInventoryCompensation.OutMovementNumber,
                                                               portfolioClosing,
                                                               portfolioItemType,
                                                               recordInventoryCompensation.FaceValue);

                            PortfolioInventoryCompensation longPortfolioInventoryCompensation = ios.CreateObject<PortfolioInventoryCompensation>();

                            FillPortfolioInventoryCompensation(ios,
                                                               longPortfolioInventoryCompensation,
                                                               portfolio,
                                                               lPortfolioInventoryItemFiltered,
                                                               recordInventoryCompensation.OutMovementNumber,
                                                               recordInventoryCompensation.InMovementNumber,
                                                               portfolioClosing,
                                                               portfolioItemType,
                                                               recordInventoryCompensation.FaceValue);


                            if (NumberOfObjectsCreated % blockSize == 0)
                            {
                                ios.CommitChanges();
                                NumberOfObjectsCreated = 0;
                            }

                        }

                        if (NumberOFlPortfolioInventoryItem == 0) ios.CommitChanges();
                    }

                    PortfolioCompensation portfolioCompensation = ios.CreateObject<PortfolioCompensation>();
                    portfolioCompensation.IsActive = true;

                    if (portfolioClosingMaster.PortfolioCompensationUndos.Count > 0)
                    {
                        List<PortfolioCompensationUndo> lPortfolioCompensationUndo = new List<PortfolioCompensationUndo>();
                        foreach (PortfolioCompensationUndo portfolioCompensationUndo in
                            portfolioClosingMaster.PortfolioCompensationUndos)
                        {
                            lPortfolioCompensationUndo.Add(portfolioCompensationUndo);
                        }

                        PortfolioCompensationUndo[] orderedPortfolioCompensationUndos =
                            (from pcu in lPortfolioCompensationUndo
                             where pcu.IsActive == true
                             orderby pcu.UndonedCompensationDate ascending
                             select pcu).ToArray();

                        if (orderedPortfolioCompensationUndos.Length > 0)
                        {
                            orderedPortfolioCompensationUndos[0].IsActive = false;
                        }
                    }

                    portfolioClosingMaster.LastCompensationDate = lastClosingDate;
                    portfolioClosingMaster.PortfolioCompensations.Add(portfolioCompensation);
                }
                else
                    throw new Exception("Falta ExternalInvestmentUnitClosing al compensar");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void FillPortfolioInventoryCompensation
                                                  (IObjectSpace ios,
                                                   PortfolioInventoryCompensation portfolioInventoryCompensation,
                                                   Portfolio portfolio,
                                                   List<PortfolioInventoryItem> lPortfolioInventoryItem,
                                                   int inventoryItemNumber,
                                                   int oppositeInventoryItemNumber,
                                                   PortfolioClosing portfolioClosing,
                                                   PortfolioItemType portfolioItemType,
                                                   decimal compensatedFaceValue)
        {
            portfolioInventoryCompensation.Portfolio = portfolio;
            portfolioInventoryCompensation.PortfolioInventoryItem = lPortfolioInventoryItem[inventoryItemNumber];
            portfolioInventoryCompensation.PortfolioClosing = portfolioClosing;
            Guid compensationItemOid = lPortfolioInventoryItem[oppositeInventoryItemNumber].Oid;
            portfolioInventoryCompensation.OriginOid = compensationItemOid;
            string compensationItemID = ios.GetObjectByKey<PortfolioInventoryItem>(compensationItemOid).ID;
            portfolioInventoryCompensation.CompensationOriginID = compensationItemID;
            portfolioInventoryCompensation.ID = lPortfolioInventoryItem[inventoryItemNumber].ID + "-" + compensationItemID;
            portfolioInventoryCompensation.PortfolioItemType = portfolioItemType;
            portfolioInventoryCompensation.EntryDate = DateTime.Now;
            portfolioInventoryCompensation.OriginType = "Inventario";
            portfolioInventoryCompensation.UnitsCompensated = compensatedFaceValue;
            lPortfolioInventoryItem[inventoryItemNumber].UnitsCompensated += compensatedFaceValue;
            decimal constitutionValueInLocalCurrency = lPortfolioInventoryItem[oppositeInventoryItemNumber].ConstitutionValueInLocalCurrency;
            decimal constitutionUnits = lPortfolioInventoryItem[oppositeInventoryItemNumber].ConstitutionUnits;
            portfolioInventoryCompensation.CompensatedValueInLocalCurrency = constitutionValueInLocalCurrency * (compensatedFaceValue / constitutionUnits);
            lPortfolioInventoryItem[inventoryItemNumber].TotalCompensatedValueInLocalCurrency += constitutionValueInLocalCurrency * (compensatedFaceValue / constitutionUnits);
        }

        public static PeriodClosingCommonClassesC.OutRecordInventoryCompensation[] InventoriesCompensation(List<PeriodClosingCommonClassesC.RecordInventory> cIL_RecordInventories)
        {
            try
            {
                return PortfolioClosingFunctions.CompensateInventory(cIL_RecordInventories);
            }
            catch (Exception ex)
            {
                //Log.WriteLog($"Resumen de los elementos del inventario que se van a compensar.");
                //cIL_RecordInventories.ForEach(cIL_RecordInventory => Log.WriteLog($"-Elemento inventario con indice {cIL_RecordInventory.Secuence}, UnitBalance {cIL_RecordInventory.FaceValue}" +
                //                                                                 $",signo {cIL_RecordInventory.Sign}, UnitPrice {cIL_RecordInventory.UnitPrice}."));
                //Log.WriteLog(ex.Message);
                throw ex;
            }
        }
    }
}
