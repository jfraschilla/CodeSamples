using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Windows;

namespace Infrastructure.Prism
{
    public class DatabaseManagerAwareBehavior : RegionBehavior
    {
        public const string BehaviorKey = "DatabaseManagerAwareBehavior";

        protected override void OnAttach()
        {
            Region.ActiveViews.CollectionChanged += ActiveViews_CollectionChanged;
        }

        void ActiveViews_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    FrameworkElement frameworkElement = item as FrameworkElement;
                    if (frameworkElement != null)
                    {
                        var dbAwareDataContext = frameworkElement.DataContext as IDatabaseManagerAware;
                        if (dbAwareDataContext != null)
                        {
                            var databaseManager = frameworkElement.GetValue(DatabaseManager.DatabaseManagerProperty) as IDatabaseManager;

                            if (databaseManager == null)
                            {
                                var frameworkElementParent = frameworkElement.Parent as FrameworkElement;
                                if (frameworkElementParent != null)
                                {
                                    var dbAwareDataContextParent = frameworkElementParent.DataContext as IDatabaseManagerAware;
                                    if (dbAwareDataContextParent != null)
                                    {
                                        if (dbAwareDataContext == dbAwareDataContextParent)
                                        {
                                            return;
                                        }
                                    }
                                    dbAwareDataContext.DatabaseManager = dbAwareDataContextParent.DatabaseManager;
                                }
                            }
                        }
                    }
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var item in e.OldItems)
                {
                    FrameworkElement frameworkElement = item as FrameworkElement;
                    if (frameworkElement != null)
                    {
                        var dbAwareDataContext = frameworkElement.DataContext as IDatabaseManagerAware;
                        if (dbAwareDataContext != null)
                        {
                            dbAwareDataContext.DatabaseManager = null;
                        }
                    }
                }
            }
        }

        //static void InvokeOnDatabaseManagerAwareElement(object item, Action<IDatabaseManagerAware> invocation)
        //{
        //    var rmAwareItem = item as IDatabaseManagerAware;
        //    if (rmAwareItem != null)
        //        invocation(rmAwareItem);

        //    var frameworkElement = item as FrameworkElement;
        //    if (frameworkElement != null)
        //    {
        //        IDatabaseManagerAware rmAwareDataContext = frameworkElement.DataContext as IDatabaseManagerAware;
        //        if (rmAwareDataContext != null)
        //        {
        //            var frameworkElementParent = frameworkElement.Parent as FrameworkElement;
        //            if (frameworkElementParent != null)
        //            {
        //                var rmAwareDataContextParent = frameworkElementParent.DataContext as IDatabaseManagerAware;
        //                if (rmAwareDataContextParent != null)
        //                {
        //                    if (rmAwareDataContext == rmAwareDataContextParent)
        //                    {
        //                        return;
        //                    }
        //                }
        //            }

        //            invocation(rmAwareDataContext);
        //        }
        //    }
        //}
    }
}
