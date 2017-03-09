using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
   
    public PlayerStat player;
    public int Id;
    public bool CanJump;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator slowDuration()
    {
        player.PSkills["playerSlowAdd"] = player.PSkills["speedReduction"];
        float slowDurationTime = player.PSkills["sprintSlowDuration"];
        yield return new WaitForSeconds(slowDurationTime);
        player.PSkills["playerSlowAdd"] = 0;
    }
    void OnCollisionStay2D(Collision2D other)
    {
        //check if game over
        if (other.gameObject.tag.Equals("DeathWall"))
            Debug.Log("Death Wall!!!!");

        //check ground
        if (other.gameObject.tag.Equals("Ground") && Id == other.gameObject.GetComponent<Ground>().Id)
            CanJump = true;


        //check obstacle
        if (other.gameObject.tag.Equals("Obstacle"))
        {
            other.gameObject.SetActive(false);
            StartCoroutine(slowDuration());
        }

        //checks end goal
        if (other.gameObject.tag.Equals("Goal"))
        {
            Debug.Log("WINNERRR."); 
        }
   }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Ground") && Id == other.gameObject.GetComponent<Ground>().Id)
            CanJump = false;
    }
}