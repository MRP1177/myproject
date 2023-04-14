using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToNextLevel : MonoBehaviour
{
    public string nextLevelName;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextLevelName);
        }
    }
}
