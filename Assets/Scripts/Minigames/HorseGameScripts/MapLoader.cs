using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour {

    // Use this for initialization
    public GameObject[] maps;
    GameObject currentMap;
    void Awake()
    {
        currentMap = Instantiate(maps[Random.Range(0, 4)], Vector3.zero, new Quaternion(0, 0, 0, 0));
    }
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
