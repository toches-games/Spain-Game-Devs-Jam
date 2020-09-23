using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [HideInInspector]
    public bool Draggable { set; get; }

    private void OnTriggerEnter(Collider other)
    {
        // Si colisiona con el lugar correcto sobre la mesa, se actualiza la posición del objeto en pantalla
        // y hacemos que no se pueda arrastrar
        if(other.CompareTag("Table Position"))
        {
            Draggable = false;
            other.enabled = false;
            transform.position = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);
            transform.SetParent(other.transform);
        }
    }
}
