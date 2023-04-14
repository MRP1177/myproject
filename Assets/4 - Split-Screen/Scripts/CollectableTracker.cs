using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectableTracker : MonoBehaviour
{
    public int numberOfCollectablesLeft;
    public TMP_Text winnerText;

    public ScoreKeeper player1;
    public ScoreKeeper player2;

    // Update is called once per frame
    void Update()
    {
        // Keep track of # of collectables left
        numberOfCollectablesLeft = transform.childCount;

        // Process end state (when all collectables are collected)!
        if(numberOfCollectablesLeft <= 1)
        {
            ProcessEndOfGame();
        }
    }

    void ProcessEndOfGame()
    {
        winnerText.gameObject.SetActive(true);

        if(player1.myScore > player2.myScore)
        {
            winnerText.text = "Player 1 wins with a score of " + player1.myScore + "!";
        }
        else if(player2.myScore > player1.myScore)
        {
            winnerText.text = "Player 2 wins with a score of " + player2.myScore + "!";
        }
        else
        {
            winnerText.text = "It's a draw!";
        }
    }

}
