using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.ComponentModel.DataAnnotations;

namespace XAF_Blazor_App.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Car : BaseObject
    {
        public Car(Session session) : base(session) { }

        private string _model;
        private string _brand;
        private string _year;
        private FuelType _fuelType;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField]
        [Display(Name = "Бренд")]
        public string Brand
        {
            get => _brand;
            set => SetPropertyValue(nameof(Brand), ref _brand, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField]
        [Display(Name = "Модель")]
        public string Model
        {
            get => _model;
            set => SetPropertyValue(nameof(Model), ref _model, value);
        }

        [RuleRequiredField]
        [RuleRegularExpression(DefaultContexts.Save, @"^(19[7-9]\d|20[0-2]\d)$", CustomMessageTemplate = "Year must be between 1970 and 2025.")]
        [Display(Name = "Рік")]
        public string Year
        {
            get => _year;
            set => SetPropertyValue(nameof(Year), ref _year, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [Display(Name = "Тип пального")]
        public FuelType Fuel
        {
            get => _fuelType;
            set => SetPropertyValue(nameof(Fuel), ref _fuelType, value);
        }

        [DevExpress.Xpo.Association("Car-CarDrivers")]
        public XPCollection<CarDriver> CarDrivers
        {
            get { return GetCollection<CarDriver>(nameof(CarDrivers)); }
        }

        public enum FuelType
        {
            [Display(Name = "Бензин")]
            Petrol,

            [Display(Name = "Дизель")]
            Diesel,

            [Display(Name = "Електро")]
            Electro,

            [Display(Name = "Гібрид")]
            Hybrid
        }
    }
}
