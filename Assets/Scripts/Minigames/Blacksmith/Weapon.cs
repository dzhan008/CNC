    using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    [SerializeField]
    private int PointsLeft;
    [SerializeField]
    private int SpawnPoints;

    private Transform TopSpawn;
    private Transform BotSpawn;

    private GameObject Parent;
    private GameObject HitPoint;

    private bool _Completed = false;
    public bool Completed
    {
        get
        {
            return _Completed;
        }
        set
        {
            _Completed = value;
        }
    }

	// Use this for initialization
	void Start () {
        Parent = GameObject.Find("Swift Smiths");
        HitPoint = GameObject.Find("HitPoint");
        TopSpawn = gameObject.transform.FindChild("Top").transform;
        BotSpawn = gameObject.transform.FindChild("Bottom").transform;

        float length = gameObject.GetComponent<SpriteRenderer>().bounds.min.y;
        float max_length = gameObject.GetComponent<SpriteRenderer>().bounds.max.y;

        for (int i = 0; i < SpawnPoints; i++)
        {
            CreateNewSpawn();
        }
    }

    void CreateNewSpawn()
    {
        HitPoint.transform.position = new Vector2(HitPoint.transform.position.x, Random.Range(BotSpawn.position.y, TopSpawn.position.y));
    }
	
	// Update is called once per frame
	void Update () {
        if (PointsLeft <= 0)
        {
            Completed = true;
        }
        else if(HitPoint.GetComponent<HitPoint>().Tempered)
        {
            HitPoint.GetComponent<HitPoint>().ResetHealth();
            PointsLeft--;
            Debug.Log(PointsLeft);
            if(PointsLeft != 0)
            CreateNewSpawn();
        }
	
	}

    public void ResetWeapon()
    {
        PointsLeft = 5;
        Completed = false;
    }
}
