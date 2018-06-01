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
using System.Windows.Shapes;

using Graphy0_0_2.Parser;
using Graphy0_0_2.Parser.Calculable;

namespace Graphy0_0_2.GUI
{
    /// <summary>
    /// Logika interakcji dla klasy GraphyWindow.xaml
    /// </summary>
    public partial class GraphyWindow : Window
    {
        ICalculable currentFunc;
        HashSet<Key> KeysPressed = new HashSet<Key>();

        public GraphyWindow()
        {
            InitializeComponent();

            VariableDictionary.Content.Add("x", 0);
        }

        private void FuncInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                currentFunc = ExpressionParser.FromText(FuncInput.Text);
                FuncInput.Foreground = Brushes.AntiqueWhite;
                Graph.CurrentFunction = currentFunc;
                Graph.DrawCartesian();
            }
            catch
            {
                FuncInput.Foreground = new SolidColorBrush(Color.FromArgb(255, 230, 150, 150));
            }
        }

        private void MouseLocationInfo_Loaded(object sender, RoutedEventArgs e)
        {
            Graph.MouseCoordinatesChanged += MouseCoorinates_Changed;
            Graph.MouseLeave += MouseLeaveOfDrawingArea;
        }
        private void MouseCoorinates_Changed(object sender, MouseCoordinatesChangedEventArgs e)
        {
            MouseLocationInfo.Content = "X: " + e.After.X.ToString("0.0000") + "; Y: " + e.After.Y.ToString("0.0000");
        }
        private void MouseLeaveOfDrawingArea(object sender, MouseEventArgs e)
        {
            MouseLocationInfo.Content = "X: -; Y: -";
        }

        private void ScallingBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (KeysPressed.Contains(Key.LeftShift))
                {
                    Graph.Xmap = 0.01;
                    Graph.Ymap = 0.01;
                    Graph.DrawCartesian();
                    Graph.DrawGrid();
                }
                else
                {
                    switch (Graph.ScallingMode)
                    {
                        case ScallingMode.Both:
                            Graph.ScallingMode = ScallingMode.X;
                            ScallingBtn.Content = "→";
                            break;
                        case ScallingMode.X:
                            Graph.ScallingMode = ScallingMode.Y;
                            ScallingBtn.Content = "↑";
                            break;
                        case ScallingMode.Y:
                            Graph.ScallingMode = ScallingMode.Both;
                            ScallingBtn.Content = "∟";
                            break;
                    }
                }
            }
        }

        private void FlowBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                Graph.FloatAnimation = !Graph.FloatAnimation;
                FlowBtn.Content = "☼";
                if (Graph.FloatAnimation)
                {
                    FlowBtn.Content = "~";
                }
            }
        }

        private void MovingBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                if (KeysPressed.Contains(Key.LeftShift))
                {
                    Graph.Origin = new Point(Graph.DrawingArea.ActualWidth / 2, Graph.DrawingArea.ActualHeight / 2);
                    Graph.DrawCartesian();
                    Graph.DrawGrid();
                }
                else
                {
                    switch (Graph.MovingMode)
                    {
                        case MovingMode.Both:
                            Graph.MovingMode = MovingMode.X;
                            MovingBtn.Content = "►";
                            break;
                        case MovingMode.X:
                            Graph.MovingMode = MovingMode.Y;
                            MovingBtn.Content = "▲";
                            break;
                        case MovingMode.Y:
                            Graph.MovingMode = MovingMode.Both;
                            MovingBtn.Content = "+";
                            break;
                    }
                }
            }
        }

        private void PixelJumpSld_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (IsLoaded)
            {
                Graph.PixelJump = (int)PixelJumpSld.Value;
                Graph.InitialiseGraph();
                Graph.DrawCartesian();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            KeysPressed.Add(e.Key);
            if (KeysPressed.Contains(Key.LeftCtrl) || KeysPressed.Contains(Key.LeftShift))
            {
                if (KeysPressed.Contains(Key.LeftCtrl))
                {
                    Graph.ShowingDetails = true;
                    Graph.ShowDetails();
                }
                switch (e.Key)
                {
                    case Key.M:
                        MovingBtn_Click(null, null);
                        break;
                    case Key.S:
                        ScallingBtn_Click(null, null);
                        break;
                    case Key.G:
                        GridBtn_Click(null, null);
                        break;
                    case Key.F:
                        FlowBtn_Click(null, null);
                        break;
                    case Key.Q:
                        PixelJumpSld.Value += 1;
                        break;
                    case Key.W:
                        PixelJumpSld.Value -= 1;
                        break;
                }
            }
            
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            KeysPressed.Remove(e.Key);
            switch (e.Key)
            {
                case Key.LeftCtrl:
                    Graph.ShowingDetails = false;
                    Graph.ShowDetails();
                    break;
            }
        }

        private void GridBtn_Click(object sender, RoutedEventArgs e)
        {
            Graph.DrawingGrid = !Graph.DrawingGrid;
            if (Graph.DrawingGrid)
            {
                GridBtn.Content = "ON";
                Graph.DrawGrid();
            }
            else
            {
                GridBtn.Content = "OFF";
                Graph.ClearGrid();
            }
        }
    }
}

#region Shortcuts
/*
 Ctrl           - Detale
 Shift + M      - Origin na 0 
 Shift + S      - Mapy na 0
 (Ctrl) + M     - zmiana trybu poruszania
 (Ctrl) + S     - zmiana trybu skalowania
 (Ctrl) + G     - ON/OFF siatka
 (Ctrl) + F     - ON/OFF latanie
 (Ctrl) + Q     - Zmniejszenie jakości
 (Ctrl) + W     - Poprawa jakości
 */
#endregion
