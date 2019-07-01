using System;
using System.Linq;
using System.Data.SqlClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;

namespace RealtimeSampleTask.Hubs
{

    public  class NewMessageNotifier<T> : IDisposable where T : class
    {
        public event OnChangeEventHandler OnChanged;
        private IQueryable iquery = null;
        private ObjectQuery oquery = null;
        private SqlConnection connection = null;
        private SqlCommand command = null;
        private SqlDependency dependency = null;

        public NewMessageNotifier(DbContext context, IQueryable query)
        {
            this.iquery = query;

            // Get the ObjectQuery directly or convert the DbQuery to ObjectQuery.
            oquery = QueryExtension.GetObjectQuery<T>(context, iquery);

            QueryExtension.GetSqlCommand(oquery, ref connection, ref command);

            RegisterSqlDependency();
        }

        // Starts the notification of SqlDependency 
        public static void StartMonitor(DbContext context)
        {
            try
            {
                SqlDependency.Start(context.Database.Connection.ConnectionString);
            }
            catch (Exception ex)
            {
                throw new System.Exception("Fails to Start the SqlDependency in the ImmediateNotificationRegister class", ex);
            }
        }
        

        private void RegisterSqlDependency()
        {

            // Make sure the command object does not already have
            // a notification object associated with it.
            command.Notification = null;

            // Create and bind the SqlDependency object to the command object.
            dependency = new SqlDependency(command);
            dependency.OnChange += new OnChangeEventHandler(DependencyOnChange);

            // After register SqlDependency, the SqlCommand must be executed, or we can't 
            // get the notification.
            RegisterSqlCommand();
        }

        private void RegisterSqlCommand()
        {
            if (connection != null && command != null)
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        private void DependencyOnChange(object sender, SqlNotificationEventArgs e)
        {

            // Move the original SqlDependency event handler.
            SqlDependency dependency = (SqlDependency)sender;
            dependency.OnChange -= DependencyOnChange;

            if (OnChanged != null)
            {
                OnChanged(this, e);
            }

            // We re-register the SqlDependency.
            RegisterSqlDependency();
        }
        //Stops the notification of SqlDependency 
        public static void StopMonitor(DbContext context)
        {
            try
            {
                SqlDependency.Stop(context.Database.Connection.ConnectionString);
            }
            catch (Exception ex)
            {
                throw new System.Exception("Fails to Stop the SqlDependency in the ImmediateNotificationRegister class", ex);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(Boolean disposed)
        {
            if (disposed)
            {
                if (command != null)
                {
                    command.Dispose();
                    command = null;
                }

                if (connection != null)
                {
                    connection.Dispose();
                    connection = null;
                }

                OnChanged = null;
                iquery = null;
                dependency.OnChange -= DependencyOnChange;
                dependency = null;
            }
        }
    }
}