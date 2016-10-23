using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class random_spawn : MonoBehaviour {
	public GameObject Block_0;
	public GameObject Block_1;

	private int index = 0;
	private int number_of_blocks = 20;
	private int[] x_positions = new int[20];
	private int[] y_positions = new int[20] ;

	// Use this for initialization
	void Start () {
		// Call initial Q1 spawn, then reflect horizontal and virtical.
		Create_Blocks ();
		Reflect_Horizontal ();
		Reflect_Virtical ();
		Debug_Check ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void Create_Blocks () {
		// Loop until all number of number_of_blocks is spawned.
		for (int i = 0; i < number_of_blocks; i++) {
			if (i % 2 == 0) {
				Instantiate (Block_0, Random_Position (), Quaternion.identity);
			} 
			else {
				//GameObject new_block = (GameObject)(Instantiate (Block_1, Random_Position (), Quaternion.identity));
				//new_block.GetComponent<block_status> ().id = 1;
				Instantiate (Block_1, Random_Position (), Quaternion.identity);

			}
			index++;
		}
	}

	// Random Position generator.
	Vector3 Random_Position () {
		// Spawn a random position for x and y, z stays 0.
		int x = UnityEngine.Random.Range (1, 15);
		int y = UnityEngine.Random.Range (1, 5);
		if (index != 0) {
			for (int i = 0; i < x_positions.Length; i++) {
				Debug.Log ("i " + i);
				// If the position was already taken.
				if (x == x_positions [i] && y == y_positions [i]) {
					Debug.Log ("same" + x);
					x = UnityEngine.Random.Range (1, 15);
					y = UnityEngine.Random.Range (1, 5);
					Debug.Log ("after" + x);
					// Recheck
					i = -1;
				}
			} 
		}
		// Set new coordinates
		x_positions [index] = x;
		y_positions [index] = y;
		return new Vector3 (x, y, 0);
	}

	void Reflect_Horizontal () {
		for (int i = 0; i < number_of_blocks; i++) {
			if (i % 2 == 0) {
				Instantiate (Block_0, new Vector3 (x_positions [i] - (2 * x_positions [i]), y_positions [i], 0), Quaternion.identity);
			} 
			else {
				Instantiate (Block_1, new Vector3 (x_positions [i] - (2 * x_positions [i]), y_positions [i], 0), Quaternion.identity);
			}
		}
	}

	void Reflect_Virtical () {
		for (int i = 0; i < number_of_blocks; i++) {
			if (i % 2 == 0) {
				Instantiate (Block_0, new Vector3 (x_positions [i], y_positions [i] - (2 * y_positions [i]), 0), Quaternion.identity);
				Instantiate (Block_0, new Vector3 (x_positions [i] - (2 * x_positions [i]), y_positions [i] - (2 * y_positions [i]), 0), Quaternion.identity);
			} 
			else {
				Instantiate (Block_1, new Vector3 (x_positions [i], y_positions [i] - (2 * y_positions [i]), 0), Quaternion.identity);
				Instantiate (Block_1, new Vector3 (x_positions [i] - (2 * x_positions [i]), y_positions [i] - (2 * y_positions [i]), 0), Quaternion.identity);

			}

		}
	}

	void Debug_Check () {
		for (int i = 0; i < x_positions.Length; i++) {
			Debug.Log ("x: " + x_positions [i] + "\n" + "y: " + y_positions [i]);
		}
	}

}
