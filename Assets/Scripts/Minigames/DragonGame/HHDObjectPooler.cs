using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HHDObjectPooler : MonoBehaviour {

	// Setting variables
	public GameObject pooledObject;
	public int pooledAmount;
    private GameObject poolContainer;
    List<GameObject> pooledObjects = new List<GameObject>();

    void Awake()
    {
        poolContainer = new GameObject();
        poolContainer.name = pooledObject.name + " Container";
    }

	// Use this for initialization
	void Start () {
        for (int i = 0; i < pooledAmount; i++)
		{
			GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.transform.parent = poolContainer.transform;
            obj.SetActive(false);
			pooledObjects.Add(obj);
		}
	}

	// Update is called once per frame
	void Update () {
        //test
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
        obj.transform.parent = poolContainer.transform;
        obj.SetActive(false);
		pooledObjects.Add(obj);
		return obj;
	}
}