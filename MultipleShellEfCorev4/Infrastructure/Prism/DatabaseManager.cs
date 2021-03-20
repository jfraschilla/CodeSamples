using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Infrastructure.Prism
{
    public class DatabaseManager : IDatabaseManager
    {
        public string DatabaseName { get; set; }

        public static readonly DependencyProperty DatabaseManagerProperty = DependencyProperty.RegisterAttached("DatabaseManager", typeof(IDatabaseManager), typeof(DatabaseManager), null);

        public static IDatabaseManager GetDatabaseManager(DependencyObject target)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            return (IDatabaseManager)target.GetValue(DatabaseManagerProperty);
        }

        public static void SetDatabaseManager(DependencyObject target, IDatabaseManager value)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.SetValue(DatabaseManagerProperty, value);
        }

        public IDatabaseManager CreateDatabaseManager()
        {
            return new DatabaseManager();
        }


    }
}
