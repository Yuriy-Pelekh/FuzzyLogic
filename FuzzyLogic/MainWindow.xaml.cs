using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace FuzzyLogic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var sw = new Stopwatch();
            sw.Start();

            var r = Euler();

            sw.Stop();

            Title += string.Format(". Execution time: {0}", sw.Elapsed);

            var res = r.Select(
                e => new EulerResult {Step = e.Key, X = e.Value[0], A = e.Value[1], X0 = e.Value[2], A0 = e.Value[3]}).
                ToArray();

            dataGrid.ItemsSource = res;
        }

        public Dictionary<double, double[]> Euler()
        {
            var res = new Dictionary<double, double[]>();

            const double a = 0;
            const double b = 10;
            const int iterationsCount = 200;//10000;
            const double step = 0.0078;//(b - a)/iterationsCount;

            var t = a;
            var w = new double[] {0, 0, 0, 0};

            for (var iCount = 1; iCount <= iterationsCount; iCount++)
            {
                var temp = Tap(t, w);
                w[0] += temp[0]*step;
                w[1] += temp[1]*step;
                w[2] += temp[2]*step;
                w[3] += temp[3]*step;

                t = a + (iCount*step);

                res.Add(t, new[] {w[0], w[1], w[2], w[3]});
            }

            return res;
        }

        public double[] Tap(double t, double[] w)
        {
            var matrix = new[,]
                             {
                                 {0, 0, 1, 0},
                                 {0, 0, 0, 1},
                                 {0, 1.5216, -11.6513, 0.0049},
                                 {0, -26.1093, 26.8458, -0.0841}
                             };

            var vector = new[] {0, 0, 1.5304, -3.5261};

            var um = Um(w[0], w[1], t);

            var v0 = matrix[0, 0]*w[0] + matrix[0, 1]*w[1] + matrix[0, 2]*w[2] + matrix[0, 3]*w[3];
            var v1 = matrix[1, 0]*w[0] + matrix[1, 1]*w[1] + matrix[1, 2]*w[2] + matrix[1, 3]*w[3];
            var v2 = matrix[2, 0]*w[0] + matrix[2, 1]*w[1] + matrix[2, 2]*w[2] + matrix[2, 3]*w[3];
            var v3 = matrix[3, 0]*w[0] + matrix[3, 1]*w[1] + matrix[3, 2]*w[2] + matrix[3, 3]*w[3];
            
            var res = new[] {v0 + vector[0]*um, v1 + vector[1]*um, v2 + vector[2]*um, v3 + vector[3]*um};

            return res;
        }

        public double Um(double x1, double x2, double t)
        {
            return t;
        }
    }
}
