#region References

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

#endregion

namespace WinPure.ContactManagement.Client.Helpers
{
    public static class DependencyObjectExtensions
    {
        // walk up the visual tree to find objects of type T
        public static IEnumerable<T> GetChildObjects<T>(this DependencyObject obj)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                object child = VisualTreeHelper.GetChild(obj, i);
                if (child is T)
                    yield return (T) child;

                IEnumerable<T> objects = ((DependencyObject) child).GetChildObjects<T>();
                if (objects != null)

                    foreach (T childObjects in objects)
                        yield return childObjects;
            }
        }

        public static IEnumerable<T> GetChildObjects<T>(this DependencyObject obj, string name)
            where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                object child = VisualTreeHelper.GetChild(obj, i);
                if (child is T && (String.IsNullOrEmpty(name) || ((FrameworkElement) child).Name == name))
                {
                    yield return (T) child;
                }

                IEnumerable<T> objects = ((DependencyObject) child).GetChildObjects<T>(name);
                if (objects != null)

                    foreach (T childObjects in objects)
                        yield return childObjects;
            }
        }

        public static void UpdateBindingSources<T>(this DependencyObject parent, DependencyProperty dependencyProperty)
            where T : FrameworkElement
        {
            IEnumerable<T> objects = parent.GetChildObjects<T>();
            IEnumerable<BindingExpression> bindingExpressions =
                objects.Select(o => o.GetBindingExpression(dependencyProperty)).Where(
                    bindingExpression => bindingExpression != null);

            updateSources(bindingExpressions);
        }

        private static void updateSources(IEnumerable<BindingExpression> bindingExpressions)
        {
            foreach (BindingExpression bindingExpression in bindingExpressions)
            {
                bindingExpression.UpdateSource();
            }
        }
    }
}