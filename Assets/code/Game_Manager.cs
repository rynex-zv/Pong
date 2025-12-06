using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public  Player player1;
    public  Player player2;
  

    // Start is called before the first frame update
    void Start()
    {
    }
    internal void OnPlayer1Scores() => UpdateScore() ;
    internal void OnPlayer2Scores() => UpdateScore(false);
    // Update is called once per frame
    void Update()
    {

    }

    void UpdateScore(bool isPlayer_1=true)
    {
        if ( isPlayer_1 )
        {
            player1.Score();
            player1.PScore.text = player1.MyPoints.ToString();

        } else
        {
            player2.Score();
            player2.PScore.text = player2.MyPoints.ToString();
        }
    }

}
