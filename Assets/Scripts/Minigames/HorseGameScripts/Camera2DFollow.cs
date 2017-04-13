using UnityEngine;
using System.Collections;

public class Camera2DFollow : MonoBehaviour {
	
	public Transform target;
	public float damping = 1;
	public float lookAheadFactor = 3;
	public float lookAheadReturnSpeed = 0.5f;
	public float lookAheadMoveThreshold = 0.1f;
    public GameObject Player1;
    public GameObject Player2;
	float offsetZ;
	Vector3 lastTargetPosition;
	Vector3 currentVelocity;
	Vector3 lookAheadPos;
    bool transition = false;
    float elapsed;
    float duration = 1;
    public float CamSize
    {
        get
        {
            return cam.orthographicSize;
        }
        set
        {
            cam.orthographicSize = value;
            if (value <= 5)
            {
                cam.orthographicSize = 5;
            }
        }
    }
    private float _CamSize;
	float nextTimeToSearch = 0;
    Camera cam;

	// Use this for initialization
	void Start () {
		lastTargetPosition = target.position;
		offsetZ = (transform.position - target.position).z;
		transform.parent = null;
        cam = GetComponent<Camera>();
        _CamSize = cam.orthographicSize;
	}

    public float n = 2;
    public float min = 5;
    public float max = 0;
	// Update is called once per frame
	void Update () {
        Vector3 midpoint = new Vector3(0, 0, 10);
        float Distance = Vector3.Distance(Player1.transform.position, Player2.transform.position);
        this.transform.position = (Player1.transform.position - Player2.transform.position) * 0.5f + Player2.transform.position - midpoint;
        //The camera will always be in the mid point between Player 1 and 2
        //Debug.Log(this.transform.position + "  " + Distance );
        //cam.orthographicSize = CamSize;
        transition = true;
        float x = (Distance / 2 + 0.5f);
        if (Distance <= 9 && CamSize > 5)
        {
            elapsed += Time.deltaTime / duration;
            CamSize = Mathf.SmoothStep(CamSize, 5, elapsed);
            //Debug.Log("Elaped: " + elapsed);
            if (elapsed > 1.0)
            {
                elapsed = 0;
            }
        }
        if (Distance > 9)
        {
            CamSize = 5 + Distance / 2;
            if (Player1.GetComponent<SpriteRenderer>().isVisible && Player2.GetComponent<SpriteRenderer>().isVisible)
            {

            }
            else
            {
                elapsed += Time.deltaTime / duration;
                CamSize = Mathf.SmoothStep(5, x, elapsed);
                if (elapsed > 1.0)
                {
                    elapsed = 0;
                }
            }
        }
        //if (transition)
        //{
        //    elapsed += Time.deltaTime / duration;
        //    Debug.Log("Elaped: " + elapsed);
        //    CamSize = Mathf.SmoothStep(5, 20, elapsed);
        //    if (elapsed > 1.0f)
        //    {
        //        transition = false;
        //        elapsed = 0.0f;
        //    }
        //}


    }
    

	void FindPlayer () {
		if (nextTimeToSearch <= Time.time) {
			GameObject searchResult = GameObject.FindGameObjectWithTag ("Player");
			if (searchResult != null)
				target = searchResult.transform;
			nextTimeToSearch = Time.time + 0.5f;
		}
	}
    IEnumerator Expand()
    {
        yield return null;
    }
}
