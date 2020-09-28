using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusicNegacion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MusicController.Instance.StopMecanic();
        MusicController.Instance.PlayHouse();
    }

}
