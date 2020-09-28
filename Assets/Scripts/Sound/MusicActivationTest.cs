using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicActivationTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MusicController.Instance.Play();
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        MusicController.Instance.PlayBuriel();
        yield return new WaitForSeconds(0.1f);
        MusicController.Instance.PlayHouse();
    }
}
