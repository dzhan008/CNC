using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {
	// Use this for initialization
	Transform Ground;
	void Start () {
		Ground = transform.FindChild ("Background");
	}
	
	// Update is called once per frame
	void Update () {
		//Ground.transform.Translate (-0.1f, 0f, 0f);
	}
}
