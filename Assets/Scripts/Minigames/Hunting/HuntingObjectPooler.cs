using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HuntingObjectPooler : MonoBehaviour {

    // Setting variables
    public GameObject pooledObject;
    public GameObject container;
    public int pooledAmount;

    List<GameObject> pooledObjects;

	// Use this for initialization
	void Start () {
        pooledObjects = new List<GameObject>();
        container = new GameObject();
        container.name = pooledObject.name + "Container";

        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.transform.SetParent(container.transform);
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
            if (!pooledObjects[i].activeInHierarchy) 
            {
                return pooledObjects[i];
            }
        }
        GameObject obj = (GameObject)Instantiate(pooledObject);
        obj.transform.SetParent(container.transform);
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }
}
