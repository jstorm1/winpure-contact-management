#region References

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Transitionals;
using WinPure.ContactManagement.Client.Data.Managers.DataManagers;
using WinPure.ContactManagement.Client.Data.Model;
using WinPure.ContactManagement.Common;

#endregion

namespace WinPure.ContactManagement.Client.Data.Managers
{
    public class TransitionsManager
    {
        #region Events

        public event EventHandler CurrentTransitionChanged;

        #endregion

        #region Fields

        private readonly ObservableCollection<Type> _transitionTypes = new ObservableCollection<Type>();
        private Transition _currentTransition;
        private readonly Setting _transitionSetting;

        #endregion

        #region Singleton Constructor

        private static TransitionsManager _instance;

        private TransitionsManager()
        {
            loadStockTransitions();

            _transitionSetting = loadTransitionSettings();
            if (_transitionSetting != null) activateTransition(_transitionSetting.Value);
        }

        /// <summary>
        /// Returns current instance.
        /// </summary>
        public static TransitionsManager Current
        {
            get { return _instance ?? (_instance = new TransitionsManager()); }
        }

        #endregion

        #region Properties

        public Transition CurrentTransition
        {
            get { return _currentTransition; }
            private set
            {
                _currentTransition = value;

                //Notify about changes.
                if (CurrentTransitionChanged != null)
                    CurrentTransitionChanged.Invoke(this, null);
            }
        }

        public ObservableCollection<Type> TransitionTypes
        {
            get { return _transitionTypes; } 
        }

        #endregion

        #region Methods

        private Setting loadTransitionSettings()
        {
            var settingsCache = SettingsManager.Current.LoadSettings();

            var transitionAssemblyName = settingsCache.Where(s => s.Name == SettingsConstants.TRANSITION_ASSEMBLY_NAME).FirstOrDefault();
            if (transitionAssemblyName == null)
            {
                SettingsManager.Current.AddSetting(SettingsConstants.TRANSITION_ASSEMBLY_NAME, null);
               transitionAssemblyName = loadTransitionSettings();
            }

            return transitionAssemblyName;
        }

        /// <summary>
        /// Loads the transitions found in the specified assembly.
        /// </summary>
        /// <param name="assembly">
        /// The assembly to search for transitions in.
        /// </param>
        private void loadTransitions(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                // Must not already exist
                if (TransitionTypes.Contains(type))
                {
                    continue;
                }

                // Must not be abstract.
                if ((typeof (Transition).IsAssignableFrom(type)) && (!type.IsAbstract))
                {
                    TransitionTypes.Add(type);
                }
            }
        }

        /// <summary>
        /// Loads the transitions that are part of the framework.
        /// </summary>
        private void loadStockTransitions()
        {
            loadTransitions(Assembly.GetAssembly(typeof (Transition)));
        }

        private void activateTransition(string typeName)
        {
            if (string.IsNullOrEmpty(typeName)) return;
            ActivateTransition(Type.GetType(typeName));
        }

        /// <summary>
        /// Activates a transition and displays it.
        /// </summary>
        /// <param name="transitionType">
        /// The type of transition to activate.
        /// </param>
        public void ActivateTransition(Type transitionType)
        {
            // If no type, ignore
            if (transitionType == null) return;

            // Create the instance
            Transition transition = GetTransition(transitionType);

            CurrentTransition = transition;
        }

        public Transition GetTransition(Type transitionType)
        {
            return (Transition) Activator.CreateInstance(transitionType);
        }

        public void SaveSettings()
        {
            if (_transitionSetting == null)
            {
                SettingsManager.Current.AddSetting(SettingsConstants.TRANSITION_ASSEMBLY_NAME, CurrentTransition.GetType().AssemblyQualifiedName);
                return;
            }


            _transitionSetting.Value = CurrentTransition.GetType().AssemblyQualifiedName;
            SettingsManager.Current.Save(_transitionSetting);
        }

        #endregion
    }
}