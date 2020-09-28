using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class PlayerHand: MonoBehaviour
{
    // Imagen de cursor para el jugador
    public Texture2D cursor;
    public Texture2D cursor2;

    // Layer para que solo detecte al item al dar un solo click
    public LayerMask tableMask;

    // Layer para que detecte la mesa y suba los item a la misma altura de la mesa
    public LayerMask itemMask;

    // Distancia del suelo al item, cuando está seleccionado
    [Range(0.25f, 2f)]
    public float itemDistance = 1f;

    // Velocidad de desplazamiento del item seleccionado
    [Range(5, 10)]
    public float speed = 10;

    //Nos dice si ya seleccionó a un item o no
    private bool selected;

    //Guarda el item que selecionó
    private RaycastHit item;

    bool sound = false;

    private void Start()
    {
        Cursor.SetCursor(cursor, new Vector2(cursor.width / 2, cursor.height /2), CursorMode.Auto);
    }

    private void Update()
    {
        // Rayo desde la camara al espacio 3d
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            // Si seleccionó un item
            if(Physics.Raycast(ray, out item, Mathf.Infinity, itemMask))
            {
                sound = true;
                if (!SoundController.Instance.takeObject.IsPlaying() && sound == true)
                {
                    SoundController.Instance.takeObject.Play();
                    SoundController.Instance.takeObject.EventInstance.setParameterByName("takeObject", 0);
                    sound = false;
                }
                // Si el item está en la posición correcta, hacemos que no se pueda arrastrar
                if (!item.transform.GetComponent<Item>().Draggable)
                {
                    // Borramos el item seleccionado actual
                    item = new RaycastHit();

                    // Retornamos para que no se cambien los demas valores
                    return;
                }

                // Cambiamos el estado a item seleccionado
                selected = true;

                // Se saca el item del suelo
                //item.transform.SetParent(null);

                // Para que no le afecte la gravedad
                //item.rigidbody.isKinematic = true;
            }
        }

        // Si mantiene el click presionado y seleccionó un objeto, entonces lo arrastra
        if(Input.GetMouseButton(0) && selected)
        {
            Cursor.SetCursor(cursor2, new Vector2(cursor2.width / 2, cursor2.height / 2), CursorMode.Auto);

            // Posición del mouse en 3d
            Vector3 mouse3D = Input.mousePosition + Vector3.forward * Camera.main.transform.position.y;

            // Cordenadas del mouse al mundo 3d
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(mouse3D);

            // Si tiene el cursor sobre la mesa o el suelo mientras arrastra al item
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, tableMask))
            {
                // El item se va a estar arrastrando
                item.transform.GetComponent<Item>().Dragging = true;

                // Entonces la acomoda la altura del item a la de ese suelo o mesa
                // y se mueve de acuerdo a la posición del mouse
                Vector3 targetPosition = new Vector3(worldPoint.x, hit.point.y + itemDistance, worldPoint.z);
                float distance = Vector3.Distance(item.rigidbody.position, targetPosition);

                item.rigidbody.velocity = new Vector3(item.rigidbody.velocity.x, 0f, item.rigidbody.velocity.z);
                item.rigidbody.MovePosition(Vector3.MoveTowards(item.rigidbody.position, targetPosition, distance * speed * Time.deltaTime));
                //item.rigidbody.MovePosition(targetPosition);
            }
        }

        // Si deja de presionar el click
        if (Input.GetMouseButtonUp(0) && selected)
        {
            Cursor.SetCursor(cursor, new Vector2(cursor.width / 2, cursor.height / 2), CursorMode.Auto);

            // Se cambia el estado a no seleccionado
            selected = false;

            // Velocidad a cero para que no salga volando cuando se suelte el click
            item.rigidbody.velocity = Vector3.zero;

            // El item deja de arrastrarse
            item.transform.GetComponent<Item>().Dragging = false;

            // Para que caiga cuando se suelta
            //item.rigidbody.isKinematic = false;

            // Nos aseguramos de que el item no quede seleccionado
            item = new RaycastHit();
        }
    }
}
