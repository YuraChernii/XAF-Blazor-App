using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace XAF_Blazor_App.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class Driver : BaseObject
    {
        public Driver(Session session) : base(session) { }

        string _name;
        string _lastName;
        int _age;
        int _drivingExperience;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField]
        public string Name
        {
            get => _name;
            set => SetPropertyValue(nameof(Name), ref _name, value);
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField]
        public string LastName
        {
            get => _lastName;
            set => SetPropertyValue(nameof(LastName), ref _lastName, value);
        }

        [RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThanOrEqual, 18, CustomMessageTemplate = "Age must be at least 18.")]
        [RuleValueComparison(DefaultContexts.Save, ValueComparisonType.LessThanOrEqual, 100, CustomMessageTemplate = "Age should be less.")]
        public int Age
        {
            get => _age;
            set => SetPropertyValue(nameof(Age), ref _age, value);
        }

        [RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThanOrEqual, 0, CustomMessageTemplate = "Driving experience must be at least 0 years.")]
        [RuleValueComparison(DefaultContexts.Save, ValueComparisonType.LessThanOrEqual, 82, CustomMessageTemplate = "Driving experience should be less.")]
        public int DrivingExperience
        {
            get => _drivingExperience;
            set => SetPropertyValue(nameof(DrivingExperience), ref _drivingExperience, value);
        }

        [Association("Driver-CarDrivers")]
        public XPCollection<CarDriver> CarDrivers
        {
            get
            {
                return GetCollection<CarDriver>(nameof(CarDrivers));
            }
        }
    }
}
