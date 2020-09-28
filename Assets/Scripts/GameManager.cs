using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedIstance;
    public const int MAX_GENERATION_GHOST = 45; 
    public const int MAX_COUNT_GHOST = 6;
    public const float INTENSITY_DEFAULT_CIRCULO = 0.5f;
    public const float INTENSITY_HIGHT_CIRCULO = 2f;
    public const float INTENSITY_DEFAULT_CONO = 1f;
    public const float INTENSITY_HIGHT_CONO = 2.5f;
    //Posiciones aleatorias donde generar los fantasmas
    [SerializeField]
    public List<GameObject> points;

    [SerializeField]
    GameObject player;
    [SerializeField]
    private float generationTime;
    public int minVelocityGhost = 15;
    public int maxVelocityGhost = 20;
    public int limitForNextLevel = 15;

    int ghostsCount;

    public Light2D playerLight;

    [SerializeField]
    PlayableDirector outroBueno;

    [SerializeField]
    PlayableDirector outroMalo;

    public bool isPlaying = true;

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
        Camera.main.transform.position = new Vector3(0, 2.4f, -15f);
        Camera.main.transform.Rotate(new Vector3(15, 0, 0));
        ghostsCount = 0;
        StartCoroutine(GenerationGhost(4f));
    }

    private void FixedUpdate()
    {
        if (ghostsCount >= MAX_GENERATION_GHOST)
        {
            if (!ObjectPool.SharedInstance.CheckStateObjects())
            {
                StartCoroutine(PlayerWin());
            }
        }
    }

    public IEnumerator GenerationGhost(float initialWait)
    {
        yield return new WaitForSeconds(initialWait);

        while (ghostsCount < MAX_GENERATION_GHOST && isPlaying)
        {
            if (CheckStatePoints())
            {
                GenerateGhost(points[RandomPoint()]);
                ghostsCount++;

                DifficultyController(ghostsCount, limitForNextLevel);

                //Debug.Log("Generado: " + i);
                //Debug.Log("generationTime: " + generationTime);
                //Debug.Log("minVelocity: " + minVelocityGhost);
                //Debug.Log("maxVelocity: " + maxVelocityGhost);
            }
            yield return new WaitForSeconds(generationTime);
            //Debug.Log(i);
        }

        //yield return new WaitUntil(() => ObjectPool.SharedInstance.GetPooledObject() == null);


    }

    IEnumerator PlayerWin()
    {
        if (player.GetComponent<SpriteRenderer>().color.a >= 0.2f)
        {
            isPlaying = false;
            outroBueno.Play();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            MusicController.Instance.PlayBuriel();
            MusicController.Instance.StopRain();
            MusicController.Instance.StopMecanic();

            yield return new WaitForSeconds(2f);
            Camera.main.gameObject.transform.SetPositionAndRotation(new Vector3(0, 0, -15), new Quaternion(0, 0, 0, 0));

            GameObject.Find("Game").SetActive(false);
        }
    }

    IEnumerator PlayerLost()
    {
        
        isPlaying = false;
        outroMalo.Play();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        MusicController.Instance.PlayLose();
        MusicController.Instance.StopMecanic();

        yield return new WaitForSeconds(2f);
        Camera.main.gameObject.transform.SetPositionAndRotation(new Vector3(0, 0, -15), new Quaternion(0,0,0,0));

        GameObject.Find("Game").SetActive(false);
        //GameObject.Find("Instructions").SetActive(false);
        
    }

    public void DifficultyController(int ghostsCount, int limitForNextLevel)
    {
        if(ghostsCount == limitForNextLevel)
        {
            generationTime -= 0.3f;
            minVelocityGhost = maxVelocityGhost;
            maxVelocityGhost += 5;
            this.limitForNextLevel += 15;
        }
    }

    public int RandomPoint()
    {
        int random = 0;

        do
        {
            random = Random.Range(0, MAX_COUNT_GHOST);

        } while (!points[random].GetComponent<Point>().GetState());

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
            //ghost.GetComponent<Ghost>().InitialConfig();
            //ghost.GetComponent<Ghost>().anim = ghost.GetComponent<Animator>();

            ghost.GetComponent<Ghost>().SetGhostState(GhostState.generated);

            ghost.SetActive(true);
        }
    }

    public void PlayerDamage()
    {
        if (isPlaying)
        {
            StartCoroutine(DamageEffect());

            
        }
    }

    IEnumerator DamageEffect()
    {
        
        for (int i = 0; i < 5; i++)
        {
            float alpha = player.GetComponent<SpriteRenderer>().color.a;
            player.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, alpha - 0.0667f);

            playerLight.intensity = playerLight.intensity - 0.044f;

            yield return new WaitForSeconds(.1f);
        }

        if (player.GetComponent<SpriteRenderer>().color.a < 0.1f)
        {
            StartCoroutine(PlayerLost());
        }
    }
    
}
