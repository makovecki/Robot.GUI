using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Robot.UI.ESPController.Joystick
{
    /// <summary>
    /// Interaction logic for JoystickControl.xaml
    /// </summary>
    public partial class JoystickControl : UserControl
    {
        private (double x, double y) normalizedPosition;
        public JoystickControl()
        {
            InitializeComponent();
        }

        public double SpeedLeft
        {
            get { return (double)GetValue(SpeedLeftProperty); }
            set { SetValue(SpeedLeftProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SpeedLeft.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpeedLeftProperty =
            DependencyProperty.Register("SpeedLeft", typeof(double), typeof(JoystickControl), new PropertyMetadata(0d));

        public double SpeedRight
        {
            get { return (double)GetValue(SpeedRightProperty); }
            set { SetValue(SpeedRightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SpeedRight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SpeedRightProperty =
            DependencyProperty.Register("SpeedRight", typeof(double), typeof(JoystickControl), new PropertyMetadata(0d));


        private (double x, double y) GetKnobNormalizedPosition(Point point)
        {
            double fJoystickRadius = MoveArea.Height * 0.5;
            Vector vtJoystickPos = point - new Point(fJoystickRadius, fJoystickRadius);
            vtJoystickPos /= fJoystickRadius;
            if (vtJoystickPos.Length > 1.0) vtJoystickPos.Normalize();
            return (vtJoystickPos.X, -vtJoystickPos.Y);
        }

        private double GetKnobRotateAngle()
        {
            var angle = (Math.Atan2(normalizedPosition.x, normalizedPosition.y) * (180 / Math.PI));
            //if (normalizedPosition.x < 0) angle = (Math.Atan2(normalizedPosition.y, normalizedPosition.x) * (180 / Math.PI));
            angle = (angle + 360) % 360;
            if (angle > 90 && angle < 270) angle = angle - 180;
            return angle;
        }
        private double GetKnobAngle()
        {
            var angle = (Math.Atan2(normalizedPosition.x, normalizedPosition.y) * (180 / Math.PI));
            if (normalizedPosition.x < 0) angle = (Math.Atan2(normalizedPosition.y, normalizedPosition.x) * (180 / Math.PI));
            if (normalizedPosition.x > 0 && normalizedPosition.y<0) angle = 360-angle;
            angle = (angle + 360) % 360;
            return angle;
        }

        private void Ellipse_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                normalizedPosition = GetKnobNormalizedPosition(e.GetPosition(MoveArea));
                UpdateKnobPosition();
            }
        }

        void UpdateKnobPosition()
        {
            double fJoystickRadius = MoveArea.Height * 0.5;
            double fKnobRadius = Knob.Width * 0.5;
            Canvas.SetLeft(Knob, Canvas.GetLeft(MoveArea) + normalizedPosition.x * fJoystickRadius + fJoystickRadius - fKnobRadius);
            Canvas.SetTop(Knob, Canvas.GetTop(MoveArea) - normalizedPosition.y * fJoystickRadius + fJoystickRadius - fKnobRadius);

            Knob.RenderTransform = new RotateTransform(GetKnobRotateAngle());
            if (normalizedPosition.y >= 0) Forward.Visibility = Visibility.Visible; else Forward.Visibility = Visibility.Hidden;
            if (normalizedPosition.y < 0) Backward.Visibility = Visibility.Visible; else Backward.Visibility = Visibility.Hidden;
            var (left, right) = CalculateMotorSpeed();
            SpeedLeft = left;
            SpeedRight = right;
        }

        private (double left, double right) CalculateMotorSpeed()
        {
            var radius = MoveArea.Height * 0.5;
            var distance = Math.Abs(Point.Subtract(new Point(0, 0), new Point(normalizedPosition.x, normalizedPosition.y)).Length)*radius;
            var power = distance / radius;
            if (normalizedPosition.x >= 0 && normalizedPosition.y >= 0) return ( power*1,power*(1-GetKnobAngle()/90));
            if (normalizedPosition.x < 0 && normalizedPosition.y >= 0) return (power*(180-GetKnobAngle())/90,power*1);
            if (normalizedPosition.x < 0 && normalizedPosition.y < 0) return (-power*( GetKnobAngle()-180) / 90, -1*power);
            if (normalizedPosition.x >= 0 && normalizedPosition.y <= 0) return ( -1*power,power*(GetKnobAngle() - 270) / 90);
            return (0, 0);
        }
        private void ResizeMoveArea()
        {
            var diameter = Math.Min(LayoutRoot.ActualHeight, LayoutRoot.ActualWidth);
            MoveArea.Width = diameter;
            MoveArea.Height = diameter;

            Canvas.SetLeft(MoveArea, (LayoutRoot.ActualWidth - diameter) / 2);
            Canvas.SetTop(MoveArea, (LayoutRoot.ActualHeight - diameter) / 2);

            AxeX.Width = diameter;
            AxeY.Height = diameter;
        }

        private void LayoutRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResizeMoveArea();
            ReturnKnobToCenter();
        }

        private void ReturnKnobToCenter()
        {
            normalizedPosition = (0, 0);
            UpdateKnobPosition();
        }
        //private async void GoToCenterAsync()
        //{


        //    var XFrom = Canvas.GetLeft(Knob);
        //    var YFrom = Canvas.GetTop(Knob);
        //    var XToGoTo = (width - Knob.ActualWidth) / 2;
        //    var YToGoTo = (height - Knob.ActualHeight) / 2;
        //    var p1 = new Point(XFrom, YFrom);
        //    var p2 = new Point(XToGoTo, YToGoTo);

        //    var distance = Math.Abs(Point.Subtract(p1,p2).Length);
        //    var newPoint = p1;
        //    var decelerator = 7.0;
        //    while (distance>1)
        //    {
        //        if (distance < 20) decelerator = 1;
        //        newPoint = MovePointTowards(newPoint, p2, decelerator);
        //        distance = Math.Abs(Point.Subtract(newPoint, p2).Length);
        //        Canvas.SetLeft(Knob, newPoint.X);
        //        Canvas.SetTop(Knob, newPoint.Y);
        //        _x = newPoint.X;
        //        _y = newPoint.Y;
        //        HandeDirection();
        //        await Task.Delay(12);
        //    }

        //    Canvas.SetLeft(Knob,XToGoTo );
        //    Canvas.SetTop(Knob,YToGoTo );
        //    _x = 0;
        //    _y = 0;
        //    HandeDirection();
        //    AxeX.Width = Joystick.Width;
        //    AxeY.Height = Joystick.Height;



        //}
        public Point MovePointTowards(Point a, Point b, double distance)
        {
            var vector = new Point(b.X - a.X, b.Y - a.Y);
            var length = Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            var unitVector = new Point(vector.X / length, vector.Y / length);
            return new Point(a.X + unitVector.X * distance, a.Y + unitVector.Y * distance);
        }
        private void Ellipse_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ReturnKnobToCenter();
        }
    }
}
