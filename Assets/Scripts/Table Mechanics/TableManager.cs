using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TableManager : MonoBehaviour
{
    // Texto del timer en el canvas
    public Text timerText;

    int timer = 60;

    // Referencia a la ia
    public IAHand iaHand;

    // Referencia al jugador
    public PlayerHand playerHand;

    // Referencia a las posiciones de los items sobre la mesa
    public Transform tablePositions;

    // Referencia a las posiciones de los items sobre el suelo
    public Transform groundPositions;

    // Items del juego
    public GameObject[] items;

    // Inicia el timer
    IEnumerator Start()
    {
        InstantiateItems();

        while (timer > 0)
        {
            timerText.text = timer.ToString();
            timer--;

            yield return new WaitForSeconds(1f);
        }

        DisablePlayers();
        CheckGame();
    }

    // Pone los items sobre la mesa, las posiciones en la mesa deben ser iguales o mayores que la cantidad de items
    // ya que es un item para cada posición, si hay menos posiciones no saldria del ciclo while ya que no encontraria
    // una posición libre
    private void InstantiateItems()
    {
        foreach(GameObject item in items)
        {
            // Posicion random del item
            Transform targetPosition = tablePositions.GetChild(Random.Range(0, tablePositions.childCount));

            while(targetPosition.childCount > 0) targetPosition = tablePositions.GetChild(Random.Range(0, tablePositions.childCount));

            Transform itemTemp = Instantiate(item, targetPosition.position + Vector3.up, Quaternion.Euler(90, 0, 0)).transform;
            itemTemp.SetParent(targetPosition);
            itemTemp.GetComponent<Item>().Place = targetPosition.GetSiblingIndex();
        }
    }

    // Desactiva los jugadores para que no se puedan mover
    private void DisablePlayers()
    {
        Destroy(iaHand);
        Destroy(playerHand);
        Destroy(timerText);
    }

    // Comprueba el estado del juego
    private void CheckGame()
    {
        // Cantidad de items en la mesa
        int tableItems = CheckTable();

        // Cantidad de items en el suelo
        int groundItems = CheckGround();

        // Empate
        if(tableItems == groundItems)
        {
            Debug.Log("Empate!");
        }

        // Gana el jugador
        else if(tableItems > groundItems)
        {
            Debug.Log("Gana el jugador!");
        }

        // Gana la ia
        else
        {
            Debug.Log("Gana la ia!");
        }
    }

    // Comprueba los items sobre la mesa
    private int CheckTable()
    {
        int nItems = 0;

        foreach(Transform itemPosition in tablePositions)
        {
            // Suma los items sobre la mesa
            if (itemPosition.childCount > 0) nItems++;
        }

        return nItems;
    }

    // Comprueba los items en el suelo
    private int CheckGround()
    {
        int nItems = 0;

        foreach (Transform itemPosition in groundPositions)
        {
            // Suma los items sobre el suelo
            if (itemPosition.childCount > 0) nItems++;
        }

        return nItems;
    }
}
