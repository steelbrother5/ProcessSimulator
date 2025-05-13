using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class PortfolioInventoryItem : PortfolioItem
    {
        private InventoryOwnershipType inventoryOwnershipType;
        private ItemSign itemSign;
        private AssetSpeciesModel assetSpecies;
        private DateTime constitutionDate;
        private decimal constitutionUnits;
        private decimal constitutionValueInLocalCurrency;
        private decimal closingValueInLocalCurrency;
        private Currency currency;
        private bool isPortfolioNoveltyIncome;
        private bool isPortfolioNoveltyOutcome;
        private bool isPortfolioNoveltyGuaranteeConstitution;
        private bool isPortfolioNoveltyGuaranteeRelease;
        private bool isPortfolioNoveltyGuaranteeYield;
        private bool isGuarantee;
        private DepositAccount depositAccount;

        public PortfolioInventoryItem(Session session) : base(session) { }

        [Association("PortfolioInventoryItem-PortfolioInventoryCompensation"), Aggregated]
        public XPCollection<PortfolioInventoryCompensation> PortfolioInventoryCompensations
        {
            get { return GetCollection<PortfolioInventoryCompensation>("PortfolioInventoryCompensations"); }
        }

        public ItemSign ItemSign
        {
            get { return itemSign; }
            set
            {
                if (SetPropertyValue("ItemSign", ref itemSign, value))
                    OnChanged("AccumulatedProfit");
            }
        }

        public AssetSpeciesModel AssetSpecies
        {
            get { return assetSpecies; }
            set { SetPropertyValue("AssetSpecies", ref assetSpecies, value); }
        }

        public InventoryOwnershipType InventoryOwnershipType
        {
            get { return inventoryOwnershipType; }
            set { SetPropertyValue("InventoryOwnershipType", ref inventoryOwnershipType, value); }
        }

        public DateTime ConstitutionDate
        {
            get { return constitutionDate; }
            set { SetPropertyValue("ConstitutionDate", ref constitutionDate, value); }
        }

        public decimal ConstitutionUnits
        {
            get { return constitutionUnits; }
            set
            {
                if (SetPropertyValue("ConstitutionUnits", ref constitutionUnits, value))
                    OnChanged("UnitsBalance");
            }
        }

        public decimal ConstitutionValueInLocalCurrency
        {
            get { return constitutionValueInLocalCurrency; }
            set
            {
                if (SetPropertyValue("ConstitutionValueInLocalCurrency", ref constitutionValueInLocalCurrency, value))
                    OnChanged("AccumulatedProfit");
            }
        }

        public decimal ClosingValueInLocalCurrency
        {
            get { return closingValueInLocalCurrency; }
            set
            {
                if (SetPropertyValue("ClosingValueInLocalCurrency", ref closingValueInLocalCurrency, value))
                    OnChanged("AccumulatedProfit");
            }
        }

        public Currency Currency
        {
            get { return currency; }
            set { SetPropertyValue("Currency", ref currency, value); }
        }

        public bool IsPortfolioNoveltyIncome
        {
            get { return isPortfolioNoveltyIncome; }
            set { SetPropertyValue("IsPortfolioNoveltyIncome", ref isPortfolioNoveltyIncome, value); }
        }

        public bool IsPortfolioNoveltyOutcome
        {
            get { return isPortfolioNoveltyOutcome; }
            set { SetPropertyValue("IsPortfolioNoveltyOutcome", ref isPortfolioNoveltyOutcome, value); }
        }

        public bool IsPortfolioNoveltyGuaranteeConstitution
        {
            get { return isPortfolioNoveltyGuaranteeConstitution; }
            set { SetPropertyValue("IsPortfolioNoveltyGuaranteeConstitution", ref isPortfolioNoveltyGuaranteeConstitution, value); }
        }

        public bool IsPortfolioNoveltyGuaranteeRelease
        {
            get { return isPortfolioNoveltyGuaranteeRelease; }
            set { SetPropertyValue("IsPortfolioNoveltyGuaranteeRelease", ref isPortfolioNoveltyGuaranteeRelease, value); }
        }

        public bool IsPortfolioNoveltyGuaranteeYield
        {
            get { return isPortfolioNoveltyGuaranteeYield; }
            set { SetPropertyValue("IsPortfolioNoveltyGuaranteeYield", ref isPortfolioNoveltyGuaranteeYield, value); }
        }

        public bool IsGuarantee
        {
            get { return isGuarantee; }
            set { SetPropertyValue("IsGuarantee", ref isGuarantee, value); }
        }

        private PortfolioNovelty _portfolioNovelty;
        [Association("PortfolioNovelty-PortfolioInventoryItem")]
        public PortfolioNovelty PortfolioNovelty
        {
            get { return _portfolioNovelty; }
            set { SetPropertyValue("PortfolioNovelty", ref _portfolioNovelty, value); }
        }

        [PersistentAlias("ItemSign.Multiplier * (TotalCompensatedValueInLocalCurrency + ClosingValueInLocalCurrency - ConstitutionValueInLocalCurrency)")]
        public decimal AccumulatedProfit
        {
            get
            {
                object tempObject = EvaluateAlias("AccumulatedProfit");
                if (tempObject != null)
                {
                    return (decimal)tempObject;
                }
                else
                {
                    return 0.0m;
                }
            }
        }

        private decimal totalCompensatedValueInLocalCurrency;

        public decimal TotalCompensatedValueInLocalCurrency
        {
            get { return totalCompensatedValueInLocalCurrency; }
            set { SetPropertyValue("TotalCompensatedValueInLocalCurrency", ref totalCompensatedValueInLocalCurrency, value); }
        }

        private decimal unitsCompensated;

        public decimal UnitsCompensated
        {
            get { return unitsCompensated; }
            set { SetPropertyValue("UnitsCompensated", ref unitsCompensated, value); }
        }


        [PersistentAlias("ConstitutionUnits - UnitsCompensated")]
        public decimal UnitsBalance
        {
            get
            {
                object tempObject = EvaluateAlias("UnitsBalance");
                if (tempObject != null)
                {
                    return (decimal)tempObject;
                }
                else
                {
                    return 0.0m;
                }
            }
        }

        public DepositAccount DepositAccount
        {
            get { return depositAccount; }
            set { SetPropertyValue("DepositAccount", ref depositAccount, value); }
        }
    }
}
