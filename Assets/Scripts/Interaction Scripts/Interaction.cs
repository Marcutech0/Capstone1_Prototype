using UnityEngine;
using TMPro;
using DialogueEditor;
public class Interaction : MonoBehaviour
{
    [SerializeField] private NPCConversation _AntonioConversation; 
    public PlayerMovement _Player;
    public GameObject _InteractionIndicator;
    public TextMeshProUGUI _InteractionText;
    public bool _isInteracting;
    public bool _canInteract;
    private float _OriginalSpeed;

    private void Start()
    {
        // set the orgi speed to the default speed, references the dialogue systems conversation manager
        _OriginalSpeed = _Player.moveSpeed;

        ConversationManager.OnConversationStarted += OnConversationStarted;
        ConversationManager.OnConversationEnded   += OnConversationEnded;
    }
    private void OnDestroy()
    {
        // makes sure that np duplicates of dialogue's editors managers exists
        ConversationManager.OnConversationStarted -= OnConversationStarted;
        ConversationManager.OnConversationEnded -= OnConversationEnded;
    }

    private void Update()
    {
        // if the player can interact press f to interact with the NPC or object
        if (_canInteract && Input.GetKeyDown(KeyCode.F))
        {
            ConversationManager.Instance.StartConversation(_AntonioConversation);
            _Player.moveSpeed = 0f;
            _isInteracting = true;
            _InteractionIndicator.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If a NPC or object detects a player, set the player's interact indicator to appear
        if (other.CompareTag("Player"))
        {
            _InteractionIndicator.SetActive(true);
            _InteractionText.text = "Interact";
            _canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))  // on exit trigger interact indicator is off
        {
            _InteractionIndicator.SetActive(false);
            _isInteracting = false;
            Debug.Log("NotColliding");
        }
    }

    private void OnConversationStarted()
    {
        // if the coversation triggers set player speed to 0
        _Player.moveSpeed = 0f;
    }

    private void OnConversationEnded()
    {
        // if the conversation ended brings the player speed back to its default speed
        _Player.moveSpeed = _OriginalSpeed;
    }
}
