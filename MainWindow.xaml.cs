using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace kazislave
{
    public partial class MainWindow : Window
    {
        private Random random;
        private List<int> history;
        private double currentRotation;

        public MainWindow()
        {
            InitializeComponent();
            random = new Random();
            history = new List<int>();
        }

        private void Spin_Click(object sender, RoutedEventArgs e)
        {
            int number = random.Next(0, 37);
            history.Add(number);
            UpdateHistoryListView();

            double targetRotation = currentRotation + random.Next(720, 1440); // Random rotation between 720 and 1440 degrees
            RotateWheel(targetRotation);
        }

        private void RotateWheel(double targetRotation)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = currentRotation,
                To = targetRotation,
                Duration = TimeSpan.FromSeconds(3), // Duration of rotation animation
                FillBehavior = FillBehavior.Stop
            };

            animation.Completed += (s, e) => currentRotation = targetRotation % 360;
            arrow.RenderTransform = new RotateTransform();
            arrow.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, animation);
        }

        private void UpdateHistoryListView()
        {
            historyListView.Items.Clear();
            for (int i = history.Count - 1; i >= 0; i--)
            {
                historyListView.Items.Add(history[i]);
            }
        }
    }
}