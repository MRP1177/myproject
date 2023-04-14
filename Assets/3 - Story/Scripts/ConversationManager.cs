using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConversationManager : MonoBehaviour
{
    // Information Management
    public string NPCName = "Unknown";

    // State management
    bool ableToTalkTo = false;

    // UI Management
    public TMP_Text interactPrompt;
    public UIManager npcTextUI;
    [HideInInspector]
    public TMP_Text npcText;
    TMP_Text nameText;
    [HideInInspector]
    public UnityEngine.UI.Button continueButton;
    UnityEngine.UI.Image convoBackground;
    UnityEngine.UI.Image nameBackground;

    // Interaction Management
    public string interactionKey = "e";

    // Story management
    public Conversation[] conversations;
    int currentConversationState = 0;

    // Player manager
    ClickPlayerController player;
    FPStoryController altPlayer;

    // Testing

    // Start is called before the first frame update
    void Awake()
    {
        // Snag reference to the NPC text UI component
        npcText = npcTextUI.npcText;
        nameText = npcTextUI.nameText;
        nameText.text = NPCName;
        continueButton = npcTextUI.continueButton;
        convoBackground = npcTextUI.convoBackground;
        nameBackground = npcTextUI.nameBackground;

        if(interactPrompt)
        {
            interactPrompt.text = "Press " + interactionKey + " to talk";
            interactPrompt.gameObject.SetActive(false);
        }


        try
        {
            player = GameObject.Find("ClickMovePlayer").GetComponentInChildren<ClickPlayerController>();
        } 
        catch(System.NullReferenceException e)
        {
            Debug.Log("No click player in scene!");
        }
        try
        {
            altPlayer = GameObject.Find("FirstPersonPlayer").GetComponent<FPStoryController>();
        } 
        catch(System.NullReferenceException e)
        {
            Debug.Log("No first person controller in scene!");
        }
        

        DisableConversationUI();
        
        // Disable conversations
        foreach(Conversation convo in conversations)
        {
            convo.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            ableToTalkTo = true;

            // Turn on world UI talk prompt
            if(interactPrompt)
            {
                interactPrompt.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ableToTalkTo = false;
            
            // Turn off world UI talk prompts
            if(interactPrompt)
            {
                interactPrompt.gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if(ableToTalkTo && Input.GetKeyDown(interactionKey))
        {
            EnableConversationUI();
            // Disable player controls
            if(player)
            {
                player.TurnOffControl();
            }
            else if(altPlayer)
            {
                altPlayer.TurnOffControl();
            }
            // Turn on and start current convo
            conversations[currentConversationState].enabled = true;
            conversations[currentConversationState].StartConversation();
        }
    }

    void EnableConversationUI()
    {
        npcText.gameObject.SetActive(true);
        nameText.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(true);
        convoBackground.gameObject.SetActive(true);
        nameBackground.gameObject.SetActive(true);
    }
    void DisableConversationUI()
    {
        npcText.gameObject.SetActive(false);
        nameText.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        convoBackground.gameObject.SetActive(false);
        nameBackground.gameObject.SetActive(false);
    }

    public void IncrementConversationState()
    {
        DisableConversationUI();
        //if(currentConversationState < conversations.Length-1) // If we aren't already at the final conversation, toggle the next one!
        //{
        //    currentConversationState++;
        //}
    }
}
