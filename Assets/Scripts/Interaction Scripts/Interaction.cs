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
        _OriginalSpeed = _Player.moveSpeed;

        ConversationManager.OnConversationStarted += OnConversationStarted;
        ConversationManager.OnConversationEnded   += OnConversationEnded;
    }
    private void OnDestroy()
    {
        ConversationManager.OnConversationStarted -= OnConversationStarted;
        ConversationManager.OnConversationEnded -= OnConversationEnded;
    }

    private void Update()
    {
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
        _Player.moveSpeed = 0f;
    }

    private void OnConversationEnded()
    {
        _Player.moveSpeed = _OriginalSpeed;
    }
}
