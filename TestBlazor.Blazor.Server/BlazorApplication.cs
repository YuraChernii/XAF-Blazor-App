using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;

namespace TestBlazor.Blazor.Server
{
    public class TestBlazorBlazorApplication : BlazorApplication
    {
        public TestBlazorBlazorApplication()
        {
            ApplicationName = "TestBlazor";
            CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema;
            DatabaseVersionMismatch += TestBlazorBlazorApplication_DatabaseVersionMismatch;
            string connectionString = "XpoProvider=MSSqlServer;Data Source=CHERNIYMYKHAILO;User ID=sa;Password=M06082004i;Initial Catalog=XAFBlazor;Persist Security Info=true";
            XpoDefault.DataLayer = XpoDefault.GetDataLayer(connectionString, AutoCreateOption.DatabaseAndSchema);
        }

        protected override void OnSetupStarted()
        {
            base.OnSetupStarted();
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached && CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema)
            {
                DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
            }
#endif
        }

        private void TestBlazorBlazorApplication_DatabaseVersionMismatch(object sender, DatabaseVersionMismatchEventArgs e)
        {
#if EASYTEST
            e.Updater.Update();
            e.Handled = true;
#else
            if (System.Diagnostics.Debugger.IsAttached)
            {
                e.Updater.Update();
                e.Handled = true;
            }
            else
            {
                string message = "The application cannot connect to the specified database, " +
                    "because the database doesn't exist, its version is older " +
                    "than that of the application or its schema does not match " +
                    "the ORM data model structure. To avoid this error, use one " +
                    "of the solutions from the https://www.devexpress.com/kb=T367835 KB Article.";

                if (e.CompatibilityError != null && e.CompatibilityError.Exception != null)
                {
                    message += "\r\n\r\nInner exception: " + e.CompatibilityError.Exception.Message;
                }
                throw new InvalidOperationException(message);
            }
#endif
        }
    }
}
