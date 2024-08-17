using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using XAF_Blazor_App.Module.BusinessObjects;

public class AvailableCarsController : ObjectViewController<DetailView, CarDriver>
{
    protected override void OnActivated()
    {
        base.OnActivated();

        PropertyEditor carPropertyEditor = View.FindItem("Car") as PropertyEditor;
        if (carPropertyEditor != null)
        {
            carPropertyEditor.ControlCreated += (s, e) =>
            {
                if (carPropertyEditor is IObjectSpaceLink objectSpaceLink)
                {
                    IObjectSpace objectSpace = objectSpaceLink.ObjectSpace;
                    IEnumerable<Car> availableCars = objectSpace.GetObjects<Car>(
                        CriteriaOperator.Parse("not Exists(CarDrivers[EndDate >= ?])",
                        DateTime.Today));

                    carPropertyEditor.PropertyValue = availableCars;
                }
            };
        }
    }

}