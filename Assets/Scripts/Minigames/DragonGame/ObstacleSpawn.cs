using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour {

    public ObjectPooler ObstaclePool;
    public List<Sprite> spritePictures;

    IEnumerator SpawnObstacle()
    {
        while (true)
        {
            GameObject NewObstacle = ObstaclePool.GetPooledObject();
            NewObstacle.transform.position = transform.position;
            NewObstacle.SetActive(true); //all initially not active
            NewObstacle.GetComponent<SpriteRenderer>().sprite = spritePictures[Random.Range(0, spritePictures.Count)]; 
            yield return new WaitForSeconds(Random.Range(1, 3));

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
