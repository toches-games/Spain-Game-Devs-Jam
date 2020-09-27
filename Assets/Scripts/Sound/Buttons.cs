using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public void PlayButton()
    {
        SoundController.Instance.button.Play();
    }
}
