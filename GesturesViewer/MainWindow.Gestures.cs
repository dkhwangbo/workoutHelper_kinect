using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using Kinect.Toolbox;
using Microsoft.Kinect;

namespace GesturesViewer
{
    partial class MainWindow
    {
        void LoadCircleGestureDetector()
        {
            using (Stream recordStream = File.Open(circleKBPath, FileMode.OpenOrCreate))
            {
                circleGestureRecognizer = new TemplatedGestureDetector("안녕하세요", recordStream);
                circleGestureRecognizer.DisplayCanvas = gesturesCanvas;
                circleGestureRecognizer.OnGestureDetected += OnGestureDetected;

                MouseController.Current.ClickGestureDetector = circleGestureRecognizer;
            }
        }

        void LoadGoDetector()
        {
            using (Stream recordStream = File.Open(goKBPath, FileMode.OpenOrCreate))
            {
                goRecognizer = new TemplatedGestureDetector("가다", recordStream);
                goRecognizer.DisplayCanvas = gesturesCanvas;
                goRecognizer.OnGestureDetected += OnGestureDetected;

                MouseController.Current.ClickGestureDetector = goRecognizer;
            }
        }

        void LoadEatDetector()
        {
            using (Stream recordStream = File.Open(eatPath, FileMode.OpenOrCreate))
            {
                eatRecognizer = new TemplatedGestureDetector("재미있다", recordStream);
                eatRecognizer.DisplayCanvas = gesturesCanvas;
                eatRecognizer.OnGestureDetected += OnGestureDetected;

                MouseController.Current.ClickGestureDetector = eatRecognizer;
            }
        }

        private void recordGesture_Click(object sender, RoutedEventArgs e)
        {
            if (circleGestureRecognizer.IsRecordingPath)
            {
                circleGestureRecognizer.EndRecordTemplate();
                recordGesture.Content = "Record Gesture";
                return;
            }

            circleGestureRecognizer.StartRecordTemplate();
            recordGesture.Content = "Stop Recording";
        }

        void OnGestureDetected(string gesture)
        {
            int pos = detectedGestures.Items.Add(string.Format("{0} : {1}", gesture, DateTime.Now));
            detectedGestures.SelectedIndex = detectedGestures.Items.Count - 1;
            detectedGestures.Items.MoveCurrentTo(detectedGestures.SelectedItem);
            detectedGestures.ScrollIntoView(detectedGestures.SelectedItem);
            

            detectedGestures.SelectedIndex = pos;
        }

        void CloseGestureDetector()
        {
            if (circleGestureRecognizer == null)
                return;
            
            if (goRecognizer == null)
                return;

            if (eatRecognizer == null)
                return;
            

            /*
            using (Stream recordStream = File.Create(circleKBPath))
            {
                circleGestureRecognizer.SaveState(recordStream);
            }


            using (Stream recordStream = File.Create(goKBPath))
            {
                goRecognizer.SaveState(recordStream);
            }

            using (Stream recordStream = File.Create(eatPath))
            {
                eatRecognizer.SaveState(recordStream);
            }
            */
            circleGestureRecognizer.OnGestureDetected -= OnGestureDetected;
            goRecognizer.OnGestureDetected -= OnGestureDetected;
            eatRecognizer.OnGestureDetected -= OnGestureDetected;
        }
    }
}
