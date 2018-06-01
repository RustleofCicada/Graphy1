using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Threading;

using Graphy0_0_2.Parser;
using Graphy0_0_2.Parser.Calculable;

namespace Graphy0_0_2.GUI
{
    /// <summary>
    /// Logika interakcji dla klasy Graph.xaml
    /// </summary>
    public partial class Graph : UserControl
    {
        public Point Origin { get; set; }
        private Point _mouseCoordinates = new Point();
        public Point MouseCoordinates
        {
            get
            {
                return _mouseCoordinates;
            }
            set
            {
                MouseCoordinatesChanged.Invoke(this, new MouseCoordinatesChangedEventArgs(_mouseCoordinates, value));
                _mouseCoordinates = value;
            }
        }
        public Point MouseLocation { get; set; }
        public MovingMode MovingMode = MovingMode.Both;
        
        public double Xmap = 0.01; //pixel * Xmap = x - argument funkcji
        public double Ymap = 0.01; //pixel * Ymap = y - wartość funkcji
        public ScallingMode ScallingMode = ScallingMode.Both;
        public int PixelJump = 3;

        public List<Line> CurrentGraph = new List<Line>();
        public ICalculable CurrentFunction = new Number(0);
        private double Xspacing = 10;
        private double Yspacing = 10;
        private List<Line> GridLines = new List<Line>();
        private SolidColorBrush AxisColor = new SolidColorBrush(Color.FromArgb(255, 147, 175, 175));
        private SolidColorBrush OutAxisColor = new SolidColorBrush(Color.FromArgb(60, 197, 175, 175));
        private List<Label> AxisNumbers = new List<Label>();
        public bool DrawingGrid = true;

