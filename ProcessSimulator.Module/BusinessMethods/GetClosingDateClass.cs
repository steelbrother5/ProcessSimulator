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
    public class GetClosingDateClass
    {
        public static DateTime GetClosingDate(IObjectSpace ios, ExternalInvestmentUnit externalInvestmentUnit, int daysToAdd)
            => FindClosingDate(((XPObjectSpace)ios).Session, externalInvestmentUnit, daysToAdd);

        public static DateTime GetClosingDate(Session session, ExternalInvestmentUnit externalInvestmentUnit, int daysToAdd)
            => FindClosingDate(session, externalInvestmentUnit, daysToAdd);

        /// <summary>
        /// Esta sobrecarga toma la primera Unidad Externa de Inversión.
        /// Debe restringirse su uso.
        /// </summary>
        /// <param name="ios"></param>
        /// <returns></returns>
        public static DateTime GetClosingDate(IObjectSpace ios, int daysToAdd)
        {
            ExternalInvestmentUnit externalInvestmentUnit =
                (from eiu in new XPQuery<ExternalInvestmentUnit>(((XPObjectSpace)ios).Session)
                 select eiu).FirstOrDefault();

            if (externalInvestmentUnit == null)
            {
                throw new Exception("No hay unidades externas de inversión buscando ClosingDate");
            }

            return FindClosingDate(((XPObjectSpace)ios).Session, externalInvestmentUnit, daysToAdd);
        }

        /// <summary>
        /// Esta sobrecarga toma la primera Unidad Externa de Inversión.
        /// Debe restringirse su uso.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="daysToAdd"></param>
        /// <returns></returns>
        public static DateTime GetClosingDate(Session session, int daysToAdd)
        {
            try
            {
                ExternalInvestmentUnit externalInvestmentUnit =
                    new XPQuery<ExternalInvestmentUnit>(session).Single();

                if (externalInvestmentUnit == null)
                    throw new Exception("No hay unidades externas de inversión buscando ClosingDate");

                return FindClosingDate(session, externalInvestmentUnit, daysToAdd);
            }
            catch (InvalidOperationException ex)
            {
                //Log.WriteLog(ex.Message);
                throw new NotImplementedException("No existe una única Unidad Externa de Inversión.");
            }
            catch (Exception ex)
            {
                //Log.WriteLog(ex.Message);
                throw;
            }
        }

        public static DateTime FindClosingDate(Session session, ExternalInvestmentUnit externalInvestmentUnit, int daysToAdd)
        {
            if (externalInvestmentUnit == null) throw new Exception("UEI no puede ser nula para buscar fecha de cierre");

            DateTime closingDate =
                (from eiu in new XPQuery<ExternalInvestmentUnitClosingMaster>(session)
                 where eiu.ExternalInvestmentUnit.Name == externalInvestmentUnit.Name
                 select eiu.LastClosingDate).FirstOrDefault();

            if (closingDate == null)
            {
                // Se agrego para buscar la primera fecha de cierre parametrizada en BD
                closingDate = (from cd in new XPQuery<InitialClosingDate>(session)
                               select cd.InitialCloseDate).FirstOrDefault();

                if (closingDate == null)
                {

                    throw new Exception("(GetClosingDate) No hay fecha de cierre para Unidad Externa de Inversión:" + externalInvestmentUnit.Name);
                }
            }
            else
            {
                if (closingDate == DateTime.MinValue)
                {
                    // Se agrego para buscar la primera fecha de cierre parametrizada en BD
                    closingDate = (from cd in new XPQuery<InitialClosingDate>(session)
                                   select cd.InitialCloseDate).FirstOrDefault();

                    if (closingDate == null)
                    {

                        throw new Exception("(GetClosingDate) No hay fecha de cierre para Unidad Externa de Inversión:" + externalInvestmentUnit.Name);
                    }

                }
            }
            return closingDate.AddDays(daysToAdd).Date;
        }

        public static ExternalInvestmentUnitClosingMaster GetExistClosingDateMaster(Session session, DateTime ClosingDate)
        {
            ExternalInvestmentUnitClosingMaster ExternalInvestment =
                                                      (from eiu in new XPQuery<ExternalInvestmentUnitClosingMaster>(session)
                                                       where eiu.LastClosingDate >= ClosingDate
                                                       select eiu).FirstOrDefault();
            return ExternalInvestment;
        }

        ///// <summary>
        ///// Response de un diccionario el cuál contiene las fechas de cierre relacionadas con la 
        ///// unidad externa de inversión, donde su Key y Value es el "Nombre de la UEI" y la fecha de cierre 
        ///// respectivamente, además, en caso de no haber fecha de cierre para la UEI se añade la fecha de cierre guardada
        ///// en la tabla InitialClosingDate.
        ///// NOTA: El Response es invalido sino hay información sobre las fechas de cierre. </summary>
        ///// <param name="unitOfWork">Unidad de trabajo.</param>
        ///// <returns>Response de un diccionario cuya "Key" es el nombre de la unidad externa de 
        ///// inversión y el "Value" es la fecha de cierre de dicha unidad.</returns>
        //public Response<Dictionary<string, DateTime>> GetClosingDate(UnitOfWork unitOfWork)
        //{
        //    Response<Dictionary<string, DateTime>> GetClosingDateResponse = new Response<Dictionary<string, DateTime>>();
        //    try
        //    {
        //        if (unitOfWork is null)
        //        {
        //            GetClosingDateResponse.ErrorStack.Add(string.Format(UserMessages.NullUnitOfWork, nameof(GetClosingDate)));
        //            return GetClosingDateResponse;
        //        }
        //        ExternalInvestmentUnitClosingMaster[] ExternalInvestmentUnitClosingMaster = new XPQuery<ExternalInvestmentUnitClosingMaster>(unitOfWork).ToArray();

        //        DateTime ClosingDate = new XPQuery<InitialClosingDate>(unitOfWork).Select(closingDate => closingDate.InitialCloseDate).FirstOrDefault();
        //        if (ClosingDate == DateTime.MinValue)
        //        {
        //            GetClosingDateResponse.ErrorStack.Add("No hay fecha de cierre.");
        //            return GetClosingDateResponse;
        //        }

        //        Dictionary<string, DateTime> ClosingDates =
        //            ExternalInvestmentUnitClosingMaster?.ToDictionary(externalInvestmentUnitClosingMaster => externalInvestmentUnitClosingMaster?.ExternalInvestmentUnit?.Name,
        //                                                              externalInvestmentUnitClosingMaster => externalInvestmentUnitClosingMaster.LastClosingDate.AddDays(1));
        //        ClosingDates.Add(BusinessExpressions.ClosingDate, ClosingDate.Date.AddDays(1));

        //        GetClosingDateResponse.IsValid = true;
        //        GetClosingDateResponse.Value = ClosingDates;
        //        return GetClosingDateResponse;
        //    }
        //    catch (Exception exception)
        //    {
        //        Log.WriteInnerExceptionLog(exception);
        //        GetClosingDateResponse.ErrorStack.Add(string.Format(UserMessages.ProcessUnexpectedException, nameof(GetClosingDate)));
        //        return GetClosingDateResponse;
        //    }
        //}

        ///// <summary>
        ///// Encargado de buscar las fechas de cierre por UEI y devolverlas como un diccionario
        ///// en el que las llaves corresponden con los Oid de las unidades externas de inversión
        ///// </summary>
        ///// <param name="objectSpace">Espacio de objetos con el que se van a realizar las consultas a base de datos</param>
        ///// <returns>Response conteniendo diccionario de las fechas de cierre por UEI</returns>
        //public Response<Dictionary<Guid, DateTime>> GetClosingDatesByEIU(IObjectSpace objectSpace)
        //{
        //    Response<Dictionary<Guid, DateTime>> ClosingDatesByEIUResponse = new Response<Dictionary<Guid, DateTime>>();

        //    try
        //    {
        //        if (objectSpace is null)
        //        {
        //            ClosingDatesByEIUResponse.ErrorStack.Add(string.Format(UserMessages.NullObjectSpaceLocated,
        //                                                                   nameof(GetClosingDatesByEIU)));
        //            return ClosingDatesByEIUResponse;
        //        }

        //        if (((XPObjectSpace)objectSpace)?.Session is null)
        //        {
        //            ClosingDatesByEIUResponse.ErrorStack.Add(string.Format(UserMessages.NullSession,
        //                                                                   nameof(GetClosingDatesByEIU)));
        //            return ClosingDatesByEIUResponse;
        //        }

        //        return GetClosingDatesByEIU(((XPObjectSpace)objectSpace).Session);
        //    }
        //    catch (Exception exception)
        //    {
        //        Log.WriteInnerExceptionLog(exception);
        //        ClosingDatesByEIUResponse.ErrorStack.Add(string.Format(UserMessages.ProcessUnexpectedException,
        //                                                               "consulta de fechas de cierre por UEI"));
        //        return ClosingDatesByEIUResponse;
        //    }
        //}

        ///// <summary>
        ///// Encargado de buscar las fechas de cierre por UEI y devolverlas como un diccionario
        ///// en el que las llaves corresponden con los Oid de las unidades externas de inversión
        ///// </summary>
        ///// <param name="session">Sesión con la que se van a realizar las consultas a base de datos</param>
        ///// <returns>Response conteniendo diccionario de las fechas de cierre por UEI</returns>
        //public Response<Dictionary<Guid, DateTime>> GetClosingDatesByEIU(Session session)
        //{
        //    Response<Dictionary<Guid, DateTime>> ClosingDatesByEIUResponse = new Response<Dictionary<Guid, DateTime>>();

        //    try
        //    {
        //        if (session is null)
        //        {
        //            ClosingDatesByEIUResponse.ErrorStack.Add(string.Format(UserMessages.NullSession,
        //                                                                   nameof(GetClosingDatesByEIU)));
        //            return ClosingDatesByEIUResponse;
        //        }

        //        Dictionary<Guid, DateTime> ClosingDatesByEIU =
        //            new XPQuery<ExternalInvestmentUnitClosingMaster>(session)
        //                .Where(closingMaster => closingMaster != null &&
        //                                        closingMaster.ExternalInvestmentUnit != null)
        //                .ToDictionary(closingMaster => closingMaster.ExternalInvestmentUnit.Oid,
        //                              closingMaster => closingMaster.LastClosingDate);

        //        ClosingDatesByEIUResponse.IsValid = true;
        //        ClosingDatesByEIUResponse.Value = ClosingDatesByEIU;
        //        return ClosingDatesByEIUResponse;
        //    }
        //    catch (Exception exception)
        //    {
        //        Log.WriteInnerExceptionLog(exception);
        //        ClosingDatesByEIUResponse.ErrorStack.Add(string.Format(UserMessages.ProcessUnexpectedException,
        //                                                               "consulta de fechas de cierre por UEI"));
        //        return ClosingDatesByEIUResponse;
        //    }
        //}
    }
}
