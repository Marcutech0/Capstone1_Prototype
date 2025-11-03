using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.Video;
public class PhoneInteraction : MonoBehaviour
{
    [Header("UI")]
    public GameObject _DialoguePanel;
    public GameObject _InteractIndicator;
    public GameObject _ChoicePanel;
    public TextMeshProUGUI _NpcName; // since this is a object npc name is empty
    public TextMeshProUGUI _StoryText; // no story to write
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
            _NpcName.text = string.Empty;
            _StoryText.text = string.Empty;
            _PlayerController.enabled = false;
            _PlayerControls.enabled = false;
            _DialoguePanel.SetActive(true);
            _ChoicePanel.SetActive(true);
        }
    }

    public void Choice1Dorm() 
    {
        _LegendTracker._ReputationCount++;
        _LegendTracker._ReputationText.text = "Reputation: " + _LegendTracker._ReputationCount;
        _LegendTracker._Reputation.SetActive(true);

        StartCoroutine(CloseDialogueAndChoicePanel(2f));
    }

    public void Choice2Dorm() 
    {
        _LegendTracker._GuiltCount++;
        _LegendTracker._GuiltText.text = "Guilt: " + _LegendTracker._GuiltCount;
        _LegendTracker._Guilt.SetActive(true);

        StartCoroutine(CloseDialogueAndChoicePanel(2f));
    }

    public void Choice3Dorm() 
    {
        _LegendTracker._AnonymityCount++;
        _LegendTracker._AnonymityText.text = "Anonymity: " + _LegendTracker._AnonymityCount;
        _LegendTracker._Anonymity.SetActive(true);

        StartCoroutine(CloseDialogueAndChoicePanel(2f));

    }

    IEnumerator CloseDialogueAndChoicePanel(float delay) 
    {
        yield return new WaitForSeconds(delay);

        _DialoguePanel.SetActive(false);
        _ChoicePanel.SetActive(false);
        _LegendTracker._Reputation.SetActive(false);
        _LegendTracker._Guilt.SetActive(false);
        _LegendTracker._Anonymity.SetActive(false);

        _PlayerController.enabled = true;
        _PlayerControls.enabled = true;

        //_LegendTracker._Cutscene2.Play();

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Phone") && !_HasInteracted)
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
        if (other.CompareTag("Phone"))
        {
            _IsInRange = false;
            _InteractIndicator.SetActive(false);
        }
    }
}
