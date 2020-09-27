using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Plat()
    {
        MusicController.Instance.Play();
    }

    public void Stop()
    {
        MusicController.Instance.Stop();
    }
    
    public void PlayMenu()
    {
        MusicController.Instance.PlayMenu();
    }

    public void PlayBuriel()
    {
        MusicController.Instance.PlayBuriel();
    }

    public void PlayHouse()
    {
        MusicController.Instance.PlayHouse();
    }

    public void PlayWin1()
    {
        MusicController.Instance.PlayWin1();
    }

    public void PlayWin2()
    {
        MusicController.Instance.PlayWin2();
    }

    public void PlayLose()
    {
        MusicController.Instance.PlayLose();
    }

    public void PlayRain()
    {
        MusicController.Instance.PlayRain();
    }

    public void StopRain()
    {
        MusicController.Instance.StopRain();
    }

    public void PlayMecanic()
    {
        MusicController.Instance.PlayMecanic();
    }

    public void StopMecanic()
    {
        MusicController.Instance.StopMecanic();
    }

}
