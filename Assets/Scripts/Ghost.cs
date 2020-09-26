using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GhostState
{
    generated,
    inPosition,
    finish
}

public class Ghost : MonoBehaviour
{
    [Range(7,25)]
    [SerializeField]
    int velocity;
    GameObject target;
    [SerializeField]
    public GameObject initialPoint;
    public GhostState currentGhostState = GhostState.generated;
    Animator anim;

    public bool endAnimation;
    // Start is called before the first frame update
    void Start()
    {        
    }

    private void FixedUpdate()
    {

        if(currentGhostState != GhostState.finish)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position,
                                                 velocity * Time.deltaTime);
        }

        if (endAnimation)
        {
            anim.applyRootMotion = true;
        }
        else
        {
            anim.applyRootMotion = false;
        }

    }

    public void SetGhostState(GhostState newGhostState)
    {
        currentGhostState = newGhostState;

        if (newGhostState == GhostState.generated)
        {
            target = GameManager.sharedIstance.points[Random.Range(0, GameManager.MAX_COUNT_GHOST)];

        }
        else if (newGhostState == GhostState.inPosition)
        {
            target = GameObject.Find("Player");
            velocity = Random.Range(GameManager.sharedIstance.minVelocityGhost, 
                                    GameManager.sharedIstance.maxVelocityGhost);
            gameObject.layer = LayerMask.NameToLayer("Ghost");
        }
        else if (newGhostState == GhostState.finish)
        {
            //TO DO animation
            initialPoint.GetComponent<Point>().SetState(true);
            //StartCoroutine(GameManager.sharedIstance.GenerationGhost(Random.Range(2f, 4f), 2, 10, 20));
            //GameManager.sharedIstance.GenerateGhost(10,20);
            //GameManager.sharedIstance.Coroutine(Random.Range(2f, 4f), 1, 10, 20);
            ResetGameObject();

            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            //TO DO bajar vida al personaje
            //GameManager.sharedIstance
            SetGhostState(GhostState.finish);
        }

        if (other.CompareTag("InPosition"))
        {
            SetGhostState(GhostState.inPosition);
            
        }
    }

    public void SetVelocity(int velocity)
    {
        this.velocity = velocity;
    }

    void ResetGameObject()
    {
        //anim.enabled = true;
        endAnimation = false;
    }

    public void InitialConfig()
    {
        target = initialPoint;
        initialPoint.GetComponent<Point>().SetState(false);
        currentGhostState = GhostState.generated;
        velocity = 7;
        gameObject.layer = LayerMask.NameToLayer("Default");
        anim = GetComponent<Animator>();               
        //anim.SetBool("Dead", false);


    }

    public void Damage()
    {
        //anim.SetBool("Dead", true);

        //anim.enabled = true;
        anim.SetTrigger("Dead");
        currentGhostState = GhostState.finish;

        //anim.Play("GhostDead");
    }
}
