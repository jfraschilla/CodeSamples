using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Infrastructure.Prism
{
    public static class DatabaseManagerAware
    {
        public static void SetDatabaseManagerAware(object item, IDatabaseManager databaseManager)
        {
            var rmAware = item as IDatabaseManagerAware;
            if (rmAware != null)
                rmAware.DatabaseManager = databaseManager;

            var rmAwareFrameworkElement = item as FrameworkElement;
            if (rmAwareFrameworkElement != null)
            {
                var rmAwareDataContext = rmAwareFrameworkElement.DataContext as IDatabaseManagerAware;
                if (rmAwareDataContext != null)
                    rmAwareDataContext.DatabaseManager = databaseManager;
            }

        }
    }
}
