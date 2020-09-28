using DigitalRuby.LightningBolt;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class PlayerAimWeapon : MonoBehaviour
{
    //LightningBoltScript bolt;

    public Light2D lightParent; 
    public Light2D lightChild;

    Vector3 origin;
    Vector3 target;
    Vector3 dir;

    void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        //bolt = FindObjectOfType<LightningBoltScript>();

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = BulletRaycast.Shoot(origin, dir, 200);
            //BulletRaycast.Shoot(ray.origin, ray.direction, 200);
            SoundController.Instance.laserShoot.Play();

            if (hit.collider != null)
            {
                dir = (hit.collider.GetComponent<Transform>().position - origin).normalized;
                target = hit.collider.GetComponent<Transform>().position;
            }

            //bolt.EndPosition = target;
            //StartCoroutine(ActiveBolt());
            StartCoroutine(LightsChangeIntensity());
            //Debug.DrawRay(origin, target * 200, Color.green);
            Debug.DrawRay(origin, dir * 200, Color.green);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.sharedIstance.isPlaying)
        {
            return;
        }

        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20));
        transform.position = new Vector3(worldPoint.x, worldPoint.y, transform.position.z);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        origin = Camera.main.transform.position;
        target = this.transform.position;
        dir = (target - origin).normalized;
        //Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);
        //Debug.DrawRay(origin, dir * 100, Color.green);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = BulletRaycast.Shoot(origin, dir, 200);
            //BulletRaycast.Shoot(ray.origin, ray.direction, 200);
            SoundController.Instance.laserShoot.Play();

            if(hit.collider != null)
            {
                dir = (hit.collider.GetComponent<Transform>().position - origin).normalized;
                target = hit.collider.GetComponent<Transform>().position;
            }

            //bolt.EndPosition = target;
            //StartCoroutine(ActiveBolt());
            StartCoroutine(LightsChangeIntensity());
            //Debug.DrawRay(origin, target * 200, Color.green);
            Debug.DrawRay(origin, dir * 200, Color.green);
        }

        /**
        IEnumerator ActiveBolt()
        {
            bolt.GetComponent<LineRenderer>().enabled = true;
            yield return new WaitForSeconds(.15f);
            bolt.GetComponent<LineRenderer>().enabled = false;
        }
        **/
        /**
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        mouseX = Mathf.Clamp(mouseX, -clamp, clamp);
        mouseY = Mathf.Clamp(mouseY, -clamp, clamp);
        transform.position = transform.position + new Vector3(mouseX, mouseY, 0f);
        **/
    }

    IEnumerator LightsChangeIntensity()
    {

        lightParent.intensity = GameManager.INTENSITY_HIGHT_CIRCULO;
        lightChild.intensity = GameManager.INTENSITY_HIGHT_CONO;

        yield return new WaitForSeconds(.15f);

        lightParent.intensity = GameManager.INTENSITY_DEFAULT_CIRCULO;
        lightChild.intensity = GameManager.INTENSITY_DEFAULT_CONO;
        yield break;

    }
}
