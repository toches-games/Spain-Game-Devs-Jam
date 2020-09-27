using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DissolveIntro : MonoBehaviour
{
    public float dissolveDuration;

    private Material material;

    private float velocity;

    private void Awake() {
        material = GetComponent<Image>().material;
    }

    // Update is called once per frame
    void Update()
    {
        material.SetFloat("Fade", Mathf.SmoothDamp(material.GetFloat("Fade"), 1, ref velocity, dissolveDuration));   
    }
}
