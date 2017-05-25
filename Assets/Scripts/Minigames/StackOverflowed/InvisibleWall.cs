using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WallType
{
    left = 0,
    right
}

public class InvisibleWall : MonoBehaviour {

    public WallType Type;
    public float PushAmount;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if(other.tag == "BookHolder")
        {
            Transform parent = other.transform.parent;
            if(Type == WallType.left)
            {
                parent.position = new Vector2(parent.position.x + PushAmount, parent.position.y);
            }
            else if(Type == WallType.right)
            {
                parent.position = new Vector2(parent.position.x - PushAmount, parent.position.y);
            }
        }
    }
}
