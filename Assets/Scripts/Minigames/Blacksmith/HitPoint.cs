using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HitPoint : MonoBehaviour {

    List<GameObject> PlayersInZone;

    [SerializeField]
    private int MaxHealth;
    private int Health;

    private bool _Tempered = false;
    public bool Tempered
    {
        get
        {
            return _Tempered;
        }
        set
        {
            _Tempered = value;
        }
    }

	// Use this for initialization
	void Start () {
        PlayersInZone = new List<GameObject>();
        Health = MaxHealth;
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Health <= 0)
        {
            Tempered = true;
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayersInZone.Add(other.gameObject);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        PlayersInZone.Remove(other.gameObject);
    }

    public void ResetHealth()
    {
        Health = MaxHealth;
        Tempered = false;
    }

    public void Damage(int damage)
    {
        Health -= damage;
    }

    public bool InZone(GameObject player)
    {
        return PlayersInZone.Contains(player);
    }
}
