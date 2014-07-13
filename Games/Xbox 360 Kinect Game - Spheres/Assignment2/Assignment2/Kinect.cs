using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Kinect;
using Assignment2;

namespace Assignment2
{
    class Kinect
    {
        public static KinectSensor kinectSensor;
        public static Texture2D kinectRGBVideo;
        public static string connectedStatus;

        public static Texture2D handImg;
        public static Vector2 handPosition;

        void Initialize()
        {
            KinectSensor.KinectSensors.StatusChanged += new
                    EventHandler<StatusChangedEventArgs>(KinectSensors_StatusChanged);
            DiscoverKinectSensor();
        }

        void KinectSensors_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            if (kinectSensor == e.Sensor)
            {
                if (e.Status == KinectStatus.Disconnected ||
                        e.Status == KinectStatus.NotPowered)
                {
                    kinectSensor = null;
                    this.DiscoverKinectSensor();
                }
            }
        }

        private void DiscoverKinectSensor()
        {
            foreach (KinectSensor sensor in KinectSensor.KinectSensors)
            {
                if (sensor.Status == KinectStatus.Connected)
                {
                    kinectSensor = sensor;
                    break;
                }
            }
            if (kinectSensor == null)
            {
                connectedStatus = "Found none Kinect Sensors connected to USB";
                return;
            }
            // Init the kinect
            if (kinectSensor.Status == KinectStatus.Connected)
            {
                InitializeKinect();
            }
        }

        private bool InitializeKinect()
        {
            // Color stream
            kinectSensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
            kinectSensor.ColorFrameReady += new
                  EventHandler<ColorImageFrameReadyEventArgs>(kinectSensor_ColorFrameReady);

            // Skeleton Stream
            kinectSensor.SkeletonStream.Enable();
            kinectSensor.SkeletonFrameReady += new
                 EventHandler<SkeletonFrameReadyEventArgs>(kinectSensor_SkeletonFrameReady);

            //kinectSensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
            //kinectSensor.ColorFrameReady += new
            //        EventHandler<ColorImageFrameReadyEventArgs>(kinectSensor_ColorFrameReady);
            try
            {
                kinectSensor.Start();
            }
            catch
            {
                connectedStatus = "Unable to start the Kinect Sensor";
                return false;
            }
            return true;
        }

        void kinectSensor_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            using (ColorImageFrame colorImageFrame = e.OpenColorImageFrame())
            {
                if (colorImageFrame != null)
                {
                    byte[] pixelsFromFrame = new byte[colorImageFrame.PixelDataLength];
                    colorImageFrame.CopyPixelDataTo(pixelsFromFrame);
                    Color[] color = new Color[colorImageFrame.Height * colorImageFrame.Width];
                    kinectRGBVideo = new Texture2D(Game1.graphics.GraphicsDevice,
                                                   colorImageFrame.Width,
              colorImageFrame.Height);
                    // Go through each pixel and set the bytes correctly
                    // Remember, each pixel has Red, Green and Blue
                    int index = 0;
                    for (int y = 0; y < colorImageFrame.Height; y++)
                    {
                        for (int x = 0; x < colorImageFrame.Width; x++, index += 4)
                        {
                            color[y * colorImageFrame.Width + x] = new Color(pixelsFromFrame[index + 2],
                                             pixelsFromFrame[index + 1], pixelsFromFrame[index + 0]);
                        }
                    }
                    // Set pixeldata from the ColorImageFrame to a Texture2D
                    kinectRGBVideo.SetData(color);
                }
            }
        }

        void kinectSensor_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    Skeleton[] skeletonData = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletonData);
                    Skeleton playerSkeleton = (from s in skeletonData
                                               where s.TrackingState == SkeletonTrackingState.Tracked
                                               select s).FirstOrDefault();
                    if (playerSkeleton != null)
                    {
                        Joint rightHand = playerSkeleton.Joints[JointType.HandRight];
                        handPosition = new Vector2((((0.5f * rightHand.Position.X) + 0.5f) * (640)),
                                                  (((-0.5f * rightHand.Position.Y) + 0.5f) * (480)));
                    }
                }
            }
        }
    }
}
