using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class AssetTypeModel : EnterpriseBaseObject
    {
        private int iD;
        private string name;

        public AssetTypeModel(Session session) : base(session) { }

        [Association("AssetTypeModel-AssetSpeciesModel", typeof(AssetSpeciesModel)), Aggregated]
        public XPCollection<AssetSpeciesModel> AssetSpecia
        {
            get { return GetCollection<AssetSpeciesModel>("AssetSpecia"); }
        }

        public int ID
        {
            get { return iD; }
            set { SetPropertyValue("ID", ref iD, value); }
        }

        public string Name
        {
            get { return name; }
            set { SetPropertyValue("Name", ref name, value); }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
