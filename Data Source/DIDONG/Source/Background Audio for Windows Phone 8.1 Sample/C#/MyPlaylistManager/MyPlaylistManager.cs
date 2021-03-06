/*
 * (c) Copyright Microsoft Corporation.
This source is subject to the Microsoft Public License (Ms-PL).
All other rights reserved.
*/

using System;
using System.Diagnostics;
using Windows.Foundation;
using Windows.Media.Playback;


using Windows.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;

/*
 * This file implements a sample implementation of playlist management.
 * Make sure to note that objects of this class are to be initialized and 
 * run in background context. Using these in foreground context will lead 
 * music to stop or MediaPlayer to throw exception once the foreground app 
 * is suspended. 
 */
namespace SampleBackgroundAudio.MyPlaylistManager
{
    /// <summary>
    /// Manage playlist information. For simplicity of this sample, we allow only one playlist
    /// </summary>
    public sealed class MyPlaylistManager
    {
        private static MyPlaylist instance;
        public MyPlaylist Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new MyPlaylist();
                }
                return instance;
            }
        }

        /// <summary>
        /// Clears playlist for re-initialization
        /// </summary>
        public void ClearPlaylist()
        {
            instance = null;
        }
    }
    public sealed class MyPlaylist
    {
        #region Private members
        //static String[] tracks = { "ms-appx:///Assets/Media/Ring01.wma", 
        //                           "ms-appx:///Assets/Media/Ring02.wma",
        //                           "ms-appx:///Assets/Media/Ring03.wma"};

        public StorageFolder folder = KnownFolders.MusicLibrary;
        public static List<StorageFile> musicList = new List<StorageFile>();

        int CurrentTrackId = -1;

        private MediaPlayer mediaPlayer;
        private TimeSpan startPosition = TimeSpan.FromSeconds(0);
        internal MyPlaylist()
        {
            mediaPlayer = BackgroundMediaPlayer.Current;
            mediaPlayer.MediaOpened += MediaPlayer_MediaOpened;
            mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
            mediaPlayer.CurrentStateChanged += mediaPlayer_CurrentStateChanged;
            mediaPlayer.MediaFailed += mediaPlayer_MediaFailed;
            
            initValue();
        }

        public async void initValue()
        {
            musicList.Clear();
            await getFiles(musicList, folder);
        }

        public async Task getFiles(List<StorageFile> list, StorageFolder parent)
        {
            foreach (var item in await parent.GetFilesAsync())
            {
                list.Add(item);
            }
            foreach (var item in await parent.GetFoldersAsync())
                await getFiles(list, item);
        }

        #endregion

        #region Public properties, events and handlers
        /// <summary>
        /// Get the current track name
        /// </summary>
        public string CurrentTrackName
        {
            get
            {
                if (CurrentTrackId == -1)
                {
                    return String.Empty;
                }
                if (CurrentTrackId < musicList.Count)
                {
                }
                else
                    throw new ArgumentOutOfRangeException("Track Id is higher than total number of tracks");
            }
        }
        /// <summary>
        /// Invoked when the media player is ready to move to next track
        /// </summary>
        public event TypedEventHandler<MyPlaylist, object> TrackChanged;
        #endregion

        #region MediaPlayer Handlers
        /// <summary>
        /// Handler for state changed event of Media Player
        /// </summary>
        void mediaPlayer_CurrentStateChanged(MediaPlayer sender, object args)
        {

            if (sender.CurrentState == MediaPlayerState.Playing && startPosition != TimeSpan.FromSeconds(0))
            {
                // if the start position is other than 0, then set it now
                sender.Position = startPosition;
                sender.Volume = 1.0;
                startPosition = TimeSpan.FromSeconds(0);
                sender.PlaybackMediaMarkers.Clear();
            }
        }

        /// <summary>
        /// Fired when MediaPlayer is ready to play the track
        /// </summary>
        void MediaPlayer_MediaOpened(MediaPlayer sender, object args)
        {
            // wait for media to be ready
            sender.Play();
            Debug.WriteLine("New Track" + this.CurrentTrackName);
            TrackChanged.Invoke(this, CurrentTrackName);
        }
        
        /// <summary>
        /// Handler for MediaPlayer Media Ended
        /// </summary>
        private void MediaPlayer_MediaEnded(MediaPlayer sender, object args)
        {
            SkipToNext();          
        }

        private void mediaPlayer_MediaFailed(MediaPlayer sender, MediaPlayerFailedEventArgs args)
        {
            Debug.WriteLine("Failed with error code " + args.ExtendedErrorCode.ToString());
        }
        #endregion

        #region Playlist command handlers
        /// <summary>
        /// Starts track at given position in the track list
        /// </summary>
        private void StartTrackAt(int id)
        {
            CurrentTrackId = id;
            mediaPlayer.AutoPlay = false;
            mediaPlayer.SetFileSource(musicList[id]);
        }

        /// <summary>
        /// Starts a given track by finding its name
        /// </summary>
        //public void StartTrackAt(string TrackName)
        //{
        //    for (int i = 0; i < tracks.Length; i++)
        //    {
        //        if (tracks[i].Contains(TrackName))
        //        {
        //            string source = tracks[i];
        //            CurrentTrackId = i;
        //            mediaPlayer.AutoPlay = false;
        //            mediaPlayer.SetUriSource(new Uri(source));
        //        }
        //    }
        //}

        /// <summary>
        /// Starts a given track by finding its name and at desired position
        /// </summary>
        //public void StartTrackAt(string TrackName, TimeSpan position)
        //{
        //    for (int i = 0; i < tracks.Length; i++)
        //    {
        //        if (tracks[i].Contains(TrackName))
        //        {
        //            CurrentTrackId = i;
        //            break;
        //        }
        //    }

        //    mediaPlayer.AutoPlay = false;

        //    // Set the start position, we set the position once the state changes to playing, 
        //    // it can be possible for a fraction of second, playback can start before we are 
        //    // able to seek to new start position
        //    mediaPlayer.Volume = 0;
        //    startPosition = position;
        //    mediaPlayer.SetUriSource(new Uri(tracks[CurrentTrackId]));
        //}

        /// <summary>
        /// Play all tracks in the list starting with 0 
        /// </summary>
        public void PlayAllTracks()
        {
            StartTrackAt(0);
        }

        /// <summary>
        /// Skip to next track
        /// </summary>
        public void SkipToNext()
        {
            StartTrackAt((CurrentTrackId + 1) % musicList.Count);
        }

        /// <summary>
        /// Skip to next track
        /// </summary>
        public void SkipToPrevious()
        {
            if (CurrentTrackId == 0)
            {
                StartTrackAt(CurrentTrackId);
            }
            else
            {
                StartTrackAt(CurrentTrackId - 1);
            }
        }
        
        #endregion
        
    }
}
