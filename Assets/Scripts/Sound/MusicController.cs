using UnityEngine;
using System.Collections;
class MusicController
{
    static MusicController _instance;

    public static MusicController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new MusicController();
            }

            return _instance;
        }
    }

    FMOD.Studio.EventInstance AllMusicGame;

    MusicController()
    {
        AllMusicGame = FMODUnity.RuntimeManager.CreateInstance("event:/Music");
    }

    public void Play()
    {
        AllMusicGame.start();
    }

    public void Stop()
    {
        AllMusicGame.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void PlayMenu()
    {
        AllMusicGame.setParameterByName("Music", 0);

    }

    public void PlayLevel1()
    {
        AllMusicGame.setParameterByName("Music", 1);
    }

    public void PlayLevel2()
    {
        AllMusicGame.setParameterByName("Music", 2);
    }

    public void PlayLevel3()
    {
        AllMusicGame.setParameterByName("Music", 3);
    }

    public void PlayLevel4()
    {
        AllMusicGame.setParameterByName("Music", 4);
    }
}