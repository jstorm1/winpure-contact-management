#region References

using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup; 

#endregion

namespace WinPure.ContactManagement.Client.Localization
{
    [ContentProperty("Parameters")]
    public class Translate : MarkupExtension
    {
        #region Fields

        private readonly Collection<BindingBase> _parameters = new Collection<BindingBase>();
        private object _default;
        private DependencyProperty _property;
        private DependencyObject _target;
        private string _uid;

        #endregion

        #region Constructors

        public Translate()
        {
        }

        public Translate(object defaultValue)
        {
            _default = defaultValue;
        }

        #endregion

        #region Properties

        public object Default
        {
            get { return _default; }
            set { _default = value; }
        }

        public string Uid
        {
            get { return _uid; }
            set { _uid = value; }
        }

        public Collection<BindingBase> Parameters
        {
            get { return _parameters; }
        }

        #region UidProperty DProperty

        public static readonly DependencyProperty UidProperty =
            DependencyProperty.RegisterAttached("Uid", typeof (string), typeof (Translate),
                                                new UIPropertyMetadata(string.Empty));

        public static string GetUid(DependencyObject obj)
        {
            return (string) obj.GetValue(UidProperty);
        }

        public static void SetUid(DependencyObject obj, string value)
        {
            obj.SetValue(UidProperty, value);
        }

        #endregion

        #endregion

        #region Overrides

        /// <summary>
        /// When implemented in a derived class, returns an object that is set as the value of the target property for this markup extension. 
        /// </summary>
        /// <returns>
        /// The object value to set on the property where the extension is applied. 
        /// </returns>
        /// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var service = serviceProvider.GetService(typeof (IProvideValueTarget)) as IProvideValueTarget;
            if (service == null)
            {
                return this;
            }

            var property = service.TargetProperty as DependencyProperty;
            var target = service.TargetObject as DependencyObject;
            if (property == null || target == null)
            {
                return this;
            }

            _target = target;
            _property = property;

            return bindDictionary(serviceProvider);
        }

        #endregion

        #region Privates

        private object bindDictionary(IServiceProvider serviceProvider)
        {
            string uid = _uid ?? GetUid(_target);
            string vid = _property.Name;

            var binding = new Binding("Dictionary") {Source = LanguageContext.Current, Mode = BindingMode.TwoWay};
            var converter = new LanguageConverter(uid, vid, _default);
            if (_parameters.Count == 0)
            {
                binding.Converter = converter;
                object value = binding.ProvideValue(serviceProvider);
                return value;
            }
            else
            {
                var multiBinding = new MultiBinding {Mode = BindingMode.TwoWay, Converter = converter};
                multiBinding.Bindings.Add(binding);
                if (string.IsNullOrEmpty(uid))
                {
                    var uidBinding = _parameters[0] as Binding;
                    if (uidBinding == null)
                    {
                        throw new ArgumentException("Uid Binding parameter must be the first, and of type Binding");
                    }
                }
                foreach (Binding parameter in _parameters)
                {
                    multiBinding.Bindings.Add(parameter);
                }
                object value = multiBinding.ProvideValue(serviceProvider);
                return value;
            }
        }

        #endregion
    }
}