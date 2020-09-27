using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class TextAnimation : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;
    private bool pressF;

    [SerializeField]
    PlayableDirector outro;

    // Start is called before the first frame update
    void Start()
    {
        pressF = false;
        StartCoroutine(Animation());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            pressF = true;
            outro.Play();

        }
    }

    IEnumerator Animation()
    {

        yield return new WaitForSeconds(1f);
        float duration = 1.5f;
        Color notAlpha = new Color(text.color.r, text.color.g, text.color.b, 0);
        Color alpha = new Color(text.color.r, text.color.g, text.color.b, 1);

        while (!pressF)
        {
            //Debug.Log("Entra");
            text.CrossFadeColor(notAlpha, duration, false, true);
            yield return new WaitForSeconds(duration);
            text.CrossFadeColor(alpha, duration, false, true);
            yield return new WaitForSeconds(duration);
        }
        yield break;

        /**
   
        while (!pressF)
        {
            Debug.Log("Entra");
            text.CrossFadeColor(alpha, duration, false, true);
            yield return new WaitForSeconds(3f);
            text.CrossFadeColor(notAlpha, duration, false, true);
            yield return new WaitForSeconds(3f);
        }
        while (!pressF)
        {
            for (int i = 0; i < 10; i++)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - 0.1f);
                yield return new WaitForSeconds(.2f);
            }

            yield return new WaitForSeconds(1f);
            //text.CrossFadeColor(notAlpha, 1f, false, true);
            for (int i = 0; i < 10; i++)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + 0.1f);
                yield return new WaitForSeconds(.2f);
            }
        }
        **/
    }
}
