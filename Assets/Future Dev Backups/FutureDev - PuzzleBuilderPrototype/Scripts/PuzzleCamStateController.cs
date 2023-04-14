using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCamStateController : MonoBehaviour
{
    public string stateChangeKey = "q";
    public bool spawnState = true;

    ClickSpawner clickSpawner;
    DragRigidbody dragRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        clickSpawner = GetComponent<ClickSpawner>();
        dragRigidBody = GetComponent<DragRigidbody>();

        clickSpawner.enabled = true;
        dragRigidBody.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(stateChangeKey))
        {
            spawnState = !spawnState;
            Debug.Log("State change!");
            StateChanger();
        }
    }

    void StateChanger()
    {
        if(clickSpawner.isActiveAndEnabled)
        {
            clickSpawner.enabled = false;
            dragRigidBody.enabled = true;
        }
        else
        {
            clickSpawner.enabled = true;
            dragRigidBody.enabled = false;
        }
    }
}
