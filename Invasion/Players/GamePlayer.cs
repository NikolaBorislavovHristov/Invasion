namespace Invasion.Players
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Media;

    public class GamePlayer : IGamePlayer
    {
        private List<MediaPlayer> players;

        public GamePlayer()
        {
            this.players = new List<MediaPlayer>();
        }

        public void PlayAndRepeat(string songPath)
        {
            var player = new MediaPlayer();
            player.Open(new Uri(songPath, UriKind.RelativeOrAbsolute));
            this.players.Add(player);
            player.MediaEnded += this.DelateMedia;
            player.Play();
        }

        public void PlayOnce(string songPath)
        {
            var player = new MediaPlayer();
            player.Open(new Uri(songPath, UriKind.RelativeOrAbsolute));
            this.players.Add(player);
            player.MediaEnded += this.DelateMedia;
            player.Play();
        }

        public void ClearPlaylist()
        {
            this.players.Clear();
        }

        private void DelateMedia(object sender, EventArgs e)
        {
            var player = (sender as MediaPlayer);
            this.players.Remove(player);
        }

        private void RestartMedia(object sender, EventArgs e)
        {
            var player = (sender as MediaPlayer);
            player.Play();
        }
    }
}