using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace UICore.Buttons.Controls
{
    public class ButtonsControl : Control
    {
        public ButtonsControl()
        {
            DefaultStyleKey = typeof(ButtonsControl);
        }



        public ObservableCollection<ButtonViewModel> Buttons
        {
            get { return (ObservableCollection<ButtonViewModel>)GetValue(ButtonsProperty); }
            set { SetValue(ButtonsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Buttons.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonsProperty =
            DependencyProperty.Register("Buttons", typeof(ObservableCollection<ButtonViewModel>), typeof(ButtonsControl), new PropertyMetadata(null));


    }
}
