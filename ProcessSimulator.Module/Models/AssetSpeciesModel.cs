using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class AssetSpeciesModel : EnterpriseBaseObject
    {
        private int iD;
        private string name;
        private AssetTypeModel assetType;

        public AssetSpeciesModel(Session session) : base(session) { }

        [Association("AssetTypeModel-AssetSpeciesModel", typeof(AssetTypeModel))]
        public AssetTypeModel AssetType
        {
            get { return assetType; }
            set { SetPropertyValue("AssetType", ref assetType, value); }
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
    }
}
