using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner: MonoBehaviour
{

    public ObjectPooler ObstaclePool;
    public List<Sprite> spritePictures;
    public int numSpawned;

    void SpawnObstacle()
    {
        int counter = 0;
        while (counter++ < numSpawned)
        {
            GameObject NewObstacle = ObstaclePool.GetPooledObject();
            NewObstacle.transform.position = transform.position;
            NewObstacle.SetActive(true); //all initially not active
            NewObstacle.GetComponent<SpriteRenderer>().sprite = spritePictures[Random.Range(0, spritePictures.Count -1)];
            this.transform.Translate(6f, 0f, 0f);
        }
        GameObject LastTile = ObstaclePool.GetPooledObject();
        LastTile.transform.position = transform.position;
        LastTile.SetActive(true); //all initially not active
        LastTile.GetComponent<SpriteRenderer>().sprite = spritePictures[spritePictures.Count - 1];
    }
    // Use this for initialization
    void Start()
    {
        SpawnObstacle();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
