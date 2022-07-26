﻿using SortingMethods;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SortLines
{
    public class DrawLines : ISortingObserver<int>
    {
        private int[] array;

        private readonly Canvas canvas;

        private readonly DispatcherTimer timer = new DispatcherTimer();

        public DrawLines(Canvas can)
        {
            canvas = can;

            timer.Interval = TimeSpan.FromMilliseconds(50);

            timer.Tick += Timer_Tick;

            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                AddLines(canvas, array);
            }
            catch
            {

            }
        }

        public void AddUnsortedLines(int numLines)
        {
            array = new int[numLines];

            for (int i = 0; i < array.Length; i++)
                array[i] = i;

            array = SelectionSort<int>.Shuffle(array);

            AddLines(canvas, array);
        }

        async public void AddSortedLinesAsync()
        {
            await Task.Run(() =>
                {
                    try
                    {
                        for (int i = 0; i < 111111; i++)
                        {
                            array = new int[100];

                            for (int j = 0; j < array.Length; j++)
                                array[j] = j;

                            array = SelectionSort<int>.Shuffle(array);

                            //AddLines(canvas, array);

                            SortingMethod<int> sorting = null;

                            int mod = i % 4;

                            if (mod == 0)
                                sorting = new BubbleSort<int>();

                            if (mod == 1)
                                sorting = new SelectionSort<int>();

                            if (mod == 2)
                                sorting = new InsertionSort<int>();

                            if (mod == 3)
                                sorting = new QuickSort<int>();

                            sorting.AddObserver(this);

                            array = sorting.Sort(array);
                        }
                    }
                    catch(Exception exception)
                    {

                    }
                });

            AddLines(canvas, array);
        }

        public static void AddLines(Canvas canvas, int[] arr)
        {
            canvas.Children.Clear();

            for (int i = 0; i < (arr?.Length).GetValueOrDefault(); i++)
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

        public void Swapped(int[] arr)
        {
            //canvas.Dispatcher.BeginInvoke(new DrawLinesCallback(AddLines), canvas, arr);
            array = arr;
        }

        public delegate void DrawLinesCallback(Canvas canvas, int[] arr);
    }
}
