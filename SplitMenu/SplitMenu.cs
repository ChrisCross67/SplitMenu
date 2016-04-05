using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SplitMenu
{
    public class SplitMenu : ItemsControl
    {
        public SplitMenu()
        {

        }


        /// <summary>
        /// Gets or sets a value indicating whether this instance is expanded.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is expanded; otherwise, <c>false</c>.
        /// </value>
        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsExpanded.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded", typeof(bool), typeof(SplitMenu), new PropertyMetadata(false));



        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(double), typeof(SplitMenu), new PropertyMetadata(48d));


        /// <summary>
        /// Returns true is item is a DragDockPanel.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns>True is item is a DragDockPanel.</returns>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is SplitMenuItem;
        }

        /// <summary>
        /// Returns a new drag dock panel.
        /// </summary>
        /// <returns>A new drag dock panel.</returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new SplitMenuItem();
        }

    }
}
