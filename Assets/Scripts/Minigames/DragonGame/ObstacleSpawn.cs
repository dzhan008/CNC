using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour {

    public HHDObjectPooler ObstaclePool;
    public List<Sprite> spritePictures;
    public GameObject game;

    IEnumerator SpawnObstacle()
    {
        bool spawn = game.GetComponent<DragonMiniGame>().StartGame;
        while (true)
        {
            if (spawn)
            {
                GameObject NewObstacle = ObstaclePool.GetPooledObject();
                NewObstacle.transform.position = transform.position;
                NewObstacle.SetActive(true); //all initially not active
                NewObstacle.GetComponent<SpriteRenderer>().sprite = spritePictures[Random.Range(0, spritePictures.Count)];
                spawn = game.GetComponent<DragonMiniGame>().StartGame;
                yield return new WaitForSeconds(Random.Range(1, 3));
            }
            else
            {
                spawn = game.GetComponent<DragonMiniGame>().StartGame;
                yield return new WaitForSeconds(1);
            }
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
