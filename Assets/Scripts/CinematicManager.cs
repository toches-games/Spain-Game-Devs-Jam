using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicManager : MonoBehaviour
{
    [SerializeField]
    PlayableDirector intro;
    [SerializeField]
    PlayableDirector outroBueno;
    [SerializeField]
    PlayableDirector outroMalo;

    [SerializeField]
    double introSkipTime;
    [SerializeField]
    double outroBuenoSkipTime;
    [SerializeField]
    double outroMaloSkipTime;

    private void Awake()
    {
        
    }

    public void SkipButton()
    {
        if (intro.state == PlayState.Playing)
        {
            intro.time = introSkipTime;
            MusicController.Instance.PlayMecanic();
        }
        else if (outroBueno.state == PlayState.Playing)
        {
            outroBueno.time = outroBuenoSkipTime;
            MusicController.Instance.PlayBuriel();
        }else if(outroMalo.state == PlayState.Playing)
        {
            outroMalo.time = outroMaloSkipTime;
            MusicController.Instance.PlayLoseFast();
        }
    }
}
