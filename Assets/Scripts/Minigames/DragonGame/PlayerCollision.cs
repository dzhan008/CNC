using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{

    public PlayerStat player;
    public int Id;
    public bool CanJump;

    public void Initialize(int ID, PlayerStat player)
    {
        Id = ID;
        this.player = player;
    }
    IEnumerator slowDuration(float slow)
    {
        player.PSkills["playerSlowAdd"] = player.PSkills["speedReduction"] + slow;
        float slowDurationTime = player.PSkills["sprintSlowDuration"];
        yield return new WaitForSeconds(slowDurationTime);
        player.PSkills["playerSlowAdd"] = 0;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        //check if game over
        if (other.gameObject.tag.Equals("DeathWall"))
            Debug.Log("Death Wall!!!!");

        //check ground
        if (other.gameObject.tag.Equals("Ground") && Id == other.gameObject.GetComponent<Ground>().Id)
        {
            CanJump = true;
        }
        //check obstacle
        if (other.gameObject.tag.Equals("Obstacle"))
        {
            other.gameObject.SetActive(false);
            StartCoroutine(slowDuration(0));
        }
        //check cock block
        if (other.gameObject.tag.Equals("CockBlock"))
        {
            other.gameObject.SetActive(false);
            StartCoroutine(slowDuration(0.04f));
        }
        //checks end goal
        if (other.gameObject.tag.Equals("EndGoal"))
        {
            Debug.Log("MOSHI MOSHI");
            gameObject.GetComponent<Stats>().MiniGameScore++;
            GameObject game = GameObject.Find("DragonGame");
            game.GetComponent<DragonMiniGame>().IsGameEnd = true;

        }
    }
    /*
        void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.tag.Equals("Ground") && Id == other.gameObject.GetComponent<Ground>().Id)
                CanJump = false;
       }
       */
}