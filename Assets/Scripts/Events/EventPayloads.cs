// ReSharper disable once CheckNamespace
namespace Events
{
    public struct Win {}
    
    public struct Lose {}
    
    public struct ShowRoundEndPanel {}

    public struct PlaySFX
    {
        public Sound Sound;

        public PlaySFX(Sound sound)
        {
            Sound = sound;
        }
    }
}