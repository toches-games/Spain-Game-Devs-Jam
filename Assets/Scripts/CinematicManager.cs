using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicManager : MonoBehaviour
{
    PlayableDirector intro;
    PlayableDirector outroBueno;
    PlayableDirector outroMalo;

    private void Awake()
    {
        intro = GameObject.Find("TimeLineIntro").GetComponent<PlayableDirector>();
        outroBueno = GameObject.Find("TimeLineOutroBueno").GetComponent<PlayableDirector>();
        outroMalo = GameObject.Find("TimeLineOutroMalo").GetComponent<PlayableDirector>();
    }

    public void SkipButton()
    {
        if (intro.state == PlayState.Playing)
        {
            intro.time = 13.35;
        }else if (outroBueno.state == PlayState.Playing)
        {
            outroBueno.time = 12;
        }else if(outroMalo.state == PlayState.Playing)
        {
            outroMalo.time = 12.20;
        }
    }
}
