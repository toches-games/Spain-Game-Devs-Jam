using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IAHand : MonoBehaviour
{
    // Imagen de cursor para la ia
    public Image cursor;

    // Referencia a las posiciones de los items sobre la mesa
    public Transform tablePositions;

    // Referencia a las posiciones de los items sobre el suelo
    public Transform groundPositions;

    IEnumerator Start()
    {
        while (true)
        {
            Transform targetDragItemPosition = tablePositions.GetChild(Random.Range(0, tablePositions.childCount));

            while(targetDragItemPosition.childCount == 0)
            {
                targetDragItemPosition = tablePositions.GetChild(Random.Range(0, tablePositions.childCount));
                yield return null;
            }

            Transform targetDropItemPosition = groundPositions.GetChild(Random.Range(0, groundPositions.childCount));

            while (targetDropItemPosition.childCount == 1)
            {
                targetDropItemPosition = groundPositions.GetChild(Random.Range(0, groundPositions.childCount));
                yield return new WaitForSeconds(0.1f);
            }

            targetDragItemPosition.GetChild(0).SetParent(targetDropItemPosition);
            targetDropItemPosition.GetChild(0).localPosition = new Vector3(0, 1f, 0);

            cursor.rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, targetDropItemPosition.GetChild(0).position);

            targetDropItemPosition.GetChild(0).GetComponent<Item>().Draggable = true;
            targetDragItemPosition.GetComponent<BoxCollider>().enabled = true;

            yield return null;
        }
    }
}
