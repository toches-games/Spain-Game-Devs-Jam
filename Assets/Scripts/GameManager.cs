using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedIstance;
    public const int MAX_GENERATION_GHOST = 30;
    public const int MAX_COUNT_GHOST = 7;
    //Posiciones aleatorias donde generar los fantasmas
    [SerializeField]
    public List<GameObject> points;

    [SerializeField]
    GameObject player;
    [SerializeField]
    private float generationTime;

    private void Awake()
    {
        if(sharedIstance == null)
        {
            sharedIstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerationGhost(2f));
    }

    public IEnumerator GenerationGhost(float initialWait)
    {
        int i = 0;
        yield return new WaitForSeconds(initialWait);

        while (i < MAX_GENERATION_GHOST)
        {
            if (CheckStatePoints())
            {
                GenerateGhost(points[RandomPoint()]);
                i++;
                Debug.Log("Generado: " + i);
            }
            yield return new WaitForSeconds(generationTime);
        }

    }

    public int RandomPoint()
    {
        int random = Random.Range(0, MAX_COUNT_GHOST);
        for (int i = 0; i < MAX_COUNT_GHOST &&
            !points[random].GetComponent<Point>().GetState(); i++)
        {
            random = Random.Range(0, MAX_COUNT_GHOST);
        }
        return random;
    }

    public bool CheckStatePoints()
    {
        foreach (var point in points)
        {
            if (point.GetComponent<Point>().GetState())
            {
                return true;
            }
        }

        return false;
    }

    public void GenerateGhost(GameObject initialPoint)
    {
        GameObject ghost = ObjectPool.SharedInstance.GetPooledObject();
        if (ghost != null)
        {
            //Posicion es controlada por la animacion inicial
            //ghost.transform.position = ghostInitialPosition;
            //ghost.transform.rotation = new Quaternion(0,0,0,0);
            ghost.GetComponent<Ghost>().initialPoint = initialPoint;
            ghost.GetComponent<Ghost>().InitialConfig();
            ghost.SetActive(true);
        }
    }
}
