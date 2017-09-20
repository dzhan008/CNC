using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CreationControllerScript : MonoBehaviour {

    public Stats Player1;
    public Stats Player2;
    public Text Player1Text;
    public Text Player2Text;
    public Text Player1FirstText;
    public Text Player1SecondText;
    public Text Player2FirstText;
    public Text Player2SecondText;
    public Text Player1ConfirmText;
    public Text Player2ConfirmText;
    public Button Player1Yes;
    public Button Player1No;
    public Button Player2Yes;
    public Button Player2No;

    public string GameState;
    private string Player1GameState;
    private string Player2GameState;

    private int Player1First;
    private int Player1Second;
    private int Player2First;
    private int Player2Second;

    public GameObject CharacterCanvas;

    public List<Roles> thePlayerRoles;
    public List<GameObject> PlayerModels;

    private int Player1Index;
    private int Player2Index;
    private bool Player1Confirm;
    private bool Player2Confirm;

    public GameObject Player1ModelHolder;
    public GameObject Player2ModelHolder;

    private GameObject Player1Model;
    private GameObject Player2Model;

    public Image Player1STRBar;
    public Image Player1DEXBar;
    public Image Player1INTBar;

    public Image Player2STRBar;
    public Image Player2DEXBar;
    public Image Player2INTBar;

    // Use this for initialization
    void Start () {
        Player1.Str = 0;
        Player1.Dex = 0;
        Player1.Intel = 0;

        Player2.Str = 0;
        Player2.Dex = 0;
        Player2.Intel = 0;

        GameState = "Choosing";
        Player1GameState = "Choosing";
        Player2GameState = "Choosing";

        Player1First = -1;
        Player1Second = -1;
        Player2First = -1;
        Player2Second = -1;

        Player1Index = 0;
        Player2Index = 0;
        Player1Confirm = true;
        Player2Confirm = true;

        Player1Text.text = thePlayerRoles[Player1Index].name;
        Player2Text.text = thePlayerRoles[Player2Index].name;

        Player1Model = Instantiate(PlayerModels[Player1Index], Player1ModelHolder.transform.position, Player1ModelHolder.transform.rotation);
        Player2Model = Instantiate(PlayerModels[Player2Index], Player2ModelHolder.transform.position, Player2ModelHolder.transform.rotation);
    }
	
	// Update is called once per frame
	void Update () {
        Player1STRBar.fillAmount = (float)Player1.Str / 10;
        Player1DEXBar.fillAmount = (float)Player1.Dex / 10;
        Player1INTBar.fillAmount = (float)Player1.Intel / 10;
        Player2STRBar.fillAmount = (float)Player2.Str / 10;
        Player2DEXBar.fillAmount = (float)Player2.Dex / 10;
        Player2INTBar.fillAmount = (float)Player2.Intel / 10;
        UpdateConfirm();
        UpdatePlayerRoles();
        UpdateIndexes();
    }

    public void UpdateRoles()
    {

        Player1Text.text = thePlayerRoles[Player1Index].name;
        Player2Text.text = thePlayerRoles[Player2Index].name;

        Destroy(Player1Model);
        Player1Model = Instantiate(PlayerModels[Player1Index], Player1ModelHolder.transform.position, Player1ModelHolder.transform.rotation);
        Destroy(Player2Model);
        Player2Model = Instantiate(PlayerModels[Player2Index], Player2ModelHolder.transform.position, Player2ModelHolder.transform.rotation);

        if (Player1First < 0)
        {
            Player1FirstText.text = "";
        }
        else
        {
            Player1FirstText.text = thePlayerRoles[Player1First].name;
        }
        if (Player1Second < 0)
        {
            Player1SecondText.text = "";
        }
        else
        {
            Player1SecondText.text = thePlayerRoles[Player1Second].name;
        }
        if (Player2First < 0)
        {
            Player2FirstText.text = "";
        }
        else
        {
            Player2FirstText.text = thePlayerRoles[Player2First].name;
        }
        if (Player2Second < 0)
        {
            Player2SecondText.text = "";
        }
        else
        {
            Player2SecondText.text = thePlayerRoles[Player2Second].name;
        }
    }

    public void UpdateConfirm()
    {
        if(Player1GameState == "Done" && Player2GameState == "Done")
        {
            Player1Model.transform.parent = Player1ModelHolder.transform.root;
            Player2Model.transform.parent = Player2ModelHolder.transform.root;
            Player1.SetModel(Player1Model);
            Player2.SetModel(Player2Model);
            Player1ModelHolder.SetActive(false);
            Player2ModelHolder.SetActive(false);
            CharacterCanvas.SetActive(false);
            UIManager.Instance.ShowMiniGameScreen();
            GameState = "Done";
        }

        if(Player1GameState == "Chosen")
        {
            Player1ConfirmText.gameObject.SetActive(true);
            Player1Yes.gameObject.SetActive(true);
            Player1No.gameObject.SetActive(true);
            
            if (Player1Confirm)
            {
                Player1Yes.image.color = Player1Yes.colors.highlightedColor;
                Player1No.image.color = Player1No.colors.normalColor;
            }
            else if (!Player1Confirm)
            {
                Player1Yes.image.color = Player1Yes.colors.normalColor;
                Player1No.image.color = Player1No.colors.highlightedColor;
            }
        }
        
        if(Player2GameState == "Chosen")
        {
            Player2ConfirmText.gameObject.SetActive(true);
            Player2Yes.gameObject.SetActive(true);
            Player2No.gameObject.SetActive(true);

            if (Player2Confirm)
            {
                Player2Yes.image.color = Player2Yes.colors.highlightedColor;
                Player2No.image.color = Player2No.colors.normalColor;
            }
            else if (!Player2Confirm)
            {
                Player2Yes.image.color = Player2Yes.colors.normalColor;
                Player2No.image.color = Player2No.colors.highlightedColor;
            }
        }
    }
    public void UpdatePlayerRoles()
    {
        


        if(Player1First == -1 && Player1Second == -1)
        {
            Player1.Str = 0;
            Player1.Dex = 0;
            Player1.Intel = 0;
        }
        else if(Player1First != -1 && Player1Second == -1)
        {
            Player1.Str = thePlayerRoles[Player1First].Strength;
            Player1.Dex = thePlayerRoles[Player1First].Dexterity;
            Player1.Intel = thePlayerRoles[Player1First].Intelligence;
        }
        else if(Player1First != -1 && Player1Second != -1)
        {
            Player1.Str = (float)(thePlayerRoles[Player1First].Strength + thePlayerRoles[Player1Second].Strength) / 2;
            Player1.Dex = (float)(thePlayerRoles[Player1First].Dexterity + thePlayerRoles[Player1Second].Dexterity) / 2;
            Player1.Intel = (float)(thePlayerRoles[Player1First].Intelligence + thePlayerRoles[Player1Second].Intelligence) / 2;
        }

        if (Player2First == -1 && Player2Second == -1)
        {
            Player2.Str = 0;
            Player2.Dex = 0;
            Player2.Intel = 0;
        }
        else if (Player2First != -1 && Player2Second == -1)
        {
            Player2.Str = thePlayerRoles[Player2First].Strength;
            Player2.Dex = thePlayerRoles[Player2First].Dexterity;
            Player2.Intel = thePlayerRoles[Player2First].Intelligence;
        }
        else if (Player2First != -1 && Player2Second != -1)
        {
            Player2.Str = (float)(thePlayerRoles[Player2First].Strength + thePlayerRoles[Player2Second].Strength) / 2;
            Player2.Dex = (float)(thePlayerRoles[Player2First].Dexterity + thePlayerRoles[Player2Second].Dexterity) / 2;
            Player2.Intel = (float)(thePlayerRoles[Player2First].Intelligence + thePlayerRoles[Player2Second].Intelligence) / 2;
        }


    }

    public void UpdateIndexes()
    {
        if(Input.GetKeyDown("a"))
        {
            ChangePlayer1Index(-1);
            UpdateRoles();
        }
        if (Input.GetKeyDown("w"))
        {
            Select(1);
            UpdateRoles();
        }
        if (Input.GetKeyDown("s"))
        {
            DeSelect(1);
            UpdateRoles();
        }
        if (Input.GetKeyDown("d"))
        {
            ChangePlayer1Index(1);
            UpdateRoles();
        }
        if (Input.GetKeyDown("j"))
        {
            ChangePlayer2Index(-1);
            UpdateRoles();
        }
        if (Input.GetKeyDown("i"))
        {
            Select(2);
            UpdateRoles();
        }
        if (Input.GetKeyDown("k"))
        {
            DeSelect(2);
            UpdateRoles();
        }
        if (Input.GetKeyDown("l"))
        {
            ChangePlayer2Index(1);
            UpdateRoles();
        }
    }

    public void ChangePlayer1Index(int value)
    {
        if (Player1GameState == "Choosing")
        {
            Player1Index += value;
            if (Player1Index < 0)
            {
                Player1Index = thePlayerRoles.Count - 1;
            }
            else if (Player1Index >= thePlayerRoles.Count)
            {
                Player1Index = 0;
            }
        }
        else if(Player1GameState == "Chosen")
        {
            if(Player1Confirm)
            {
                Player1Confirm = false;
            }
            else
            {
                Player1Confirm = true;
            }
        }
    }

    public void ChangePlayer2Index(int value)
    {
        if(Player2GameState == "Choosing")
        {
            Player2Index += value;
            if (Player2Index < 0)
            {
                Player2Index = thePlayerRoles.Count - 1;
            }
            else if (Player2Index >= thePlayerRoles.Count)
            {
                Player2Index = 0;
            }
        }
        else if (Player2GameState == "Chosen")
        {
            if (Player2Confirm)
            {
                Player2Confirm = false;
            }
            else
            {
                Player2Confirm = true;
            }
        }
    }

    public void Select(int player)
    {
        if (player == 1)
        {
            if (Player1GameState == "Choosing")
            {
                if (Player1First == -1)
                {
                    Player1First = Player1Index;
                }
                else if (Player1Second == -1)
                {
                    Player1Second = Player1Index;
                    Player1GameState = "Chosen";
                }
            }
            else if(Player1GameState == "Chosen")
            {
                if(Player1Confirm)
                {
                    Player1GameState = "Done";
                    Player1.CurrentRole = thePlayerRoles[Player1Second];
                }
                else
                {
                    Player1First = -1;
                    Player1Second = -1;
                    Player1Confirm = true;
                    Player1GameState = "Choosing";

                    Player1ConfirmText.gameObject.SetActive(false);
                    Player1Yes.gameObject.SetActive(false);
                    Player1No.gameObject.SetActive(false);
                }
            }
        }
        else if (player == 2)
        {
            if(Player2GameState == "Choosing")
            {
                if (Player2First == -1)
                {
                    Player2First = Player2Index;
                }
                else if (Player2Second == -1)
                {
                    Player2Second = Player2Index;
                    Player2GameState = "Chosen";
                }
            }
            else if (Player2GameState == "Chosen")
            {
                if (Player2Confirm)
                {
                    Player2GameState = "Done";
                    Player2.CurrentRole = thePlayerRoles[Player2Second];
                }
                else
                {
                    Player2First = -1;
                    Player2Second = -1;
                    Player2Confirm = true;
                    Player2GameState = "Choosing";

                    Player2ConfirmText.gameObject.SetActive(false);
                    Player2Yes.gameObject.SetActive(false);
                    Player2No.gameObject.SetActive(false);
                }
            }
        }
    }

    public void DeSelect(int player)
    {
        if (player == 1 && Player1GameState != "Done")
        {
            Player1First = -1;
            Player1Second = -1;
            Player1GameState = "Choosing";
            Player1Confirm = true;
            Player1ConfirmText.gameObject.SetActive(false);
            Player1Yes.gameObject.SetActive(false);
            Player1No.gameObject.SetActive(false);
        }
        else if (player == 2 && Player1GameState != "Done")
        {
            Player2First = -1;
            Player2Second = -1;
            Player2GameState = "Choosing";
            Player2Confirm = true;
            Player2ConfirmText.gameObject.SetActive(false);
            Player2Yes.gameObject.SetActive(false);
            Player2No.gameObject.SetActive(false);
        }
    }
}
