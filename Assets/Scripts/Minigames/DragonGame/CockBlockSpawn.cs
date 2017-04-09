using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CockBlockSpawn : MonoBehaviour {
    public HHDObjectPooler ObstaclePool;

    public void spawnCockBlock()
    {
        GameObject NewObstacle = ObstaclePool.GetPooledObject();
        NewObstacle.transform.position = transform.position;
        NewObstacle.GetComponent<Rigidbody2D>().AddTorque(5000000);
        NewObstacle.SetActive(true); //all initially not active
    }
}
