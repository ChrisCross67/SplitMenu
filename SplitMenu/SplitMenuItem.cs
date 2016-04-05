using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Markup;

namespace SplitMenu
{
    public class SplitMenuItem : RadioButton
    {
        #region Dependencies

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(object), typeof(SplitMenuItem), new PropertyMetadata(null));

        #endregion Dependencies

        public SplitMenuItem()
        {
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        internal SplitMenuItem ParentItem { get; set; }

        public ItemsControl SubItems
        {
            get { return (ItemsControl)GetValue(SubItemsProperty); }
            set { SetValue(SubItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SubItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SubItemsProperty =
            DependencyProperty.Register("SubItems", typeof(ItemsControl), typeof(SplitMenuItem), new PropertyMetadata(null, OnSubItemsChanged));

        private static void OnSubItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var splitMenuItem = d as SplitMenuItem;
            if (splitMenuItem == null)
                return;
            //SplitViewNavButtonStyle
            if (e.OldValue != null)
            {
                var oldItemsControl = e.NewValue as ItemsControl;
                //((INotifyCollectionChanged)oldItemsControl.Items).CollectionChanged -= OnSubItemCollectionChanged;

                var parentPopup = oldItemsControl.Parent as Popup;
                if(parentPopup != null)
                {
                    parentPopup.Child = null;
                }
            }
            if (e.NewValue != null)
            {
                var itemsControl = e.NewValue as ItemsControl;
                if (itemsControl == null)
                    return;
                itemsControl.ItemsPanel = GetItemsPanelTemplate();
                //((INotifyCollectionChanged)itemsControl.Items).CollectionChanged += OnSubItemCollectionChanged;
                //new ItemsPanelTemplate(new StackPanel());
                var popup = new Popup
                {
                    StaysOpen = false,
                    PlacementTarget = splitMenuItem,
                    Placement = PlacementMode.Right,
                    Child = itemsControl
                };
                var binding = new Binding
                {
                    Source = splitMenuItem,
                    Path = new PropertyPath(nameof(IsChecked)),
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    NotifyOnSourceUpdated = true
                };
                BindingOperations.SetBinding(popup, Popup.IsOpenProperty, binding);

                //var subItemStyle = Application.Current.Resources["SplitViewMenuButtonStyle"] as Style;
                //if(subItemStyle != null)
                //    splitMenuItem.Style = subItemStyle;
            }
        }

        private static void OnSubItemCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var subItem = sender as SplitMenuItem;
            if (subItem == null)
                return;
            subItem.GroupName = "Sub 1";
        }

        private static ItemsPanelTemplate GetItemsPanelTemplate()
        {
            string xaml = @"<ItemsPanelTemplate
                                    xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
                                    xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'>
                                <StackPanel/>
                            </ItemsPanelTemplate>";
            return XamlReader.Parse(xaml) as ItemsPanelTemplate;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public object Icon
        {
            get { return (object)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        #endregion Properties
    }
}