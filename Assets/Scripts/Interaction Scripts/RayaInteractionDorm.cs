using TMPro;
using UnityEngine;
using System.Collections;
public class RayaInteractionDorem : MonoBehaviour
{
    [Header("UI")]
    public GameObject _DialoguePanel;
    public GameObject _InteractIndicator;
    public GameObject _Choice2Panel;
    public TextMeshProUGUI _NpcName; 
    public TextMeshProUGUI _StoryText;
    public TextMeshProUGUI _InteractText;

    [TextArea] public string _Storyline;
    public GameFlowLegendManager _LegendTracker;
    public CharacterController _PlayerController;
    public PlayerMovement _PlayerControls;
    public bool _IsInRange;
    public bool _HasInteracted;

    public void Update()
    {
        if (_IsInRange && !_HasInteracted && Input.GetKeyDown(KeyCode.F))
        {
            _HasInteracted = true;
            StartCoroutine(ShowDialogueRaya());
            _NpcName.text = "Raya";
        }
    }

    IEnumerator ShowDialogueRaya()
    {
        _DialoguePanel.SetActive(true);
        _InteractIndicator.SetActive(false);

        _PlayerController.enabled = false;
        _PlayerControls.enabled = false;

        _StoryText.text = "";

        foreach (char c in _Storyline)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.03f);
        }

        if (_StoryText.text == _Storyline) 
        {
            _Choice2Panel.SetActive(true);
        }
    }

    IEnumerator ShowNewDialogueText(string _NewLine) 
    {
        _StoryText.text = "";

        foreach (char c in _NewLine)
        {
            _StoryText.text += c;
            yield return new WaitForSeconds(0.03f);
        }
    }

    public void Choice4Dorm() 
    {
        _LegendTracker._ReputationCount++;
        _LegendTracker._ReputationText.text = "Reputation: " + _LegendTracker._ReputationCount;
        _LegendTracker._Reputation.SetActive(true);
        _Choice2Panel.SetActive(false);
        StartCoroutine(ShowNewDialogueText("Can we talk later? Just us."));
    }

    public void Choice5Dorm() 
    {
    
    }

    public void Choice6Dorm() 
    {
        
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RayaDorm") && !_HasInteracted)
        {
            _IsInRange = true;
            _InteractIndicator.SetActive(true);

            if (_HasInteracted)
                _InteractText.text = "Interacted!";
            else
                _InteractText.text = "Press F to Interact";
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RayaDorm"))
        {
            _IsInRange = false;
            _InteractIndicator.SetActive(false);
        }
    }
}
