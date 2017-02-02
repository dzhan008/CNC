using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour {

    public ObjectPooler ObstaclePool;
    IEnumerator SpawnObstacle()
    {
        while (true)
        {
            GameObject NewObstacle = ObstaclePool.GetPooledObject();
            NewObstacle.transform.position = transform.position;
            NewObstacle.SetActive(true); //all initially not active
            yield return new WaitForSeconds(2);
        }
    }

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnObstacle());
	}
	
	// Update is called once per frame
	void Update () {

	}
}
