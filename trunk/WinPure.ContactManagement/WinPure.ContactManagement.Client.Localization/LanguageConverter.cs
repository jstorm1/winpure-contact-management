#region References

using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data; 

#endregion

namespace WinPure.ContactManagement.Client.Localization
{
    public class LanguageConverter : IValueConverter, IMultiValueConverter
    {
        #region Fields

        private readonly object _defaultValue;
        private readonly string _vid;
        private bool _isStaticUid;
        private string _uid;

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor.
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="vid"></param>
        /// <param name="defaultValue"></param>
        public LanguageConverter(string uid, string vid, object defaultValue)
        {
            _uid = uid;
            _vid = vid;
            _defaultValue = defaultValue;
            _isStaticUid = true;
        }

        #endregion

        #region IMultiValueConverter Members

        /// <summary>
        /// Converts source values to a value for the binding target. The data binding engine calls this method when it propagates the values from source bindings to the binding target.
        /// </summary>
        /// <returns>
        /// A converted value.If the method returns <see langword="null"/>, the valid <see langword="null"/> value is used.A return value of <see cref="T:System.Windows.DependencyProperty"/>.<see cref="F:System.Windows.DependencyProperty.UnsetValue"/> indicates that the converter did not produce a value, and that the binding will use the <see cref="P:System.Windows.Data.BindingBase.FallbackValue"/> if it is available, or else will use the default value.A return value of <see cref="T:System.Windows.Data.Binding"/>.<see cref="F:System.Windows.Data.Binding.DoNothing"/> indicates that the binding does not transfer the value or use the <see cref="P:System.Windows.Data.BindingBase.FallbackValue"/> or the default value.
        /// </returns>
        /// <param name="values">The array of values that the source bindings in the <see cref="T:System.Windows.Data.MultiBinding"/> produces. The value <see cref="F:System.Windows.DependencyProperty.UnsetValue"/> indicates that the source binding has no value to provide for conversion.</param><param name="targetType">The type of the binding target property.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                int parametersCount = _isStaticUid ? values.Length - 1 : values.Length - 2;
                if (string.IsNullOrEmpty(_uid))
                {
                    if (values[1] == null)
                    {
                        throw new ArgumentNullException(
                            "Uid must be provided as the first Binding element, and must not be null");
                    }
                    _isStaticUid = false;
                    _uid = values[1].ToString();
                    --parametersCount;
                }
                LanguageDictionary dictionary = resolveDictionary();
                object translatedObject = dictionary.Translate(_uid, _vid, _defaultValue, targetType);
                if (translatedObject != null && parametersCount != 0)
                {
                    var parameters = new object[parametersCount];
                    Array.Copy(values, values.Length - parametersCount, parameters, 0, parameters.Length);
                    try
                    {
                        translatedObject = string.Format(translatedObject.ToString(), parameters);
                    }
                    catch (Exception)
                    {
                        #region Trace

                        Debug.WriteLine(string.Format("LanguageConverter failed to format text {0}", translatedObject));

                        #endregion
                    }
                }
                return translatedObject;
            }
            catch (Exception ex)
            {
                #region Trace

                Debug.WriteLine(string.Format("LanguageConverter failed to convert text: {0}", ex.Message));

                #endregion
            }
            return null;
        }

        /// <summary>
        /// Converts a binding target value to the source binding values.
        /// </summary>
        /// <returns>
        /// An array of values that have been converted from the target value back to the source values.
        /// </returns>
        /// <param name="value">The value that the binding target produces.</param><param name="targetTypes">The array of types to convert to. The array length indicates the number and types of values that are suggested for the method to return.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[0];
        }

        #endregion

        #region Privates

        private static LanguageDictionary resolveDictionary()
        {
            LanguageDictionary dictionary = LanguageDictionary.GetDictionary(
                LanguageContext.Current.Culture);
            if (dictionary == null)
            {
                throw new InvalidOperationException(string.Format("Dictionary for language {0} was not found",
                                                                  LanguageContext.Current.Culture));
            }
            return dictionary;
        }

        #endregion

        #region IValueConverter Members

        /// <summary>
        /// Converts a value. 
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns <see langword="null"/>, the valid <see langword="null"/> value is used.
        /// </returns>
        /// <param name="value">The value produced by the binding source.</param><param name="targetType">The type of the binding target property.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            LanguageDictionary dictionary = resolveDictionary();
            object translation = dictionary.Translate(_uid, _vid, _defaultValue, targetType);
            return translation;
        }

        /// <summary>
        /// Converts a value. 
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns <see langword="null"/>, the valid <see langword="null"/> value is used.
        /// </returns>
        /// <param name="value">The value that is produced by the binding target.</param><param name="targetType">The type to convert to.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }

        #endregion
    }
}