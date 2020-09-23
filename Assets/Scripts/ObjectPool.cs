using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            tmp.transform.parent = GameObject.Find("Ghosts").transform;
            pooledObjects.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
    /** Codigo para script donde se vaya a instanciar el objeto
    
    GameObject bullet = ObjectPool.SharedInstance.GetPooledObject(); 
    if (bullet != null) 
    { 
        bullet.transform.position = turret.transform.position; 
        bullet.transform.rotation = turret.transform.rotation; 
        bullet.SetActive(true); 
    }

    //Dentro del script del objeto a instanciar para destruirlo lo desactivamos
    gameobject.SetActive(false);
    **/
    
}
 