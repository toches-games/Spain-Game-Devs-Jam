using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool Draggable { set; get; }

    public bool Dragging { set; get; }

    public Rigidbody Rig { private set; get; }

    public int Place { set; get; }

    private bool incorrect;

    public Transform GroundPosition{ set; private get; }

    private void Awake()
    {
        Rig = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Se desplaza el item al lugar del suelo donde estaba si se pone en un lugar que no es
        if (incorrect)
        {
            float distance = Vector3.Distance(GroundPosition.position, Rig.position);
            Rig.MovePosition(Vector3.MoveTowards(Rig.position, GroundPosition.position, distance * 8f * Time.deltaTime));

            if(distance <= 0.1f)
            {
                incorrect = false;
                Draggable = true;
                transform.SetParent(GroundPosition);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si se pone en un lugar incorrecto sobre la mesa
        if (other.CompareTag("Table Position") && other.transform.GetSiblingIndex() != Place && !Dragging)
        {
            incorrect = true;
            Draggable = false;
            return;
        }

        // Si colisiona con el lugar correcto sobre la mesa, se actualiza la posición del objeto en pantalla
        // y hacemos que no se pueda arrastrar
        if (other.CompareTag("Table Position") && !Dragging)
        {
            // Si el item está en un lugar correcto
            Draggable = false;
            other.enabled = false;
            transform.position = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);
            transform.SetParent(other.transform);
        }
    }

    // Si golpea con el suelo o mesa
    private void OnCollisionEnter(Collision other) {
        
    }
}
