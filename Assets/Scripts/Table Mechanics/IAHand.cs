using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IAHand : MonoBehaviour
{
    // Imagen de cursor para la ia
    public Image cursor;

    // Velocidad de desplazamiento de la ia
    [Range(1, 8)]
    public float speed = 8;

    // Referencia a las posiciones de los items sobre la mesa
    public Transform tablePositions;

    // Referencia a las posiciones de los items sobre el suelo
    public Transform groundPositions;

    // Nos dice si la ia está arrastrando o no
    private bool dragging;

    // Guarda el item actual a arrastrar
    private Transform draggingItem;

    // Guarda la posición del item al que va arrastrar y la posición a donde va a soltar ese item
    private Vector3 targetPosition;

    private Rigidbody item;

    // Regresa una posición aleatorea que contiene un item sobre la mesa, si es una posición vacia retorna nulo
    Transform RandomTargetDragItemPosition()
    {
        Transform targetDragItemPosition = tablePositions.GetChild(Random.Range(0, tablePositions.childCount));

        if (targetDragItemPosition.childCount == 0)
        {
            targetDragItemPosition = null;
        }

        return targetDragItemPosition;
    }

    // Regresa una posición aleatorea para el item en el suelo, si es una posición ocupada retorna nulo
    Transform RandomTargetDropItemPosition()
    {
        Transform targetDropItemPosition = groundPositions.GetChild(Random.Range(0, groundPositions.childCount));
      
        if (targetDropItemPosition.childCount == 1)
        {
           
            targetDropItemPosition = null;
        }

        return targetDropItemPosition;
    }

    private void Update()
    {
        // Si no está arrastrando algun item
        if (!dragging)
        {
            // Si no hay un item para arrastrar, busca uno, no hace mas nada hasta que tenga un item para arrastrar
            if (!draggingItem)
            {
                draggingItem = RandomTargetDragItemPosition();
                return;
            }

            // Si ya sabe que item arrastrar, se le da la posición objetivo para que se desplace hacia el item a buscar
            else if (targetPosition != draggingItem.position) targetPosition = draggingItem.position;

            // Movemos el cursor de la ia en el mundo 3d
            float distance = Vector3.Distance(transform.position, targetPosition);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * distance * Time.deltaTime);

            // Si el cursor de la ia está cerca del item que va arrastrar, va a comenzar a arrastrarlo
            // "Click" de la ia para empezar a arrastrar
            if (distance <= 0.1f && !dragging)
            {
                SoundController.Instance.takeObject.Play();
                SoundController.Instance.takeObject.EventInstance.setParameterByName("takeObject", 0);
                item = draggingItem.GetChild(0).GetComponent<Rigidbody>();
                //item.transform.SetParent(null);
                item.transform.GetComponent<Item>().Draggable = false;
                item.transform.GetComponent<Item>().Dragging = true;
                draggingItem.GetComponent<BoxCollider>().enabled = true;

                dragging = true;
                draggingItem = null;
            }
        }

        else
        {
            // Busca una posición para colocar el item en el suelo, y no hace mas nada hasta que haya una
            if (!draggingItem)
            {
                draggingItem = RandomTargetDropItemPosition();
                return;
            }

            // Si ya sabe a que posición del suelo ir, se le da la posición objetivo para que se desplace hacia el suelo
            else if (targetPosition != draggingItem.position)
            {
                targetPosition = draggingItem.position + Vector3.up * 2f;
                item.transform.GetComponent<Item>().GroundPosition = draggingItem;
            }

            // Movemos el cursor de la ia en el mundo 3d
            float distance = Vector3.Distance(transform.position, targetPosition);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * distance * Time.deltaTime);

            //if (item) item.velocity = new Vector3(item.velocity.x, 0f, item.velocity.z);

            // Si el cursor de la ia está cerca de donde dejará al item, lo suelta
            // "Click" de la ia para soltar el objeto
            if (distance <= 0.1f && dragging)
            {
                item.transform.SetParent(draggingItem);
                item.transform.GetComponent<Item>().Draggable = true;
                item.transform.GetComponent<Item>().Dragging = false;
                item = null;

                dragging = false;
                draggingItem = null;
            }
        }

        // Movemos el cursor en pantalla dependiendo de a donde esté en el mundo 3d
        cursor.rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);

        // Arrastra al item
        if (item)
        {
            item.velocity = new Vector3(item.velocity.x, 0f, item.velocity.z);
            float distance = Vector3.Distance(item.position, targetPosition);
            item.MovePosition(Vector3.MoveTowards(item.position, targetPosition, speed * distance * 0.7f * Time.deltaTime));
            //item.MovePosition(transform.position);
        }
    }
}
