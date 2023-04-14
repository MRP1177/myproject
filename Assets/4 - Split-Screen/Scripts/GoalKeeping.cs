using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKeeping : MonoBehaviour
{
    public GameObject playerWhoScoresOnMe;
    public List<GameObject> kickoffSpot;
    //public List<GameObject> ball;

    float kickoffDelay = 3f;
    GameObject tempBall;
    GameObject tempBall2;
    GameObject tempBall3;
    GameObject currentBall;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ball")
        {
            // Update the score for the right player
            ScoreKeeper scoreKeeper = playerWhoScoresOnMe.GetComponent<ScoreKeeper>();
            scoreKeeper.AddOnePoint();

            // Respawn current ball after delay
            if(!tempBall)
            {
                tempBall = other.gameObject;
            }
            else if(!tempBall2)
            {
                tempBall2 = other.gameObject;
            }
            else if(!tempBall3)
            {
                tempBall3 = other.gameObject;
            }

            Invoke("SpawnBallAtKickoff", kickoffDelay);
        }
    }

    void SpawnBallAtKickoff()
    {
        // Spawn current ball at a random kickoff spot
        int index = (int)Random.Range(0,kickoffSpot.Count-1);

        // Assign current ball
        if (tempBall)
        {
            currentBall = tempBall;
        }
        else if (tempBall2)
        {
            currentBall = tempBall2;
        }
        else if (tempBall3)
        {
            currentBall = tempBall3;
        }

        currentBall.transform.position = kickoffSpot[index].transform.position;
        ResetBalls();
    }

    void ResetBalls()
    {
        if(tempBall)
        {
            tempBall = null;
        }
        else if(tempBall2)
        {
            tempBall2 = null;
        }
        else if(tempBall3)
        {
            tempBall3 = null;
        }
        currentBall = null;
    }
}
