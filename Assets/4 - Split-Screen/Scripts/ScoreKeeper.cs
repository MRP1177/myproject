using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    public int myScore = 0;
    public string playerName = "Player";

    // Score management
    public TMP_Text myScoreText;

    // Start is called before the first frame update
    void Start()
    {
        UpdateScoreUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateScoreUI()
    {
        myScoreText.text = playerName + " Score: " + myScore;
    }

    public void AddOnePoint()
    {
        myScore++; // Add one to my current points
        UpdateScoreUI();
    }

    public void AddPoints(int points)
    {
        myScore += points; // Add '# of points' to my current points
        UpdateScoreUI();
    }

    public void ResetPoints()
    {
        myScore = 0;
        UpdateScoreUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Collectable")
        {
            Destroy(other.gameObject);
            AddOnePoint();
        }
    }
}
