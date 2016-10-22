using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour {

    // Setting variables
    public GameObject pooledObject;
    public int pooledAmount;

    List<GameObject> pooledObjects = new List<GameObject>();

	// Use this for initialization
	void Start () {
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects != null) 
            {
                return pooledObjects[i];
            }
        }
        GameObject obj = (GameObject)Instantiate(pooledObject);
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }
}
