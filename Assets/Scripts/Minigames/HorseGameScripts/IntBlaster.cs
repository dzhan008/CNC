using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntBlaster : MonoBehaviour {

    // Use this for initialization
    public GameObject Target; //reference to target player
    GameObject Fireball;
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
            if (Fireball == null)
            {
                SpawnFire();
            }
            else
            {
                Debug.Log("Fireball exists");
            }
            
        }
	}
    void SpawnFire()
    {
        Vector2 directionVector = this.transform.right;

        Fireball = Instantiate(Projectile, SpawnLocation.position, SpawnLocation.rotation) as GameObject;
        Fireball.GetComponent<FireBehavior>().Target = Target;
        Fireball.GetComponent<FireBehavior>().TrackingDistance = 10 - this.GetComponent<Stats>().Intel;
        Fireball.GetComponent<FireBehavior>().lifetime = this.GetComponent<Stats>().Intel * 0.5f;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
    }
}
