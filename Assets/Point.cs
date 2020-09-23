using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField]
    private bool state; 

    // Start is called before the first frame update
    void Start()
    {
        state = true;
    }

    public void SetState(bool newState)
    {
        state = newState;
    }

    public bool GetState()
    {
        return state;
    }

}
