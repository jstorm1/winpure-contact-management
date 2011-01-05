using System;
using System.Globalization;
using System.Windows.Data;

namespace WinPure.ContactManagement.Client.CommonControls.Helpers
{
    internal class NavigationMultiConverter : IMultiValueConverter
    {
        #region IMultiValueConverter Members

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
           
            bool canNavigate = false;
            bool can = false;
            
            try
            {
                canNavigate = (bool)values[0];
                can = (bool)values[1];
            }
            catch (Exception)
            {
            }

            return can && canNavigate;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}