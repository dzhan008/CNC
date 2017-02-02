﻿using UnityEngine;
using System.Collections;

public class player_script : MonoBehaviour
{

    private int player_id;

    void Start()
    {
        //set player id during the beginnning
        player_id = this.gameObject.GetComponent<Stats>().Id;
    }

    //get the player id
    public int getPlayerID()
    {
        return player_id;
    }

    //update books carried from book stacking script when a book is caught
    private int _BooksCarried = 0;
    public int BooksCarried
    {
        get
        {
            return _BooksCarried;
        }
        set
        {
            _BooksCarried = value;
        }
    }

    private bool _isOnTomb = false;
    public bool isOnTomb
    {
        get
        {
            return _isOnTomb;
        }
        set
        {
            _isOnTomb = value;
        }
    }

}
