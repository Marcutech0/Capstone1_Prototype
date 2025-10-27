using UnityEngine;
using TMPro;
public class Interaction : MonoBehaviour
{
    public PlayerMovement _Player;
    public GameObject _InteractionIndicator;
    public TextMeshProUGUI _InteractionText;
    public GameObject _DialoguePanel;
    public TextMeshProUGUI _DialogueText;
    public bool _isInteracting;
    public bool _isInteractingObject;
    public bool _isDialogueActive;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))                // triggers with npc interact indicator is out
        {
            _InteractionIndicator.SetActive(true);
            _InteractionText.text = "Talk";
            Debug.Log("Collided with NPC");
            _isInteracting = true;
        }

        if (other.CompareTag("Object"))             // triggers with object interact indicator is out
        {
            _InteractionIndicator.SetActive(true);
            _InteractionText.text = "Inspect";
            Debug.Log("Collided with object");
            _isInteractingObject = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC") || other.CompareTag("Object"))  // on exit trigger interact indicator is off
        {
            _InteractionIndicator.SetActive(false);
            _isInteracting = false;
            _isInteractingObject = false;
            Debug.Log("NotColliding");
        }
    }

    private void Update()
    {
        if (_isInteracting && Input.GetKeyDown(KeyCode.F))  // if dialogue is on, press f to interact, dialogue panel pops out, if done talking, dialogue panel is off (subject to change when dialogur system is out)
            
        {
            _isDialogueActive = !_isDialogueActive;

            if (_isDialogueActive) 
            {
                _DialoguePanel.SetActive(true);
                _DialogueText.text = "You are talking to me!";
                _Player.moveSpeed = 0;
            }
            else 
            {
                _DialoguePanel.SetActive(false);
                _Player.moveSpeed = 1;
                Debug.Log("Stopped talking");
            }
        }

        if (_isInteractingObject && Input.GetKeyDown(KeyCode.F)) // if dialogue is on, press f to interact, dialogue panel pops out, if done inspecting, dialogue panel is off (subject to change when dialogur system is out)
        {
            _isDialogueActive = !_isDialogueActive;

            if (_isDialogueActive) 
            {
                _DialoguePanel.SetActive(true);
                _DialogueText.text = ("Inspecting an Object");
                _Player.moveSpeed = 0;
                Debug.Log("Inspecting Object");
            }
            else 
            {
                _DialoguePanel.SetActive(false);
                _Player.moveSpeed = 1;
                Debug.Log("Stopped Inspecting");
            }
        }
    }
}
