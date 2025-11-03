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

    public GameFlowLegendManager _LegendManager;
    [TextArea] public string _Storyline;
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
        _LegendManager._ReputationCount++;
        _LegendManager._ReputationText.text = "Reputation: " + _LegendManager._ReputationCount;
        _LegendManager._Reputation.SetActive(true);
        PlayerPrefs.SetInt("Reputation Count", _LegendManager._ReputationCount);
        PlayerPrefs.Save();

        StartCoroutine(CloseDialogueAndChoicePanel(1f));
    }

    public void Choice2Dorm() 
    {
        _LegendManager._GuiltCount++;
        _LegendManager._GuiltText.text = "Guilt: " + _LegendManager._GuiltCount;
        _LegendManager._Guilt.SetActive(true);
        PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
        PlayerPrefs.Save();

        StartCoroutine(CloseDialogueAndChoicePanel(1f));
    }

    public void Choice3Dorm() 
    {
        _LegendManager._AnonymityCount++;
        _LegendManager._AnonymityText.text = "Anonymity: " + _LegendManager._AnonymityCount;
        _LegendManager._Anonymity.SetActive(true);
        PlayerPrefs.SetInt("Anonymity Count", _LegendManager._AnonymityCount);
        PlayerPrefs.Save();

        StartCoroutine(CloseDialogueAndChoicePanel(1f));

    }

    IEnumerator CloseDialogueAndChoicePanel(float delay) 
    {
        yield return new WaitForSeconds(delay);

        _DialoguePanel.SetActive(false);
        _ChoicePanel.SetActive(false);
        _LegendManager._Reputation.SetActive(false);
        _LegendManager._Guilt.SetActive(false);
        _LegendManager._Anonymity.SetActive(false);

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
