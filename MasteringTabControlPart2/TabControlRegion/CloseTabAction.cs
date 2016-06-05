using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;
using Prism.Regions;

namespace TabControlRegion
{
	public class CloseTabAction : TriggerAction<Button>
	{
		protected override void Invoke(object parameter)
		{
			var args = parameter as RoutedEventArgs;
			if (args == null)
			{
				return;
			}

			var tabItem = FindParent<TabItem>(args.OriginalSource as DependencyObject);
			if (tabItem == null)
			{
				return;
			}

			var tabControl = FindParent<TabControl>(tabItem);
			if (tabControl == null)
			{
				return;
			}

			//this command removes tabitems from the tabcontrol
			//however, this is not what we want to do because we are using prism to inject views into the TabRegion
			//which is storing views in the region manaager.  Therefore, we need to remove the view from the region manager
			//if we remove the tabpage form the Items list the views still exist in the region manager using memory and resources
			//which will cause a memory leak.
			//tabControl.Items.Remove(tabItem.Content);

			IRegion region = RegionManager.GetObservableRegion(tabControl).Value;
			if (region == null)
				return;


			//This removes the view from the region directly there is no call to remove anything from TabControl.Items
			//Prism will automatically remove the item from the hosting control
			//This works but does not have any feature to prevent a user from not deleting the item
			//if (region.Views.Contains(tabItem.Content))
			//{
			//	region.Remove(tabItem.Content);
			//}

			RemoveItemFromRegion(tabItem.Content, region);

		}

		void RemoveItemFromRegion(object item, IRegion region)
		{
			var navigationContext = new NavigationContext(region.NavigationService, null);
			if (CanRemove(item, navigationContext))
			{
                InvokeOnNavigatedFrom(item, navigationContext);
				region.Remove(item);
			}
		}

        void InvokeOnNavigatedFrom(object item, NavigationContext navigationContext)
        {
            var navigationAwareItem = item as INavigationAware;
            if (navigationAwareItem != null)
            {
                navigationAwareItem.OnNavigatedFrom(navigationContext);
            }
            var frameworkElement = item as FrameworkElement;
            if (frameworkElement != null)
            {
                INavigationAware navigationAwareDataContext = frameworkElement.DataContext as INavigationAware;
                if (navigationAwareDataContext != null)
                {
                    navigationAwareDataContext.OnNavigatedFrom(navigationContext);
                }
            }
        }


		private bool CanRemove(object item, NavigationContext navigationContext)
		{
			bool canRemove = true;
			var confirmRequestItem = item as IConfirmNavigationRequest;
			if (confirmRequestItem != null)
			{
				confirmRequestItem.ConfirmNavigationRequest(navigationContext, result =>
					{
						canRemove = result;
					});
			}

			var frameworkElement = item as FrameworkElement;
			if (frameworkElement != null && canRemove)
			{
                IConfirmNavigationRequest confirmRequestDataContext = frameworkElement.DataContext as IConfirmNavigationRequest;
				if (confirmRequestDataContext != null)
				{
					confirmRequestDataContext.ConfirmNavigationRequest(navigationContext, result =>
					{
						canRemove = result;
					});
				}
			}
			return canRemove;
		}

		//This code is responsible for finding the parent of an element
		private static T FindParent<T>(DependencyObject child) where T:DependencyObject
		{
			DependencyObject parentObject = VisualTreeHelper.GetParent(child);
			if (parentObject == null)
			{
				return null;
			}

			var parent = parentObject as T;
			if (parent != null)
			{
				return parent;
			}

			return FindParent<T>(parentObject);
		}
	}
}
