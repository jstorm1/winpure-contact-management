#region References

using System;
using System.Collections.ObjectModel;
using System.Reflection;
using Transitionals;
using WinPure.ContactManagement.Client.ViewModels.Base;

#endregion

namespace WinPure.ContactManagement.Client.ViewModels.Settings
{
    public class ViewTabViewModel : ViewModelBase
    {
        #region Fields

        private readonly ObservableCollection<Type> _transitionTypes = new ObservableCollection<Type>();
        private Type _selectedType;
        private Transition _selectedTransition;
        private string _selectedTypeName;
        private bool _contentSwitcher;

        #endregion

        #region Constructor

        public ViewTabViewModel()
        {
            // Load transitions
            LoadStockTransitions();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the list of known transition types.
        /// </summary>
        /// <value>
        /// The list of known transition types.
        /// </value>
        public ObservableCollection<Type> TransitionTypes
        {
            get { return _transitionTypes; }
        }

        /// <summary>
        /// Gets or Sets Curretlly selected transition type.
        /// </summary>
        public Type SelectedType
        {
            get { return _selectedType; }
            set
            {
                if (_selectedType == value) return;
                _selectedType = value;
                
                RaisePropertyChanged("SelectedType");
                
                ActivateTransition(_selectedType);
                SelectedTypeName = _selectedType.Name;
                ContentSwitcher = !ContentSwitcher;
            }
        }

        /// <summary>
        /// Gets Curretlly selected transition type.
        /// </summary>
        public Transition SelectedTransition
        {
            get { return _selectedTransition; }
            private set
            {
                _selectedTransition = value;
                RaisePropertyChanged("SelectedTransition");
            }
        }

        public string SelectedTypeName
        {
            get { return _selectedTypeName; }
            set
            {
                if (_selectedTypeName == value) return;
                _selectedTypeName = value;
                RaisePropertyChanged("SelectedTypeName");
            }
        }

        /// <summary>
        /// If false then Fisrt page must be visible,
        /// if true then second page must be visible.
        /// </summary>
        public bool ContentSwitcher
        {
            get { return _contentSwitcher; }
            set
            {
                if (_contentSwitcher == value) return;
                _contentSwitcher = value;
                RaisePropertyChanged("ContentSwitcher");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the transitions found in the specified assembly.
        /// </summary>
        /// <param name="assembly">
        /// The assembly to search for transitions in.
        /// </param>
        private void LoadTransitions(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                // Must not already exist
                if (_transitionTypes.Contains(type))
                {
                    continue;
                }

                // Must not be abstract.
                if ((typeof (Transition).IsAssignableFrom(type)) && (!type.IsAbstract))
                {
                    _transitionTypes.Add(type);
                }
            }
        }

        /// <summary>
        /// Loads the transitions that are part of the framework.
        /// </summary>
        private void LoadStockTransitions()
        {
            LoadTransitions(Assembly.GetAssembly(typeof (Transition)));
        }


        /// <summary>
        /// Activates a transition and displays it.
        /// </summary>
        /// <param name="transitionType">
        /// The type of transition to activate.
        /// </param>
        private void ActivateTransition(Type transitionType)
        {
            // If no type, ignore
            if (transitionType == null) return;

            // Create the instance
            var transition = (Transition)Activator.CreateInstance(transitionType);

            SelectedTransition = transition;

            // Swap cells to show transition
            //SwapCell();
        }

        #endregion
    }
}