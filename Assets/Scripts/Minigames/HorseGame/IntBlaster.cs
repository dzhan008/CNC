using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntBlaster : MonoBehaviour {

    // Use this for initialization
    public GameObject Target; //reference to target player
    public string player; //player to be attacked
    public string spawnlocation;
    public Transform SpawnLocation; //projectile spawn location
    public GameObject Projectile;
	void Start () {
        Target = GameObject.Find(player);
        SpawnLocation = GameObject.Find(spawnlocation).transform;
        
    }
    Vector3 distance;
	// Update is called once per frame
    
	void Update () {
        distance = Target.transform.position - this.transform.position;
        if (distance.magnitude >= 5)
        {
            //spawn fireball;
            SpawnFire();
        }
	}
    void SpawnFire()
    {
        Vector2 directionVector = this.transform.right;

        GameObject Fireball = Instantiate(Projectile, SpawnLocation.position, SpawnLocation.rotation) as GameObject;
        Fireball.GetComponent<FireBehavior>().Target = Target;
        Fireball.GetComponent<FireBehavior>().TrackingDistance = 10 - this.GetComponent<Stats>().Intel;
    }
}
