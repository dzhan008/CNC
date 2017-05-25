using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum MonsterType
{
    Normal,
    Twig,
    Tank
}

public class Monster : MonoBehaviour {

    public int health = 500;
    private GameObject DestructPoint;
    private float WalkSpeed = -.01f;
    private float xSpeed;
    public float count = 1.5f;
    private int rand;
    private float rand1;
    private string monType;
    bool isReady = true;
    MonsterType type;

    // Use this for initialization
    void Start()
    {
        DestructPoint = GameObject.Find("MonsterPass");
        rand = Random.Range(0, 3);
        rand1 = Random.Range(0f, 1f);

        if (rand1 <= .3f)
        {
            type = MonsterType.Twig;
            health = 300;
            WalkSpeed = WalkSpeed * (2f);
            transform.localScale = new Vector3(2, 5, 1);
        }

        else if (rand1 <= .5f)
        {
            type = MonsterType.Tank;
            health = 700;
            WalkSpeed = WalkSpeed * (.5f);
            transform.localScale = new Vector3(10, 5, 1);

        }

        else if (rand1 < 1f)
        {
            type = MonsterType.Normal;
            health = 500;
            WalkSpeed = -.01f;
            transform.localScale = new Vector3(5, 5, 1);
        }


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            if (other.gameObject.name == "Bullet(Clone)")
            {
                health = health - other.gameObject.GetComponent<Bullet>().damage;
            }
            else if (other.gameObject.name == "Bullet 1(Clone)")
            {
                health = health - other.gameObject.GetComponent<Bullet1>().damage;
            }
            //Debug.Log(health);
            if (health <= 0)
            {
                if (type  == MonsterType.Tank)
                {
                    health = 700;
                }

                else if (type == MonsterType.Twig)
                {
                    health = 300;
                }

                else if (type == MonsterType.Normal)
                {
                    health = 500;
                }
                
                GameObject Hunting = GameObject.Find("HuntingMinigame");
                //Debug.Log (other.gameObject.name + "==" + "Bullet(Clone)");
                if (other.gameObject.name == "Bullet(Clone)") {
                    if (type == MonsterType.Tank)
                    {
                        Debug.Log("score: " + GameManager.Instance.Players[1].Value.MiniGameScore);
                        GameManager.Instance.Players[1].Value.MiniGameScore += 20;
                        Hunting.GetComponent<HuntingMinigame>().SetScoreP1();
                        Debug.Log("Kill by Player 1");
                    }
                    else if (type == MonsterType.Twig)
                    {
                        Debug.Log("score: " + GameManager.Instance.Players[1].Value.MiniGameScore);
                        GameManager.Instance.Players[1].Value.MiniGameScore += 5;
                        Hunting.GetComponent<HuntingMinigame>().SetScoreP1();
                        Debug.Log("Kill by Player 1");
                    }

                    else if (type == MonsterType.Normal)
                    {
                        Debug.Log("score: " + GameManager.Instance.Players[1].Value.MiniGameScore);
                        GameManager.Instance.Players[1].Value.MiniGameScore += 10;
                        Hunting.GetComponent<HuntingMinigame>().SetScoreP1();
                        Debug.Log("Kill by Player 1");
                    }
                }
                else if (other.gameObject.name == "Bullet 1(Clone)") {
                    if (type == MonsterType.Tank)
                    {
                        Debug.Log("score: " + GameManager.Instance.Players[2].Value.MiniGameScore);
                        GameManager.Instance.Players[2].Value.MiniGameScore += 20;
                        Hunting.GetComponent<HuntingMinigame>().SetScoreP2();
                        Debug.Log("Kill by Player 2");
                    }
                    else if (type == MonsterType.Twig)
                    {
                        Debug.Log("score: " + GameManager.Instance.Players[2].Value.MiniGameScore);
                        GameManager.Instance.Players[2].Value.MiniGameScore += 5;
                        Hunting.GetComponent<HuntingMinigame>().SetScoreP2();
                        Debug.Log("Kill by Player 2");
                    }

                    else if (type == MonsterType.Normal)
                    {
                        Debug.Log("score: " + GameManager.Instance.Players[2].Value.MiniGameScore);
                        GameManager.Instance.Players[2].Value.MiniGameScore += 10;
                        Hunting.GetComponent<HuntingMinigame>().SetScoreP2();
                        Debug.Log("Kill by Player 2");
                    }
                }
                //				Debug.Log ("Hit");
                gameObject.SetActive(false);
            }
            other.gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Random monster movement:" + rand);
        //count = count - Time.deltaTime;
        if (this.gameObject.transform.position.y < DestructPoint.transform.position.y)
        {
            gameObject.SetActive(false);
        }

        else
        {
            if (isReady)
            {
                StartCoroutine(cRandom());
            }
            xSpeed = -.01f;
            gameObject.transform.Translate(xSpeed, WalkSpeed, 0);
            //if (count <= 0)
            //{
                if (rand == 0)
                {
                    //count = 1.5f;
                    xSpeed = xSpeed * (-1);
                    gameObject.transform.Translate(xSpeed, WalkSpeed, 0);
                }

                else if (rand == 1)
                {
                    //count = 1.5f;
                    xSpeed = xSpeed * 0;
                    gameObject.transform.Translate(xSpeed, WalkSpeed, 0);
                }

                else if (rand == 2)
                {
                    //count = 1.5f;
                    xSpeed = xSpeed * 1;
                    gameObject.transform.Translate(xSpeed, WalkSpeed, 0);

                }

            //}

        }

    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Bullet")
        {

        }
    }

    IEnumerator cRandom()
    {
        isReady = false;
        rand = Random.Range(0, 3);
        yield return new WaitForSeconds(2);
        isReady = true;
    }
}
