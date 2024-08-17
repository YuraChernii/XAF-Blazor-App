using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Linq;

namespace XAF_Blazor_App.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class CarDriver : BaseObject
    {
        public CarDriver(Session session) : base(session) { }

        private Driver _driver;
        private Car _car;
        private DateTime _endDate;
        private DateTime _editDate;

        [Association("Driver-CarDrivers")]
        [Persistent("DriverId")]  
        [RuleRequiredField]
        public Driver Driver
        {
            get => _driver;
            set => SetPropertyValue(nameof(Driver), ref _driver, value);
        }

        [Association("Car-CarDrivers")]
        [RuleRequiredField]
        [ImmediatePostData]
        [XafDisplayName("Car")]
        public Car Car
        {
            get => _car;
            set => SetPropertyValue(nameof(Car), ref _car, value);
        }

        [RuleRequiredField]
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (value < DateTime.Today)
                {
                    throw new InvalidOperationException("End date cannot be in the past.");
                }

                if (value.DayOfWeek == DayOfWeek.Saturday || value.DayOfWeek == DayOfWeek.Sunday)
                {
                    throw new InvalidOperationException("End date cannot be on a weekend.");
                }

                SetPropertyValue(nameof(EndDate), ref _endDate, value);
            }
        }

        [Browsable(false)]
        public DateTime EditDate
        {
            get => _editDate;
            set => SetPropertyValue(nameof(EditDate), ref _editDate, value);
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (Session.Query<CarDriver>().Any(cd => cd.Car == this.Car && cd.EndDate >= DateTime.Today))
            {
                throw new InvalidOperationException("This car is already rented by another driver.");
            }

            EditDate = DateTime.Now;
        }
    }
}
