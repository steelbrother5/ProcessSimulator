using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Xpo;
using ProcessSimulator.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class BankingConcept : EnterpriseBaseObject
    {
        public BankingConcept(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private Enums.BankingConcept _bankingConceptEnum;
        private String _concept;
        private Boolean _automaticCreation;
        private Boolean _showInSearch;

        [Appearance("BankingConceptEnumHide", Visibility = ViewItemVisibility.Hide)]
        public Enums.BankingConcept BankingConceptEnum
        {
            get { return _bankingConceptEnum; }
            set { SetPropertyValue<Enums.BankingConcept>("BankingConceptEnum", ref _bankingConceptEnum, value); }
        }

        /// <summary>
        /// Concepto Bancario
        /// </summary>
        [Appearance("ConceptDisabled", Enabled = false, Criteria = "AutomaticCreation = true or ShowInSearch = true")]
        public String Concept
        {
            get { return _concept; }
            set { SetPropertyValue<String>("Id", ref _concept, value); }
        }

        /// <summary>
        /// Indica si el movimiento se crea automáticamente
        /// </summary>
        [Appearance("AutomaticCreationHide", Visibility = ViewItemVisibility.Hide)]
        public Boolean AutomaticCreation
        {
            get { return _automaticCreation; }
            set { SetPropertyValue<Boolean>("AutomaticCreation", ref _automaticCreation, value); }
        }

        [Appearance("ShowInSearchHide", Visibility = ViewItemVisibility.Hide)]
        public Boolean ShowInSearch
        {
            get { return _showInSearch; }
            set { SetPropertyValue<Boolean>("ShowInSearch", ref _showInSearch, value); }
        }
    }
}
