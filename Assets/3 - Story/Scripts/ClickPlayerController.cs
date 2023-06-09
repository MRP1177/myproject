﻿// Credit for script references from https://sharpcoderblog.com/blog/rts-moba-player-controller-script

using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Linq;
using TMPro;

[RequireComponent(typeof(NavMeshAgent))]

public class ClickPlayerController : MonoBehaviour
{
    public Camera playerCamera;
    public Vector3 cameraOffset;
    public GameObject targetIndicatorPrefab;
    NavMeshAgent agent;
    GameObject targetObject;

    bool controlActive = true;



    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //Instantiate click target prefab
        if (targetIndicatorPrefab)
        {
            targetObject = Instantiate(targetIndicatorPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            targetObject.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(controlActive)
        {
            ProcesssMovement();
        }
    }

    void ProcesssMovement()
    {
        #if (UNITY_ANDROID || UNITY_IOS || UNITY_WP8 || UNITY_WP8_1) && !UNITY_EDITOR
                            //Handle mobile touch input
                            for (var i = 0; i < Input.touchCount; ++i)
                            {
                                Touch touch = Input.GetTouch(i);

                                if (touch.phase == TouchPhase.Began)
                                {
                                    MoveToTarget(touch.position);
                                }
                            }
        #else
                //Handle mouse input
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    MoveToTarget(Input.mousePosition);
                }
        #endif

                //Camera follow
                playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, transform.position + cameraOffset, Time.deltaTime * 7.4f);
                playerCamera.transform.LookAt(transform);
    }


    void MoveToTarget(Vector2 posOnScreen)
    {
        //print("Move To: " + new Vector2(posOnScreen.x, Screen.height - posOnScreen.y));

        Ray screenRay = playerCamera.ScreenPointToRay(posOnScreen);

        RaycastHit hit;
        if (Physics.Raycast(screenRay, out hit, 75))
        {
            agent.destination = hit.point;

            //Show marker where we clicked
            if (targetObject)
            {
                targetObject.transform.position = agent.destination;
                targetObject.SetActive(true);
            }
        }
    }



    public void TurnOffControl()
    {
        controlActive = false;
        Debug.Log("CLICK CONTROLS OFF");
    }

    public void TurnOnControl()
    {
        controlActive = true;
        Debug.Log("CLICK CONTROLS ON");
    }

}