        public EventHandler<MouseCoordinatesChangedEventArgs> MouseCoordinatesChanged;
        public DispatcherTimer OriginFloatTimer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 1000 / 60) };
        public Point OriginOffset = new Point(0, 0);
        private double FloatDrag = 0.92;
        public bool FloatAnimation = false;

        public bool ShowingDetails = false;
        private Ellipse DetailCircle = new Ellipse() { Width = 4, Height = 4, Stroke = Brushes.AliceBlue, StrokeThickness = 1 };
        private Label DetailLabel = new Label() { FontSize = 9, Foreground = Brushes.AliceBlue, Background = new SolidColorBrush(Color.FromArgb(20, 220, 220, 240)) };

        public Graph()
        {
            InitializeComponent();
        }

        public void InitialiseGraph()
        {
            foreach (Line line in CurrentGraph)
            {
                DrawingArea.Children.Remove(line);
            }
            CurrentGraph.Clear();
            for (int px = 0; px < DrawingArea.ActualWidth; px += PixelJump)
            {
                CurrentGraph.Add(new Line()
                {
                    X1 = px,
                    Y1 = FromCartesian(new Point(0, 0)).Y,
                    X2 = px + PixelJump,
                    Y2 = FromCartesian(new Point(0, 0)).Y,
                    Stroke = Brushes.White,
                    StrokeThickness = 1
                });
            }
            foreach (Line line in CurrentGraph)
            {
                DrawingArea.Children.Add(line);
            }
            ShowDetails();
        }
        public void RenderGraph()
        {
            for (int i = 0; i < CurrentGraph.Count; i++)
            {
                try
                {
                    VariableDictionary.Content["x"] = ToCartesian(new Point(i * PixelJump, 0)).X;
                    CurrentGraph[i].Y1 = FromCartesian(new Point(0, CurrentFunction.Value)).Y;
                    VariableDictionary.Content["x"] = ToCartesian(new Point((i + 1) * PixelJump, 0)).X;
                    CurrentGraph[i].Y2 = FromCartesian(new Point(0, CurrentFunction.Value)).Y;
                }
                catch
                {
                    CurrentGraph[i].Y1 = -1;
                    CurrentGraph[i].Y2 = -1;
                }
            }
            if (ShowingDetails) ShowDetails();
        }
        private void UpdateAxis()
        {
            Xaxis.X1 = DrawingArea.ActualWidth;
            if (Origin.Y <= 1)
            {
                Xaxis.Y1 = 1;
                Xaxis.Y2 = 1;
                Xaxis.Stroke = OutAxisColor;
            }
            else if (Origin.Y >= DrawingArea.ActualHeight - 1)
            {
                Xaxis.Y1 = DrawingArea.ActualHeight - 1;
                Xaxis.Y2 = DrawingArea.ActualHeight - 1;
                Xaxis.Stroke = OutAxisColor;
            }
            else
            {
                Xaxis.Y1 = Origin.Y;
                Xaxis.Y2 = Origin.Y;
                Xaxis.Stroke = AxisColor;
            }

            Yaxis.Y1 = DrawingArea.ActualHeight;
            if (Origin.X <= 1)
            {
                Yaxis.X1 = 1;
                Yaxis.X2 = 1;
                Yaxis.Stroke = OutAxisColor;
            }
            else if (Origin.X >= DrawingArea.ActualWidth - 1)
            {
                Yaxis.X1 = DrawingArea.ActualWidth - 1;
                Yaxis.X2 = DrawingArea.ActualWidth - 1;
                Yaxis.Stroke = OutAxisColor;
            }
            else
            {
                Yaxis.X1 = Origin.X;
                Yaxis.X2 = Origin.X;
                Yaxis.Stroke = AxisColor;
            }
        }
        public void DrawGrid()
        {
            if (DrawingGrid)
            {
                List<double> Xpoints = new List<double>();
                List<double> Ypoints = new List<double>();
                Point UpLeft = ToCartesian(new Point(0, 0));
                Point DownRight = ToCartesian(new Point(DrawingArea.ActualWidth, DrawingArea.ActualHeight));
                double Xlenght = DownRight.X - UpLeft.X;
                double Ylenght = UpLeft.Y - DownRight.Y;

                Xspacing = Xmap * ((DrawingArea.ActualWidth + DrawingArea.ActualHeight) / 15);
                Yspacing = Ymap * ((DrawingArea.ActualWidth + DrawingArea.ActualHeight) / 15);
                bool Xrev = false;
                bool Yrev = false;
                if (Xspacing < 1)
                {
                    Xrev = true;
                    Xspacing = 1 / Xspacing;
                }
                if (Yspacing < 1)
                {
                    Yrev = true;
                    Yspacing = 1 / Yspacing;
                }
                int Xzeros = (int)Math.Log10(Xspacing);
                int Yzeros = (int)Math.Log10(Yspacing);
                Xspacing /= Math.Pow(10, Xzeros);
                Xspacing = (int)Xspacing;
                Xspacing *= Math.Pow(10, Xzeros);

                Yspacing /= Math.Pow(10, Yzeros);
                Yspacing = (int)Yspacing;
                Yspacing *= Math.Pow(10, Yzeros);

                if (Xrev)
                {
                    Xspacing = 1 / Xspacing;
                }
                if (Yrev)
                {
                    Yspacing = 1 / Yspacing;
                }

                for (double x = Xspacing; x < DownRight.X; x += Xspacing)
                {
                    Xpoints.Add(x);
                }
                for (double x = -Xspacing; x > UpLeft.X; x -= Xspacing)
                {
                    Xpoints.Add(x);
                }
                Xpoints.Sort();
                for (double y = Yspacing; y < UpLeft.Y; y += Yspacing)
                {
                    Ypoints.Add(y);
                }
                for (double y = -Yspacing; y > DownRight.Y; y -= Yspacing)
                {
                    Ypoints.Add(y);
                }
                Ypoints.Sort();


                foreach (Line line in GridLines)
                {
                    DrawingArea.Children.Remove(line);
                }
                GridLines.Clear();
                for (int i = 0; i < Xpoints.Count; i++)
                {
                    GridLines.Add(new Line()
                    {
                        X1 = Xpoints[i] / Xmap + Origin.X,
                        X2 = Xpoints[i] / Xmap + Origin.X,
                        Y1 = 0,
                        Y2 = DrawingArea.ActualHeight,
                        StrokeThickness = 1,
                        Stroke = Brushes.LightSkyBlue,
                        Opacity = 0.3
                    });
                }
                for (int i = 0; i < Ypoints.Count; i++)
                {
                    GridLines.Add(new Line()
                    {
                        X1 = 0,
                        X2 = DrawingArea.ActualWidth,
                        Y1 = Origin.Y - Ypoints[i] / Ymap,
                        Y2 = Origin.Y - Ypoints[i] / Ymap,
                        StrokeThickness = 1,
                        Stroke = Brushes.LightSkyBlue,
                        Opacity = 0.3
                    });
                }
                foreach (Line line in GridLines)
                {
                    DrawingArea.Children.Add(line);
                }
                DrawNumbers(Xpoints, Ypoints);
            }
        }
        private void DrawNumbers(List<double> Xpoints, List<double> Ypoints)
        {
            foreach (Label lbl in AxisNumbers)
            {
                DrawingArea.Children.Remove(lbl);
            }
            AxisNumbers.Clear();

            foreach (double x in Xpoints)
            {
                Point pos = new Point((x / Xmap) + Origin.X, Origin.Y);
                if (pos.Y < 0) pos = new Point(pos.X, 0);
                else if (pos.Y > DrawingArea.ActualHeight - 13) pos = new Point(pos.X, DrawingArea.ActualHeight - 13);
                Label toAdd = new Label()
                {
                    Content = x.ToString("0.####"),
                    Foreground = Brushes.LightSkyBlue,
                    FontSize = 8,
                    Opacity = 0.7
                };
                toAdd.SetValue(Canvas.LeftProperty, pos.X - 3);
                toAdd.SetValue(Canvas.TopProperty, pos.Y - 5);
                AxisNumbers.Add(toAdd);
            }
            foreach (double y in Ypoints)
            {
                Point pos = new Point(Origin.X, Origin.Y - (y / Ymap));
                if (pos.X < 0) pos = new Point(0, pos.Y);
                else if (pos.X > DrawingArea.ActualWidth - 13) pos = new Point(DrawingArea.ActualWidth - 10, pos.Y);
                Label toAdd = new Label()
                {
                    Content = y.ToString("0.####"),
                    Foreground = Brushes.LightSkyBlue,
                    FontSize = 8,
                    Opacity = 0.7
                };
                toAdd.SetValue(Canvas.LeftProperty, pos.X - 3);
                toAdd.SetValue(Canvas.TopProperty, pos.Y - 5);
                AxisNumbers.Add(toAdd);
            }

            foreach (Label lbl in AxisNumbers)
            {
                DrawingArea.Children.Add(lbl);
            }
        }
        public void ClearGrid()
        {
            foreach (Line line in GridLines)
            {
                DrawingArea.Children.Remove(line);
            }
            GridLines.Clear();
            foreach (Label lbl in AxisNumbers)
            {
                DrawingArea.Children.Remove(lbl);
            }
            AxisNumbers.Clear();
        }
        public void ShowDetails()
        {
            if (ShowingDetails)
            {
                DetailCircle.Opacity = 0.8;
                DetailLabel.Opacity = 0.8;
                VariableDictionary.Content["x"] = MouseCoordinates.X;
                Point pos = new Point(MouseLocation.X, Origin.Y - CurrentFunction.Value / Ymap);
                DetailLabel.Content = "X: " + MouseCoordinates.X.ToString("0.0000") + "; Y: " + CurrentFunction.Value.ToString("0.0000");
                if (pos.X < 0) pos = new Point(0, pos.Y);
                if (pos.X + DetailLabel.ActualWidth > DrawingArea.ActualWidth)
                {
                    pos = new Point(DrawingArea.ActualWidth - DetailLabel.ActualWidth, pos.Y);
                }
                else
                {
                    DetailLabel.SetValue(Canvas.LeftProperty, pos.X);
                }
                if (pos.Y < 0) pos = new Point(pos.X, 0);
                if (pos.Y + DetailLabel.ActualHeight > DrawingArea.ActualHeight)
                {
                    pos = new Point(pos.X, DrawingArea.ActualHeight - DetailLabel.ActualHeight);
                }
                else
                {
                    DetailLabel.SetValue(Canvas.TopProperty, pos.Y);
                }
                DetailCircle.SetValue(Canvas.LeftProperty, MouseLocation.X - DetailCircle.Width / 2);
                DetailCircle.SetValue(Canvas.TopProperty, Origin.Y - CurrentFunction.Value / Ymap - DetailCircle.Height / 2);
            }
            else
            {
                DetailCircle.Opacity = 0;
                DetailLabel.Opacity = 0;
            }
        }

        public void DrawCartesian()
        {
            UpdateAxis();
            RenderGraph();
        }
        private Point ToCartesian(Point onScreen)
        {
            return new Point((onScreen.X - Origin.X) * Xmap, (Origin.Y - onScreen.Y) * Ymap);
        }
        private Point FromCartesian(Point cartesian)
        {
            return new Point((cartesian.X / Xmap) - Origin.X, Origin.Y - (cartesian.Y / Ymap));
        }

        private void DrawingArea_Loaded(object sender, RoutedEventArgs e)
        {
            Origin = new Point(DrawingArea.ActualWidth / 2, DrawingArea.ActualHeight / 2);
            OriginFloatTimer.Tick += OriginFloatTimer_Tick;
            DrawingArea.Children.Add(DetailCircle);
            DrawingArea.Children.Add(DetailLabel);
            InitialiseGraph();
            DrawCartesian();
            DrawGrid();
        }
        private void DrawingArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseLocation == null)
                MouseLocation = e.GetPosition(DrawingArea);

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                OriginOffset = Origin;
                Point newOrigin = new Point(Origin.X - MouseLocation.X + e.GetPosition(DrawingArea).X,
                                            Origin.Y - MouseLocation.Y + e.GetPosition(DrawingArea).Y);
                if (MovingMode == MovingMode.X) newOrigin = new Point(newOrigin.X, Origin.Y);
                else if (MovingMode == MovingMode.Y) newOrigin = new Point(Origin.X, newOrigin.Y);
                Origin = newOrigin;
                OriginOffset = new Point(Origin.X - OriginOffset.X,
                                         Origin.Y - OriginOffset.Y);
                DrawCartesian();
                DrawGrid();
            }

            MouseLocation = e.GetPosition(DrawingArea);
            MouseCoordinates = ToCartesian(MouseLocation);
        }
        private void DrawingArea_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            int direction = -Math.Sign(e.Delta);
            Point Offset = new Point(MouseLocation.X - Origin.X, MouseLocation.Y - Origin.Y);
            double XmapChange = 0;
            double YmapChange = 0;
            if (ScallingMode != ScallingMode.Y)
            {
                XmapChange = Xmap * direction * 0.1;
                Xmap += XmapChange;
            }
            if (ScallingMode != ScallingMode.X)
            {
                YmapChange = Ymap * direction * 0.1;
                Ymap += YmapChange;
            }
            Origin = new Point(Origin.X - Xmap * Offset.X, Origin.Y - Ymap * Offset.Y);

            MouseCoordinates = ToCartesian(MouseLocation);
            DrawCartesian();
            DrawGrid();
        }
        private void DrawingArea_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (IsLoaded)
            {
                double Xgrowth = e.NewSize.Width / e.PreviousSize.Width;
                double Ygrowth = e.NewSize.Height / e.PreviousSize.Height;
                Origin = new Point(Xgrowth * Origin.X,
                                   Ygrowth * Origin.Y);
                Xmap /= Math.Min(Xgrowth, Ygrowth);
                Ymap /= Math.Min(Xgrowth, Ygrowth);


                MouseCoordinates = ToCartesian(MouseLocation);
                InitialiseGraph();
                DrawCartesian();
                DrawGrid();
            }
        }
        private void DrawingArea_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (FloatAnimation)
            {
                OriginFloatTimer.IsEnabled = true;
                ClearGrid();
            }
        }
        private void DrawingArea_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (FloatAnimation)
            {
                OriginOffset = new Point(0, 0);
                OriginFloatTimer.IsEnabled = false;
            }
        }

        private void OriginFloatTimer_Tick(object sender, EventArgs e)
        {
            if (Math.Abs(OriginOffset.X) < 0.3 && Math.Abs(OriginOffset.Y) < 0.3)
            {
                OriginFloatTimer.IsEnabled = false;
                OriginOffset = new Point(0, 0);
                DrawGrid();
                return;
            }
            else
            {
                Origin = new Point(Origin.X + OriginOffset.X,
                                   Origin.Y + OriginOffset.Y);
                OriginOffset = new Point(OriginOffset.X * FloatDrag, OriginOffset.Y * FloatDrag);
                MouseCoordinates = ToCartesian(MouseLocation);
                DrawCartesian();
            }
        }
    }

    public class MouseCoordinatesChangedEventArgs : EventArgs
    {
        public Point Before { get; set; }
        public Point After { get; set; }

        public MouseCoordinatesChangedEventArgs(Point before, Point after)
        {
            Before = before;
            After = after;
        }
    }
}
