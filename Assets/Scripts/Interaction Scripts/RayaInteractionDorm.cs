using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class RayaInteractionDorem : MonoBehaviour
{
    [Header("UI")]
    public GameObject _DialoguePanel;
    public GameObject _InteractIndicator;
    public GameObject _Choice2Panel;
    public GameObject _Choice3Panel;
    public TextMeshProUGUI _NpcName; 
    public TextMeshProUGUI _StoryText;
    public TextMeshProUGUI _InteractText;

    [TextArea] public string _Storyline;
    public CharacterController _PlayerController;
    public PlayerMovement _PlayerControls;
    public bool _IsInRange;
    public bool _HasInteracted;

    public GameFlowLegendManager _LegendManager;

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

        if (_StoryText.text == _NewLine) 
        {
            _Choice3Panel.SetActive(true);
        }
    }

    IEnumerator EndDialogueLoadScene() 
    {
        yield return new WaitForSeconds(1f);
        _DialoguePanel.SetActive(false);
        _Choice3Panel.SetActive(false);
        _LegendManager._Reputation.SetActive(false);
        _LegendManager._Guilt.SetActive(false);
        _LegendManager._Courage.SetActive(false);
        _LegendManager._Fear.SetActive(false);
        _LegendManager._Courage.SetActive(false);
        _LegendManager._Guilt.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("CampusHall");
    }

    public void Choice4Dorm() 
    {
        _LegendManager._ReputationCount++;
        _LegendManager._ReputationText.text = "Reputation: " + _LegendManager._ReputationCount;
        _LegendManager._Reputation.SetActive(true);
        PlayerPrefs.SetInt("Reputation Count", _LegendManager._ReputationCount);
        PlayerPrefs.Save();
        _Choice2Panel.SetActive(false);
        StartCoroutine(ShowNewDialogueText("Can we talk later? Just us."));
    }

    public void Choice5Dorm() 
    {
        _LegendManager._GuiltCount++;
        _LegendManager._GuiltText.text = "Guilt: " + _LegendManager._GuiltCount;
        _LegendManager._Guilt.SetActive(true);
        PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
        PlayerPrefs.Save();
        _Choice2Panel.SetActive(false);
        StartCoroutine(ShowNewDialogueText("Can we talk later? Just us."));
    }

    public void Choice6Dorm() 
    {
        _LegendManager._CourageCount++;
        _LegendManager._CourageText.text = "Courage: " + _LegendManager._CourageCount;
        _LegendManager._Courage.SetActive(true);
        PlayerPrefs.SetInt("Courage Count", _LegendManager._CourageCount);
        PlayerPrefs.Save();
        _Choice2Panel.SetActive(false);
        StartCoroutine(ShowNewDialogueText("Can we talk later? Just us."));
    }

    public void Choice7Dorm()
    {
        _LegendManager._FearCount++;
        _LegendManager._FearText.text = "Fear: " + _LegendManager._FearCount;
        _LegendManager._Fear.SetActive(true);
        PlayerPrefs.SetInt("Fear Count", _LegendManager._FearCount);
        PlayerPrefs.Save();
        StartCoroutine(EndDialogueLoadScene());
    }
    public void Choice8Dorm()
    {
        _LegendManager._CourageCount++;
        _LegendManager._CourageText.text = "Courage: " + _LegendManager._CourageCount;
        _LegendManager._Courage.SetActive(true);
        PlayerPrefs.SetInt("Courage Count", _LegendManager._CourageCount);
        PlayerPrefs.Save();
        StartCoroutine(EndDialogueLoadScene());
    }
    public void Choice9Dorm()
    {
        _LegendManager._GuiltCount++;
        _LegendManager._GuiltText.text = "Guilt: " + _LegendManager._GuiltCount;
        _LegendManager._Guilt.SetActive(true);
        PlayerPrefs.SetInt("Guilt Count", _LegendManager._GuiltCount);
        PlayerPrefs.Save();
        StartCoroutine(EndDialogueLoadScene());
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
