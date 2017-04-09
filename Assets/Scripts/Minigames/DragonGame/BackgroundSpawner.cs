using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner: MonoBehaviour
{
    public GameObject endWall;
    public GameObject LastTile;
    public HHDObjectPooler ObstaclePool;
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
        if (LastTile)
        {
            GameObject lastTile = (GameObject)Instantiate(LastTile);
            lastTile.transform.position = transform.position;
            lastTile.transform.position = new Vector3(transform.position.x - 0.3f, transform.position.y + 0.13f, transform.position.z);
            lastTile.name = "LastTile";
        }
        //GameObject LastTile = ObstaclePool.GetPooledObject();
        //LastTile.transform.position = transform.position;
        //LastTile.SetActive(true); //all initially not active
        //LastTile.GetComponent<SpriteRenderer>().sprite = spritePictures[spritePictures.Count - 1];
        //add in gameobject 
        if (endWall)
        {
            GameObject wall = (GameObject)Instantiate(endWall);
            wall.name = "GoalWall";
            wall.transform.position = this.transform.position;
        }
        
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
