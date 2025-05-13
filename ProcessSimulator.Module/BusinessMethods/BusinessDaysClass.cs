using DevExpress.Xpo;
using ProcessSimulator.Module.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.BusinessMethods
{
    public class BusinessDaysClass
    {

        public static DateTime GetClosingDate(Session session, ExternalInvestmentUnit externalInvestmentUnit, int daysToAdd)
           => FindClosingDate(session, externalInvestmentUnit, daysToAdd);


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

        /// <summary>
        /// Función que devuelve una fecha de cumplimiento basado en los días feriados registrados en el sistema y la cantidad de días que se quieran
        /// </summary>
        /// <param name="add">Días a adicionar</param>
        /// <param name="fisrtDate">FechaInicial</param>
        /// <param name="session">Sesión</param>
        /// <returns>Fecha calculada</returns>
        public static DateTime AddBusinessDays(Int32 add, DateTime fisrtDate, Session session)
        {
            if (fisrtDate.DayOfWeek == DayOfWeek.Saturday) { fisrtDate = fisrtDate.AddDays(2); }
            if (fisrtDate.DayOfWeek == DayOfWeek.Sunday) { fisrtDate = fisrtDate.AddDays(1); }
            Int32 weeks = add / 5;
            add += weeks * 2;
            if (fisrtDate.DayOfWeek > fisrtDate.AddDays(add).DayOfWeek) { add += 2; }
            if (fisrtDate.AddDays(add).DayOfWeek == DayOfWeek.Saturday) { add += 2; }
            Int32 libres = 0;
            if (libres > 0) { return AddBusinessDays(0, fisrtDate.AddDays(libres + add), session); }
            else { return fisrtDate.AddDays(add); }
        }

        /// <summary>
        /// Función que devuelve una fecha de cumplimiento basado en los días feriados registrados por un país específico en el sistema y la cantidad de días que se quieran
        /// </summary>
        /// <param name="add">Días a adicionar</param>
        /// <param name="fisrtDate">FechaInicial</param>
        /// <param name="session">Sesión</param>
        /// <returns>Fecha calculada</returns>
        //public static DateTime AddBusinessDaysCountry(Int32 add, DateTime fisrtDate, Session session, Countries country)
        //{
        //    if (fisrtDate.DayOfWeek == DayOfWeek.Saturday) { fisrtDate = fisrtDate.AddDays(2); }
        //    if (fisrtDate.DayOfWeek == DayOfWeek.Sunday) { fisrtDate = fisrtDate.AddDays(1); }
        //    Int32 weeks = add / 5;
        //    add += weeks * 2;
        //    if (fisrtDate.DayOfWeek > fisrtDate.AddDays(add).DayOfWeek) { add += 2; }
        //    if (fisrtDate.AddDays(add).DayOfWeek == DayOfWeek.Saturday) { add += 2; }
        //    //Cristian Sarmiento : Maintenance/Feature #13094 : 07/09/2020
        //    Int32 libres = HolidaysBetweenForCountry(fisrtDate, fisrtDate.AddDays(add), session, country.Code);

        //    if (libres > 0) { return AddBusinessDaysCountry(0, fisrtDate.AddDays(libres + add), session, country); }
        //    else { return fisrtDate.AddDays(add); }
        //}

        /// <summary>
        /// Método que retorna el parámetro fisrtDate más el valor de add agregando solo diás habiles, es decir lunes a viernes no festivos.
        /// </summary>
        /// <param name="add"></param>
        /// <param name="fisrtDate"></param>
        /// <param name="session"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        //public static DateTime AddBussinessDaysByCountries(int add, DateTime fisrtDate, Session session, Countries country)
        //{
        //    List<string> CountryCodes =
        //        ApplicationParametersManager.ApplicationParametersManager.GetApplicationParameterValue("FestivosAdicionalesPorPais", session)
        //                                                                 .Replace(" ", string.Empty)
        //                                                                 .Split(',')
        //                                                                 .ToList();
        //    CountryCodes.Add(country.Code);

        //    DateTime[] HolidayDates =
        //        new XPQuery<Holiday>(session).Where(holiday => holiday.Date >= fisrtDate &&
        //                                                             CountryCodes.Contains(holiday.Country.Code))
        //                                           .OrderBy(holiday => holiday.Date)
        //                                           .Select(holiday => holiday.Date)
        //                                           .Distinct()
        //                                           .Take(20)
        //                                           .ToArray();

        //    DateTime ModifiedDate = fisrtDate;
        //    while (add > 0)
        //    {
        //        ModifiedDate = ModifiedDate.AddDays(1);
        //        while (IsHoliday(ModifiedDate, HolidayDates))
        //            ModifiedDate = ModifiedDate.AddDays(1);
        //        add--;
        //    }
        //    return ModifiedDate;
        //}

        //Cristian Sarmiento : Maintenance/Feature #13094 : 07/09/2020
        /// <summary>
        /// Función que calcula los días feriados entre dos fechas de un país específico
        /// </summary>
        /// <param name="fisrtDate">Fecha Inicial</param>
        /// <param name="finalDate">Fecha Final</param>
        /// <param name="session">Sesión</param>
        /// <param name="country">País</param>
        /// <returns></returns>
        //public static Int32 HolidaysBetweenForCountry(DateTime fisrtDate, DateTime finalDate, Session session, string countryCode)
        //{
        //    Int32 dias = 0;
        //    if (!session.IsObjectsLoading && !session.IsObjectsSaving)
        //    {
        //        //Buscamos los feriados de Colombia y Estados Unidos
        //        List<Holiday> holidays = (from holiday in new XPQuery<Holiday>(session)
        //                                        where holiday.Date.Date >= fisrtDate.Date
        //                                        && holiday.Date.Date <= finalDate.Date
        //                                              //Cristian Sarmiento : Maintenance/Feature #13094 : 07/09/2020
        //                                              && holiday.Country.Code == countryCode
        //                                        select holiday).ToList();
        //        if (holidays.Count() > 0)
        //        {
        //            foreach (Holiday holiday in holidays)
        //            {
        //                if (holiday.Date.DayOfWeek != DayOfWeek.Saturday || holiday.Date.DayOfWeek != DayOfWeek.Sunday)
        //                {
        //                    dias += 1;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            dias = 0;
        //        }
        //    }
        //    return dias;
        //}

        //public static bool HolidaysBetweenForCountry(DateTime date, IObjectSpace ios, string countryCode)
        //    => date.Date.DayOfWeek == DayOfWeek.Saturday || date.Date.DayOfWeek == DayOfWeek.Sunday ||
        //           new XPQuery<Holiday>(((XPObjectSpace)ios).Session).Any(holiday => holiday.Date == date && holiday.Country.Code == countryCode);

        //public static bool HolidaysBetweenForCountry(DateTime date, Session session, string countryCode)
        //    => date.Date.DayOfWeek == DayOfWeek.Saturday || date.Date.DayOfWeek == DayOfWeek.Sunday ||
        //           new XPQuery<Holiday>(session).Any(holiday => holiday.Date == date && holiday.Country.Code == countryCode);

        //public static bool HolidaysBetweenForCountry(string countryCode, DateTime date, UnitOfWork unitOfWork)
        //    => date.Date.DayOfWeek == DayOfWeek.Saturday || date.Date.DayOfWeek == DayOfWeek.Sunday ||
        //       new XPQuery<Holiday>(unitOfWork).Any(holiday => holiday.Date == date && holiday.Country.Code == countryCode);

        /// <summary>
        /// Método que valida si una fecha es un día hábil o no
        /// </summary>
        /// <param name="date"></param>
        /// <param name="holidays"></param>
        /// <returns></returns>
        public static bool IsHoliday(DateTime date, DateTime[] holidays)
            => date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || holidays.Any(holiday => holiday == date);

        /// <summary>
        /// Método que retorna las fechas para los días festivos posteriores a una fecha dada.
        /// NOTA: El Response es invalido si la tabla de días festivos esta vacia.
        /// </summary>
        /// <param name="unitOfWork">Unidad de trabajo.</param>
        /// <param name="operationDate">Fecha de operación.</param>
        /// <returns>Response de un Array de fechas festivas posteriores a la fecha de operación.</returns>
        //public Response<DateTime[]> HolidaysAfterOperationDate(UnitOfWork unitOfWork, DateTime operationDate)
        //{
        //    Response<DateTime[]> HolidaysAfterOperationDateResponse = new Response<DateTime[]>();
        //    try
        //    {
        //        if (unitOfWork == null) HolidaysAfterOperationDateResponse.ErrorStack.Add(string.Format(UserMessages.NullUnitOfWork, nameof(HolidaysAfterOperationDate)));
        //        if (operationDate == DateTime.MinValue) HolidaysAfterOperationDateResponse.ErrorStack.Add("Fecha de operacion no válida.");
        //        if (HolidaysAfterOperationDateResponse.ErrorStack.Any()) return HolidaysAfterOperationDateResponse;

        //        string[] CountryCodes = new XPQuery<ApplicationParameters>(unitOfWork).Where(applicationParameter => applicationParameter.Property == BusinessExpressions.AdditionalHolidaysByCountries
        //                                                                                                                || applicationParameter.Property == BusinessExpressions.CountryCode)
        //                                                                                    .Select(applicationParameter => applicationParameter.Value)
        //                                                                                    .ToArray();

        //        if (CountryCodes.Count() == 0)
        //        {
        //            HolidaysAfterOperationDateResponse.ErrorStack.Add($"No hay códigos que permitan identificar los días festivos.");
        //            return HolidaysAfterOperationDateResponse;
        //        }

        //        DateTime[] HolidayDates = new XPQuery<Holiday>(unitOfWork).Where(holiday => holiday.Date >= operationDate.Date
        //                                                                                       && CountryCodes.Contains(holiday.Country.Code))
        //                                                                        .Select(holiday => holiday.Date)
        //                                                                        .ToArray();

        //        HolidaysAfterOperationDateResponse.Value = HolidayDates;
        //        HolidaysAfterOperationDateResponse.IsValid = true;
        //        return HolidaysAfterOperationDateResponse;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteInnerExceptionLog(ex);
        //        HolidaysAfterOperationDateResponse.ErrorStack.Add(string.Format(UserMessages.ProcessUnexpectedException, nameof(HolidaysAfterOperationDate)));
        //        return HolidaysAfterOperationDateResponse;
        //    }
        //}

        /// <summary>
        /// Método que retorna la fecha de cumplimiento dependiendo de los festivos próximos a la fecha de operación.
        /// </summary>
        /// <param name="daysToAdd">Días a añadir.</param>
        /// <param name="initialDate">Fecha de operación.</param>
        /// <param name="holidays">Arreglo de días festivos posteriores a la fecha de operación.</param>
        /// <returns>Response de un DateTime posterior o igual a la fecha de operación.</returns>
        //public Response<DateTime> GetComplianceDate(int daysToAdd, DateTime initialDate, DateTime[] holidays)
        //{
        //    Response<DateTime> GetComplianceDateResponse = new Response<DateTime>();
        //    try
        //    {
        //        if (initialDate == DateTime.MinValue) GetComplianceDateResponse.ErrorStack.Add("La fecha de operación no es valida.");
        //        if (GetComplianceDateResponse.ErrorStack.Any()) return GetComplianceDateResponse;

        //        DateTime ModifiedDate = initialDate;
        //        while (daysToAdd > 0)
        //        {
        //            ModifiedDate = ModifiedDate.AddDays(1);
        //            while (IsHoliday(ModifiedDate, holidays)) ModifiedDate = ModifiedDate.AddDays(1);
        //            daysToAdd--;
        //        }

        //        GetComplianceDateResponse.Value = ModifiedDate;
        //        GetComplianceDateResponse.IsValid = true;
        //        return GetComplianceDateResponse;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteInnerExceptionLog(ex);
        //        GetComplianceDateResponse.ErrorStack.Add(string.Format(UserMessages.ProcessUnexpectedException, nameof(GetComplianceDate)));
        //        return GetComplianceDateResponse;
        //    }
        //}

        /// <summary>
        /// Método que retorna la fecha de cumplimiento de acuerdo a los festivos.
        /// NOTA: El Response es inválido en el caso de que no se haya podido calcular los días 
        /// que se deben añadir para hallar la fecha de cumplimiento.
        /// </summary>
        /// <param name="daysToAdd">Días a añadir para calcular la fecha de cumplimiento.</param>
        /// <param name="initialDate">Fecha de operación</param>
        /// <param name="session">Sesión usada como parámetro para indicar si se esta cargando o guardando.</param>
        /// <param name="holidays">Lista con los festivos posteriores a una fecha dada relacionada con la fecha de cierre.</param>
        /// <returns>Response con la fecha de cumplimiento</returns>
        public DateTime GetComplianceDate(int daysToAdd, DateTime initialDate, Session session, DateTime[] holidays)
        {
            //Response<DateTime> GetComplianceDateResponse = new();
            try
            {
                //if (daysToAdd < 0) GetComplianceDateResponse.ErrorStack.Add("Los días a añadir no pueden ser negativos.");
                //if (initialDate == DateTime.MinValue) GetComplianceDateResponse.ErrorStack.Add("La fecha de operación no es válida.");
                //if (session == null) GetComplianceDateResponse.ErrorStack.Add($"La sesión es nula en {nameof(GetComplianceDate)}.");
                //if (GetComplianceDateResponse.ErrorStack.Any()) return GetComplianceDateResponse;

                DateTime[] HolidaysAfterInitialDate = holidays.Where(date => date >= initialDate).ToArray();

                if (initialDate.DayOfWeek == DayOfWeek.Saturday) initialDate = initialDate.AddDays(2);
                if (initialDate.DayOfWeek == DayOfWeek.Sunday) initialDate = initialDate.AddDays(1);
                int Weeks = daysToAdd / 5;
                daysToAdd += Weeks * 2;
                if (initialDate.DayOfWeek > initialDate.AddDays(daysToAdd).DayOfWeek) daysToAdd += 2;
                if (initialDate.AddDays(daysToAdd).DayOfWeek == DayOfWeek.Saturday) daysToAdd += 2;

                int FreeDays = DaysToAddBetweenDates(initialDate, initialDate.AddDays(daysToAdd), session, HolidaysAfterInitialDate);

                //if (!FreeDays.IsValid)
                //{
                //    GetComplianceDateResponse.ErrorStack.Add("Error al encontrar los días de negocio a añadir.");
                //    return GetComplianceDateResponse;
                //}

                //if (FreeDays.Value > 0) return GetComplianceDate(0, initialDate.AddDays(FreeDays.Value + daysToAdd), session, HolidaysAfterInitialDate);
                //else
                //{
                //    GetComplianceDateResponse.Result = initialDate.AddDays(daysToAdd);
                //    //GetComplianceDateResponse.IsValid = true;
                //    return GetComplianceDateResponse;
                //}
                //GetComplianceDateResponse.Result = initialDate.AddDays(daysToAdd);
                return initialDate.AddDays(daysToAdd);
            }
            catch (Exception ex)
            {
                //Log.WriteInnerExceptionLog(ex);
                //GetComplianceDateResponse.ErrorStack.Add(string.Format(UserMessages.ProcessUnexpectedException, nameof(GetComplianceDate)));
                return new DateTime(1900, 1, 1);
            }
        }

        /// <summary>
        /// Método que retorna la cantidad de días que se deben añadir para encontrar la fecha de cumplimiento.
        /// </summary>
        /// <param name="initialDate">Fecha de cierre.</param>
        /// <param name="finalDate">Fecha de cumplimiento supuesta.</param>
        /// <param name="session">Sesión usada como parámetro para indicar si se esta cargando o guardando.</param>
        /// <param name="holidays">Lista de días festivos posteriores a la fecha de cierre.</param>
        /// <returns>Response con la cantidad de días a añadir dada la fecha de cierre y la fecha de operación supuesta.</returns>
        private int DaysToAddBetweenDates(DateTime initialDate, DateTime finalDate, Session session, DateTime[] holidays)
        {

            try
            {
                //if (initialDate == DateTime.MinValue) DaysToAddBetweenDatesResponse.ErrorMessage = "La fecha de cierre no es válida.";
                //if (finalDate == DateTime.MinValue) DaysToAddBetweenDatesResponse.ErrorMessage = "La fecha de cumplimiento no es válida.";
                //if (session == null) DaysToAddBetweenDatesResponse.ErrorMessage = $"La sesión es nula en {nameof(DaysToAddBetweenDates)}";

                int DaysToAdd = 0;
                if (!session.IsObjectsLoading && !session.IsObjectsSaving)
                {
                    if (holidays.Count() != 0)
                    {
                        DateTime[] HolidaysBetweenDates = holidays.Where(holiday => holiday <= finalDate
                                                               && holiday >= initialDate).ToArray();

                        foreach (DateTime holiday in HolidaysBetweenDates)
                        {
                            if (holiday.DayOfWeek != DayOfWeek.Saturday || holiday.DayOfWeek != DayOfWeek.Sunday)
                                DaysToAdd += 1;
                        }
                    }
                }
                //DaysToAddBetweenDatesResponse. = DaysToAdd;
                //DaysToAddBetweenDatesResponse.Success = true;
                return DaysToAdd;
            }
            catch (Exception ex)
            {
                //DaysToAddBetweenDatesResponse.ErrorMessage = ex.Message;
                return 0;
            }
        }

    }
}
