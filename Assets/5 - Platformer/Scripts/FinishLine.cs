using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour
{
    public GameObject player;
    public GameObject firework;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Instantiate(firework, new Vector3(player.transform.position.x, player.transform.position.y + 3, player.transform.position.z), Quaternion.identity);

            PlayerController pc = player.GetComponent<PlayerController>();
            pc.PlayDanceAnimation();

            Invoke("GoToNextLevel", 3);
        }
    }

    private void GoToNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCount >= nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
