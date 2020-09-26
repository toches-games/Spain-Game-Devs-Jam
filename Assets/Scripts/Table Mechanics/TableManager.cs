﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TableManager : MonoBehaviour
{
    // Texto del timer en el canvas
    public Text timerText;

    private readonly int initialTime = 30;

    private int currentTime;

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

    // Referencia al timeline final bueno
    public GameObject goodOutro;
    public GameObject goodOutroCanvas;

    // Referencia al timeline final malo
    public GameObject badOutro;
    public GameObject badOutroCanvas;

    // Referencia al canvas del juego
    public GameObject gameCanvas;


    // Inicia el timer para el cambio de niveles
    IEnumerator Start()
    {
        currentTime = initialTime;

        yield return StartCoroutine(FirstHalf());
        DisablePlayers();

        yield return new WaitForSeconds(3f);

        EnablePlayers();
        yield return StartCoroutine(SecondHalf());
        DisablePlayers();

        yield return new WaitForSeconds(1f);

        CheckGame();
    }

    // Inicia el timer
    IEnumerator FirstHalf()
    {
        int half = currentTime / 2;
        
        InstantiateItems();

        while (currentTime >= half)
        {
            timerText.text = currentTime.ToString();
            currentTime--;

            yield return new WaitForSeconds(1f);
        }
    }

    // Inicia el timer
    IEnumerator SecondHalf()
    {
        currentTime = initialTime/ 2;

        while (currentTime > 0)
        {
            timerText.text = currentTime.ToString();
            currentTime--;

            yield return new WaitForSeconds(1f);
        }
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

            targetPosition.GetComponent<SpriteRenderer>().sprite = item.GetComponent<SpriteRenderer>().sprite;

            Transform itemTemp = Instantiate(item, targetPosition.position + Vector3.up, item.transform.rotation).transform;
            itemTemp.SetParent(targetPosition);
            itemTemp.GetComponent<Item>().Place = targetPosition.GetSiblingIndex();
        }
    }

    // Activa los jugadores para que puedan empezar a jugar
    private void EnablePlayers()
    {
        iaHand.speed *= 2;
        iaHand.enabled = true;

        playerHand.enabled = true;
        timerText.enabled = true;
    }

    // Desactiva los jugadores para que no se puedan mover
    private void DisablePlayers()
    {
        iaHand.enabled = false;
        playerHand.enabled = false;
        timerText.enabled = false;
    }

    // Comprueba el estado del juego
    private void CheckGame()
    {
        gameCanvas.SetActive(false);

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
            goodOutroCanvas.SetActive(true);
            goodOutro.SetActive(true);
        }

        // Gana la ia
        else
        {
            badOutroCanvas.SetActive(true);
            badOutro.SetActive(true);
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
