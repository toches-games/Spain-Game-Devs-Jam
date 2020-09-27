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

    public void PlayBuriel()
    {
        AllMusicGame.setParameterByName("Music", 1);
    }

    public void PlayHouse()
    {
        AllMusicGame.setParameterByName("Music", 2);
    }

    public void PlayWin1()
    {
        AllMusicGame.setParameterByName("Music", 3);
    }

    public void PlayWin2()
    {
        AllMusicGame.setParameterByName("Music", 4);
    }

    public void PlayLose()
    {
        AllMusicGame.setParameterByName("Music", 5);

    }

    public void PlayRain()
    {
        AllMusicGame.setParameterByName("Rain", 1);
    }

    public void StopRain()
    {
        AllMusicGame.setParameterByName("Rain", 0);
    }

    public void PlayMecanic()
    {
        AllMusicGame.setParameterByName("Ritmic", 1);
    }

    public void StopMecanic()
    {
        AllMusicGame.setParameterByName("Ritmic", 0);
    }
}