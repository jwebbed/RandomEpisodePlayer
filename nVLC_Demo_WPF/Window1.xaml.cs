﻿//    nVLC
//    
//    Author:  Roman Ginzburg
//
//    nVLC is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    nVLC is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//    GNU General Public License for more details.
//     
// ========================================================================

using System;
using System.Windows;
using System.Windows.Forms;
using Declarations;
using Declarations.Events;
using Declarations.Media;
using Declarations.Players;
using Implementation;
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using tvlib;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace nVLC_Demo_WPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        IMediaPlayerFactory m_factory;
        IVideoPlayer m_player;
        IMediaFromFile m_media;
        ShowManager manager;
        string show;
        private volatile bool m_isDrag;

        public Window1()
        {
            InitializeComponent();

            this.KeyDown += this.KeyPress;

            System.Windows.Forms.Panel p = new System.Windows.Forms.Panel();
            p.BackColor = System.Drawing.Color.Black;
            windowsFormsHost1.Child = p;

            m_factory = new MediaPlayerFactory(true);
            m_player = m_factory.CreatePlayer<IVideoPlayer>();

            this.DataContext = m_player;

            m_player.Events.PlayerPositionChanged += new EventHandler<MediaPlayerPositionChanged>(Events_PlayerPositionChanged);
            m_player.Events.TimeChanged += new EventHandler<MediaPlayerTimeChanged>(Events_TimeChanged);
            m_player.Events.MediaEnded += new EventHandler(Events_MediaEnded);
            m_player.Events.PlayerStopped += new EventHandler(Events_PlayerStopped);

            m_player.WindowHandle = p.Handle;
            slider2.Value = m_player.Volume;

            this.show = "Futurama";
            this.loadManager();
            this.playEpisode();
        }

        private void KeyPress(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Space)
            {
                this.m_player.Pause();
            }
        }

        void Events_PlayerStopped(object sender, EventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(delegate
            {
                InitControls();
            }));
        }

        void Events_MediaEnded(object sender, EventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(delegate
            {
                InitControls();
            }));
            this.playEpisode();
        }

        private void InitControls()
        {
            slider1.Value = 0;
            label1.Content = "00:00:00";
            label3.Content = "00:00:00";
        }

        void Events_TimeChanged(object sender, MediaPlayerTimeChanged e)
        {
            this.Dispatcher.BeginInvoke(new Action(delegate
            {
                label1.Content = TimeSpan.FromMilliseconds(e.NewTime).ToString().Substring(0, 8);
            }));
        }

        void Events_PlayerPositionChanged(object sender, MediaPlayerPositionChanged e)
        {
            this.Dispatcher.BeginInvoke(new Action(delegate
            {
                if (!m_isDrag)
                {
                    slider1.Value = (double)e.NewPosition;
                }
            }));
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            this.playEpisode();
        }

        void Events_StateChanged(object sender, MediaStateChange e)
        {
            this.Dispatcher.BeginInvoke(new Action(delegate
            {

            }));
        }

        void Events_DurationChanged(object sender, MediaDurationChange e)
        {
            this.Dispatcher.BeginInvoke(new Action(delegate
            {
                label3.Content = TimeSpan.FromMilliseconds(e.NewDuration).ToString().Substring(0, 8);
            }));
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            m_player.Pause();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            m_player.Stop();
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            m_player.ToggleMute();
        }

        private void slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (m_player != null)
            {
                m_player.Volume = (int)e.NewValue;
            }
        }

        private void slider1_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            m_player.Position = (float)slider1.Value;
            m_isDrag = false;
        }

        private void slider1_DragStarted(object sender, DragStartedEventArgs e)
        {
            m_isDrag = true;
        }

        private void playEpisode()
        {
            Episode e = this.manager.getRandomEpisode(this.show);
            e.increasePlayCount();
            this.playEpisode(e.path);
        }

        private void playEpisode(string path)
        {
            m_media = m_factory.CreateMedia<IMediaFromFile>(path);
            m_media.Events.DurationChanged += new EventHandler<MediaDurationChange>(Events_DurationChanged);
            m_media.Events.StateChanged += new EventHandler<MediaStateChange>(Events_StateChanged);

            m_player.Open(m_media);
            m_media.Parse(true);
            m_player.Play();
            this.saveManager();
        }

        private void loadManager()
        {
            string FileName = Directory.GetCurrentDirectory();
            FileName += "\\ShowData.data";
            if (File.Exists(FileName))
            {
                Stream ReadStream = File.OpenRead(FileName);
                BinaryFormatter deserializer = new BinaryFormatter();
                this.manager = (ShowManager)deserializer.Deserialize(ReadStream);
                ReadStream.Close();
            }
            else
            {
                this.manager = new ShowManager("J:\\TV Shows");
                this.manager.Populate();
            }

        }

        private void saveManager()
        {
            string FileName = Directory.GetCurrentDirectory();
            FileName += "\\ShowData.data";
            if (File.Exists(FileName))
            {
                Stream WriteStream = File.OpenWrite(FileName);
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(WriteStream, this.manager);
                WriteStream.Close();
            }
            else
            {
                Stream s = File.Create(FileName);
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(s, this.manager);
                s.Close();
            }
        }

        private void setFuturama(object sender, RoutedEventArgs e)
        {
            this.show = "Futurama";
        }

        private void setCommunity(object sender, RoutedEventArgs e)
        {
            this.show = "Community";
        }
    }
}
