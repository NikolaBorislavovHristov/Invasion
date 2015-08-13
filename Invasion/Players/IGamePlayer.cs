namespace Invasion.Players
{
    public interface IGamePlayer
    {
        void PlayAndRepeat(string songPath);
        void PlayOnce(string songPath);
        void ClearPlaylist();
    }
}