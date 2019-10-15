using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;

namespace Cypher_Decrypt.Classes
{
    class ColumnChart
    {
        private float height;
        private float width;
        private float labelHeight;
        private float colWidth;
        private float spacing;
        private float colMaxHeight;
        private float exacMaxValue;
        private List<Column> columns;
        private Dictionary<String, Column> columnStorage;

        public float Height
        {
            get => height;
            set
            {
                height = value;
            }
        }
        public float Width => width; 
        public float LabelHeight { get => labelHeight; set => labelHeight = value; }
        internal List<Column> Columns => columns;
        public float ColWidth { get => colWidth; set => colWidth = value; }
        public float Spacing { get => spacing; set => spacing = value; }
        public float ColMaxHeight => colMaxHeight;
        public float ExacMaxValue => exacMaxValue;

        public ColumnChart(int height, float labelHeight, float colWidth, float spacing)
        {
            columns = new List<Column>();
            columnStorage = new Dictionary<string, Column>();
            Height = height;
            LabelHeight = labelHeight;
            ColWidth = colWidth;
            Spacing = spacing;
        }
        public bool updateColumnByName(string name, float value)
        {
            Column returnValue = null;
            columnStorage.TryGetValue(name, out returnValue);
            if (returnValue == null)
                return false;
            returnValue.Value = value;
            exacMaxValue = Math.Max(returnValue.Value, exacMaxValue);
            return true;
        }

        public void Draw(Grid chart)
        {
            chart.Children.Clear();
            chart.Width = width = (ColWidth + Spacing) * (columns.Count - 1) + ColWidth;
            chart.Height = Height;
            colMaxHeight = Height - LabelHeight - ColWidth;

            float maxValue = exacMaxValue * 1.2f + 1;

            int run = 0;
            foreach (Column col in columns)
            {
                Brush valueColor = new SolidColorBrush(
                    Color.FromRgb(
                        0,
                        (byte)(120 + col.Value / (exacMaxValue + 1) * 135),
                        (byte)(70 + col.Value / (exacMaxValue + 1) * 40)
                    )
                );

                Brush backgroundColor = new SolidColorBrush(
                    Color.FromRgb(
                        85,
                        85,
                        85
                    )
                );

                Rectangle valueRec = new Rectangle();
                valueRec.Width = ColWidth;
                valueRec.Height = col.Value / maxValue * colMaxHeight + ColWidth / 2;
                valueRec.VerticalAlignment = VerticalAlignment.Top;
                valueRec.HorizontalAlignment = HorizontalAlignment.Left;
                valueRec.Margin = new Thickness(run * (Spacing + ColWidth), colMaxHeight - valueRec.Height + ColWidth, 0, 0);
                valueRec.Fill = valueColor;

                Ellipse valueHat = new Ellipse();
                valueHat.Width = ColWidth;
                valueHat.Height = ColWidth;
                valueHat.VerticalAlignment = VerticalAlignment.Top;
                valueHat.HorizontalAlignment = HorizontalAlignment.Left;
                valueHat.Margin = new Thickness(run * (Spacing + ColWidth), colMaxHeight - valueRec.Height + ColWidth / 2, 0, 0);
                valueHat.Fill = valueColor;

                Rectangle backgroundRec = new Rectangle();
                backgroundRec.Width = ColWidth;
                backgroundRec.Height = colMaxHeight + ColWidth / 2;
                backgroundRec.VerticalAlignment = VerticalAlignment.Top;
                backgroundRec.HorizontalAlignment = HorizontalAlignment.Left;
                backgroundRec.Margin = new Thickness(run * (Spacing + ColWidth), ColWidth / 2, 0, 0);
                backgroundRec.Fill = backgroundColor;

                Ellipse backgroundHat = new Ellipse();
                backgroundHat.Width = ColWidth;
                backgroundHat.Height = ColWidth;
                backgroundHat.VerticalAlignment = VerticalAlignment.Top;
                backgroundHat.HorizontalAlignment = HorizontalAlignment.Left;
                backgroundHat.Margin = new Thickness(run * (Spacing + ColWidth), 0, 0, 0);
                backgroundHat.Fill = backgroundColor;

                Ellipse underDot = new Ellipse();
                underDot.Width = 10;
                underDot.Height = 10;
                underDot.VerticalAlignment = VerticalAlignment.Top;
                underDot.HorizontalAlignment = HorizontalAlignment.Left;
                underDot.Margin = new Thickness(run * (Spacing + ColWidth) + (ColWidth - underDot.Width) / 2, colMaxHeight + ColWidth + 10, 0, 0);
                underDot.Fill = valueColor;

                Label label = new Label();
                label.Content = col.Name;
                label.Foreground = Brushes.White;
                label.FontFamily = Application.Current.Resources["Quicksand-Bold"] as FontFamily;
                label.FontSize = 18;
                label.FontWeight = FontWeights.Bold;
                label.HorizontalContentAlignment = HorizontalAlignment.Center;
                label.VerticalContentAlignment = VerticalAlignment.Center;
                label.Width = ColWidth;
                label.VerticalAlignment = VerticalAlignment.Top;
                label.HorizontalAlignment = HorizontalAlignment.Left;
                label.Margin = new Thickness(run * (Spacing + ColWidth), colMaxHeight + ColWidth + 30, 0, 0);

                Label value = new Label();
                value.Content = col.Value;
                value.Foreground = Brushes.White;
                value.FontFamily = Application.Current.Resources["Quicksand-Bold"] as FontFamily;
                value.FontSize = 18;
                value.FontWeight = FontWeights.UltraBold;
                value.HorizontalContentAlignment = HorizontalAlignment.Center;
                value.VerticalContentAlignment = VerticalAlignment.Center;
                value.Width = ColWidth;
                value.Height = ColWidth;
                value.VerticalAlignment = VerticalAlignment.Top;
                value.HorizontalAlignment = HorizontalAlignment.Left;
                value.Margin = new Thickness(run * (Spacing + ColWidth), colMaxHeight - valueRec.Height + ColWidth / 2, 0, 0);

                chart.Children.Add(backgroundRec);
                chart.Children.Add(backgroundHat);
                chart.Children.Add(valueRec);
                chart.Children.Add(valueHat);
                chart.Children.Add(underDot);
                chart.Children.Add(label);
                chart.Children.Add(value);

                run++;
            }
        }
        public void Add(Column col)
        {
            exacMaxValue = Math.Max(col.Value, exacMaxValue);
            columnStorage.Add(col.Name, col);
            Columns.Add(col);
        }
        public static void Link(ColumnChart chart1, ColumnChart chart2)
        {
            chart1.exacMaxValue = chart2.exacMaxValue = Math.Max(chart1.exacMaxValue, chart2.exacMaxValue);
        }
    }
}
