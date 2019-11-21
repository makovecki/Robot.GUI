using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UICore.Buttons.Controls
{
    public class ButtonIconControl:Button
    {
        public ButtonIconControl()
        {
            this.DefaultStyleKey = typeof(ButtonIconControl);
        }



        public double EllipseDiameter
        {
            get { return (double)GetValue(EllipseDiameterProperty); }
            set { SetValue(EllipseDiameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EllipseDiameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EllipseDiameterProperty =
            DependencyProperty.Register("EllipseDiameter", typeof(double), typeof(ButtonIconControl), new PropertyMetadata(12d));



        public double EllipseStrokeThickness
        {
            get { return (double)GetValue(EllipseStrokeThicknessProperty); }
            set { SetValue(EllipseStrokeThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EllipseStrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EllipseStrokeThicknessProperty =
            DependencyProperty.Register("EllipseStrokeThickness", typeof(double), typeof(ButtonIconControl), new PropertyMetadata(1d));



        public Geometry IconData
        {
            get { return (Geometry)GetValue(IconDataProperty); }
            set { SetValue(IconDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconDataProperty =
            DependencyProperty.Register("IconData", typeof(Geometry), typeof(ButtonIconControl), new PropertyMetadata(null));




        public double IconHeight
        {
            get { return (double)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.Register("IconHeight", typeof(double), typeof(ButtonIconControl), new PropertyMetadata(12d));



        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(double), typeof(ButtonIconControl), new PropertyMetadata(12d));




    }
}
