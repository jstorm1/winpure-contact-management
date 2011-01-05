using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WinPure.ContactManagement.Client.CustomMessageBox.Commands;

namespace WinPure.ContactManagement.Client.CustomMessageBox
{
    public class MessageBoxViewModel : INotifyPropertyChanged
    {
        private readonly WPFMessageBox ___View;
        private ICommand ___CancelCommand;
        private Visibility ___CancelVisibility;
        private ICommand ___CloseCommand;
        private Visibility ___CloseVisibility;
        private string ___InnerMessageDetails;
        private string ___Message;
        private ImageSource ___MessageImageSource;
        private ICommand ___NoCommand;
        private ICommand ___OKCommand;
        private Visibility ___OKVisibility;
        private Visibility ___ShowDetails;
        private string ___Title;
        private ICommand ___YesCommand;
        private Visibility ___YesNoVisibility;

        public MessageBoxViewModel(WPFMessageBox view,
                                   string title, string message, string innerMessage,
                                   WPFMessageBoxButtons buttonOption, WPFMessageBoxImage image)
        {
            Title = title;
            Message = message;
            InnerMessageDetails = innerMessage;
            SetButtonVisibility(buttonOption);
            SetImageSource(image);
            ___View = view;
        }

        public string Title
        {
            get { return ___Title; }
            set
            {
                if (___Title != value)
                {
                    ___Title = value;
                    NotifyPropertyChange("Title");
                }
            }
        }

        public string Message
        {
            get { return ___Message; }
            set
            {
                if (___Message != value)
                {
                    ___Message = value;
                    NotifyPropertyChange("Message");
                }
            }
        }

        public string InnerMessageDetails
        {
            get { return ___InnerMessageDetails; }
            set
            {
                if (___InnerMessageDetails != value)
                {
                    ___InnerMessageDetails = value;
                    NotifyPropertyChange("InnerMessageDetails");
                }
            }
        }

        public ImageSource MessageImageSource
        {
            get { return ___MessageImageSource; }
            set
            {
                ___MessageImageSource = value;
                NotifyPropertyChange("MessageImageSource");
            }
        }

        public Visibility YesNoVisibility
        {
            get { return ___YesNoVisibility; }
            set
            {
                if (___YesNoVisibility != value)
                {
                    ___YesNoVisibility = value;
                    NotifyPropertyChange("YesNoVisibility");
                }
            }
        }

        public Visibility CancelVisibility
        {
            get { return ___CancelVisibility; }
            set
            {
                if (___CancelVisibility != value)
                {
                    ___CancelVisibility = value;
                    NotifyPropertyChange("CancelVisibility");
                }
            }
        }

        public Visibility OkVisibility
        {
            get { return ___OKVisibility; }
            set
            {
                if (___OKVisibility != value)
                {
                    ___OKVisibility = value;
                    NotifyPropertyChange("OkVisibility");
                }
            }
        }

        public Visibility CloseVisibility
        {
            get { return ___CloseVisibility; }
            set
            {
                if (___CloseVisibility != value)
                {
                    ___CloseVisibility = value;
                    NotifyPropertyChange("CloseVisibility");
                }
            }
        }

        public Visibility ShowDetails
        {
            get { return ___ShowDetails; }
            set
            {
                if (___ShowDetails != value)
                {
                    ___ShowDetails = value;
                    NotifyPropertyChange("ShowDetails");
                }
            }
        }

        public ICommand YesCommand
        {
            get
            {
                if (___YesCommand == null)
                    ___YesCommand = new DelegateCommand(() =>
                                                            {
                                                                ___View.Result = WPFMessageBoxResult.Yes;
                                                                ___View.Close();
                                                            });
                return ___YesCommand;
            }
        }

        public ICommand NoCommand
        {
            get
            {
                if (___NoCommand == null)
                    ___NoCommand = new DelegateCommand(() =>
                                                           {
                                                               ___View.Result = WPFMessageBoxResult.No;
                                                               ___View.Close();
                                                           });
                return ___NoCommand;
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                if (___CancelCommand == null)
                    ___CancelCommand = new DelegateCommand(() =>
                                                               {
                                                                   ___View.Result = WPFMessageBoxResult.Cancel;
                                                                   ___View.Close();
                                                               });
                return ___CancelCommand;
            }
        }


        public ICommand CloseCommand
        {
            get
            {
                if (___CloseCommand == null)
                    ___CloseCommand = new DelegateCommand(() =>
                                                              {
                                                                  ___View.Result = WPFMessageBoxResult.Close;
                                                                  ___View.Close();
                                                              });
                return ___CloseCommand;
            }
        }

        public ICommand OkCommand
        {
            get
            {
                if (___OKCommand == null)
                    ___OKCommand = new DelegateCommand(() =>
                                                           {
                                                               ___View.Result = WPFMessageBoxResult.Ok;
                                                               ___View.Close();
                                                           });
                return ___OKCommand;
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void NotifyPropertyChange(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private void SetButtonVisibility(WPFMessageBoxButtons buttonOption)
        {
            switch (buttonOption)
            {
                case WPFMessageBoxButtons.YesNo:
                    OkVisibility = CancelVisibility = CloseVisibility = Visibility.Collapsed;
                    break;
                case WPFMessageBoxButtons.YesNoCancel:
                    OkVisibility = CloseVisibility = Visibility.Collapsed;
                    break;
                case WPFMessageBoxButtons.OK:
                    YesNoVisibility = CancelVisibility = CloseVisibility = Visibility.Collapsed;
                    break;
                case WPFMessageBoxButtons.OKClose:
                    YesNoVisibility = CancelVisibility = Visibility.Collapsed;
                    break;
                default:
                    OkVisibility = CancelVisibility = YesNoVisibility = Visibility.Collapsed;
                    break;
            }
            if (string.IsNullOrEmpty(InnerMessageDetails)) ShowDetails = Visibility.Collapsed;
            else ShowDetails = Visibility.Visible;
        }

        private void SetImageSource(WPFMessageBoxImage image)
        {
            string __Source = "pack://application:,,,/WinPure.ContactManagement.Client.CustomMessageBox;component/Images/Default.png";
            switch (image)
            {
                case WPFMessageBoxImage.Alert:
                    __Source = "pack://application:,,,/WinPure.ContactManagement.Client.CustomMessageBox;component/Images/Alert.png";
                    break;
                case WPFMessageBoxImage.Error:
                    __Source = "pack://application:,,,/WinPure.ContactManagement.Client.CustomMessageBox;component/Images/Error.png";
                    break;
                case WPFMessageBoxImage.Information:
                    __Source = "pack://application:,,,/WinPure.ContactManagement.Client.CustomMessageBox;component/Images/Info.png";
                    break;
                case WPFMessageBoxImage.OK:
                    __Source = "pack://application:,,,/WinPure.ContactManagement.Client.CustomMessageBox;component/Images/OK.png";
                    break;
                case WPFMessageBoxImage.Question:
                    __Source = "pack://application:,,,/WinPure.ContactManagement.Client.CustomMessageBox;component/Images/Help.png";
                    break;
                default:
                    __Source = "pack://application:,,,/WinPure.ContactManagement.Client.CustomMessageBox;component/Images/Default.png";
                    break;
            }
            var __ImageUri = new Uri(__Source, UriKind.RelativeOrAbsolute);
            MessageImageSource = new BitmapImage(__ImageUri);
        }
    }
}