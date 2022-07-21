using SortingMethods;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace SortLines
{
    public class DrawLines
    {
        private int[] arr;

        private readonly Canvas canvas;

        public DrawLines(Canvas can)
            => canvas = can;

        public void AddUnsortedLines(int numLines)
        {
            arr = new int[numLines];

            for (int i = 0; i < arr.Length; i++)
                arr[i] = i;

            arr = SelectionSort<int>.Shuffle(arr);

            AddLines(canvas, arr);
        }

        public void AddSortedLines()
        {
            arr = new SelectionSort<int>().Sort(arr);

            AddLines(canvas, arr);
        }

        public static void AddLines(Canvas canvas, int[] arr)
        {
            canvas.Children.Clear();

            for (int i = 0; i < arr.Length; i++)
            {
                var myLine = new Line();

                myLine.Stroke = System.Windows.Media.Brushes.Black;

                myLine.X1 = 2;
                myLine.X2 = arr[i];
                myLine.Y1 = myLine.Y2 = i * 3;

                myLine.StrokeThickness = 1;

                canvas.Children.Add(myLine);
            }
        }
    }
}
