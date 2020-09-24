using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 20));
        transform.position = new Vector3(worldPoint.x, worldPoint.y, transform.position.z);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 origin = Camera.main.transform.position;
        Vector3 target = this.transform.position;
        Vector3 dir = (target - origin).normalized;
        //Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);
        Debug.DrawRay(origin, dir * 100, Color.green);

        if (Input.GetMouseButtonDown(0))
        {
            BulletRaycast.Shoot(origin, dir, 200);
            //BulletRaycast.Shoot(ray.origin, ray.direction, 200);
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
