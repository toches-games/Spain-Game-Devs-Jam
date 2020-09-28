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
    [Range(7, 25)]
    [SerializeField]
    int velocity;
    GameObject target;
    [SerializeField]
    public GameObject initialPoint;
    public GhostState currentGhostState = GhostState.generated;
    public Animator anim;
    public Vector3 initPosition;

    public bool endAnimation;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {

        if (currentGhostState != GhostState.finish)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position,
                                                 velocity * Time.deltaTime);
        }
        //Debug.Log(target.name);
        //Debug.Log(transform.position);

    }

    private void Update()
    {
        /**
        if (endAnimation)
        {
            anim.applyRootMotion = true;
        }
        else
        {
            anim.applyRootMotion = false;
        }
        **/
    }

    public void SetGhostState(GhostState newGhostState)
    {
        currentGhostState = newGhostState;

        if (newGhostState == GhostState.generated)
        {
            SoundController.Instance.ghost.Play();
            SoundController.Instance.ghost.EventInstance.setParameterByName("Gosth", 0);
            target = initialPoint;
            initialPoint.GetComponent<Point>().SetState(false);
            velocity = GameManager.sharedIstance.minVelocityGhost;
            gameObject.layer = LayerMask.NameToLayer("Default");
            if(anim != null)
            {
                anim.SetFloat("WalkVelocity", 0.8f);

            }
        }
        else if (newGhostState == GhostState.inPosition)
        {
            target = GameObject.Find("Player");
            velocity = Random.Range(GameManager.sharedIstance.minVelocityGhost, 
                                    GameManager.sharedIstance.maxVelocityGhost);
            anim.SetFloat("WalkVelocity", velocity/10 - 0.3f);

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
            //SetGhostState(GhostState.finish);
            anim.SetFloat("DeadVelocity", 2.5f);
            anim.SetTrigger("Dead");
            GameManager.sharedIstance.PlayerDamage();
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
        //endAnimation = false;
        transform.position = initPosition;
        anim.applyRootMotion = false;
    }

    public void InitialConfig()
    {
        target = initialPoint;
        initialPoint.GetComponent<Point>().SetState(false);
        currentGhostState = GhostState.generated;
        velocity = GameManager.sharedIstance.minVelocityGhost;
        gameObject.layer = LayerMask.NameToLayer("Default");
        //anim.SetBool("Dead", false);


    }

    public void Damage()
    {
        //anim.SetBool("Dead", true);

        //anim.enabled = true;
        anim.SetFloat("DeadVelocity", 1.8f);
        anim.SetTrigger("Dead");
        currentGhostState = GhostState.finish;
        SoundController.Instance.ghost.Play();
        SoundController.Instance.ghost.EventInstance.setParameterByName("Gosth", 1);
        //anim.Play("GhostDead");
    }
}
