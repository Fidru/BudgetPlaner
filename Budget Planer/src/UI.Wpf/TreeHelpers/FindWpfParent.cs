using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace UI.Wpf.TreeHelpers
{
    public static class FindWpfParent
    {
        public static T FindParentOfType<T>(this DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentDepObj = child;
            do
            {
                parentDepObj = VisualTreeHelper.GetParent(parentDepObj);
                T parent = parentDepObj as T;
                if (parent != null) return parent;
            }
            while (parentDepObj != null);
            return null;
        }
    }
}