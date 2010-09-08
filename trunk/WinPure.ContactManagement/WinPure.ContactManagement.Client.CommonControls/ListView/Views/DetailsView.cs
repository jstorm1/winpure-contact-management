using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WinPure.ContactManagement.Client.CommonControls.ListView.Views
{
    /// <summary>
    /// Custom Details View for the <see cref="ListView"/> control.
    /// 
    /// ImageView displays image files using themselves as their icons.
    /// In order to write our own visual tree of a view, we should override its
    /// DefaultStyleKey and ItemContainerDefaultKey. DefaultStyleKey specifies
    /// the style key of <see cref="ListView"/>; ItemContainerDefaultKey specifies the style
    /// key of <see cref="ListViewItem"/>.
    /// </summary>
    public class DetailsView:ViewBase
    {
        #region DefaultStyleKey

        protected override object DefaultStyleKey
        {
            get { return new ComponentResourceKey(GetType(), "ImageView"); }
        }

        #endregion

        #region ItemContainerDefaultStyleKey

        protected override object ItemContainerDefaultStyleKey
        {
            get { return new ComponentResourceKey(GetType(), "ImageViewItem"); }
        }

        #endregion

    }
}
