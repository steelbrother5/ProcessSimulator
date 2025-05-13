using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using ProcessSimulator.Module.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Models
{
    public class CompanyBaseObject : EnterpriseBaseObject
    {
        private int iD;
        private string name;

        public CompanyBaseObject(Session session) : base(session) { }

        public int ID
        {
            get { return iD; }
            set { SetPropertyValue("ID", ref iD, value); }
        }

        [RuleRegularExpression(DefaultContexts.Save, RegularExpressions.LettersNumbersWithAccents, CustomMessageTemplate = "El Nombre solo puede contener letras, números, guiones, tildes y apostrofe. ( -, ' y espacios)")]
        public string Name
        {
            get { return name; }
            set { SetPropertyValue("Name", ref name, value); }
        }

        [Association("ExternalInvestmentUnit-CompanyBaseObject", typeof(ExternalInvestmentUnit)), Aggregated]
        public XPCollection<ExternalInvestmentUnit> ExternalInvestmentUnits
        {
            get { return GetCollection<ExternalInvestmentUnit>("ExternalInvestmentUnits"); }
        }

        private byte[] logo;
        [ImageEditor(ImageSizeMode = ImageSizeMode.StretchImage, DetailViewImageEditorMode = ImageEditorMode.DropDownPictureEdit)]
        public byte[] Logo
        {
            get => logo;
            set => SetPropertyValue<byte[]>("Logo", ref logo, value);
        }

        private byte[] declarationsReportLogo;
        /// <summary>
        /// Logotipo con el cual se van a generar las declaraciones de cambio. 
        /// </summary>
        [ImageEditor(ImageSizeMode = ImageSizeMode.StretchImage, DetailViewImageEditorMode = ImageEditorMode.DropDownPictureEdit)]
        public byte[] DeclarationsReportLogo
        {
            get => declarationsReportLogo;
            set => SetPropertyValue<byte[]>(nameof(DeclarationsReportLogo), ref declarationsReportLogo, value);
        }
        public override void AfterConstruction() => base.AfterConstruction();
    }
}
