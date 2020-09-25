using DigitalRuby.LightningBolt;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
    LightningBoltScript bolt;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        bolt = FindObjectOfType<LightningBoltScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20));
        transform.position = new Vector3(worldPoint.x, worldPoint.y, transform.position.z);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 origin = Camera.main.transform.position;
        Vector3 target = this.transform.position;
        Vector3 dir = (target - origin).normalized;
        //Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);
        //Debug.DrawRay(origin, dir * 100, Color.green);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = BulletRaycast.Shoot(origin, dir, 200);
            //BulletRaycast.Shoot(ray.origin, ray.direction, 200);

            if(hit.collider != null)
            {
                dir = (hit.collider.GetComponent<Transform>().position - origin).normalized;
                target = hit.collider.GetComponent<Transform>().position;
            }

            bolt.EndPosition = target;
            StartCoroutine(ActiveBolt());

            //Debug.DrawRay(origin, target * 200, Color.green);
            Debug.DrawRay(origin, dir * 200, Color.green);
        }

        IEnumerator ActiveBolt()
        {
            bolt.GetComponent<LineRenderer>().enabled = true;
            yield return new WaitForSeconds(.15f);
            bolt.GetComponent<LineRenderer>().enabled = false;


        }

        /**
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        mouseX = Mathf.Clamp(mouseX, -clamp, clamp);
        mouseY = Mathf.Clamp(mouseY, -clamp, clamp);
        transform.position = transform.position + new Vector3(mouseX, mouseY, 0f);
        **/

    }
}
