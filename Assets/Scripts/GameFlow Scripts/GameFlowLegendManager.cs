using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class GameFlowLegendManager : MonoBehaviour
{
    [Header("Legend Tracker")]
    public int _CourageCount;
    public int _FearCount;
    public int _ReputationCount;
    public int _AnonymityCount;
    public int _GuiltCount;
    [Header("Cutscenes")]
    public GameObject _CutscenePanel;
    public VideoPlayer _Cutscene1;
    public VideoPlayer _Cutscene2;

    [Header("UI")]
    public GameObject _Courage;
    public GameObject _Fear;
    public GameObject _Reputation;
    public GameObject _Anonymity;
    public GameObject _Guilt;
    public TextMeshProUGUI _CourageText;
    public TextMeshProUGUI _FearText;
    public TextMeshProUGUI _ReputationText;
    public TextMeshProUGUI _AnonymityText;
    public TextMeshProUGUI _GuiltText;
    public void Start()
    {
        /*
        _CutscenePanel.SetActive(true);
        Time.timeScale = 0f;
        _Cutscene1 = _Cutscene1.GetComponent<VideoPlayer>();
        _Cutscene1.loopPointReached += OnVideoEnd;
        */
    }

    public void Awake()
    {
        // testing purposes
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.Save();
        //Debug.Log("Player Int Legend count resetted!");

        _CourageCount = PlayerPrefs.GetInt("Courage Count", 0);
        _FearCount = PlayerPrefs.GetInt("Fear Count", 0);
        _ReputationCount = PlayerPrefs.GetInt("Reputation Count", 0);
        _AnonymityCount = PlayerPrefs.GetInt("Anonymity Count", 0);
        _GuiltCount = PlayerPrefs.GetInt("Guilt Count", 0);

        if (_CourageText != null)
            _CourageText.text = "Courage: " + _CourageCount;

        if (_FearText != null)
            _FearText.text = "Fear:" + _FearCount;

        if (_ReputationText != null)
            _ReputationText.text = "Reputation: " + _ReputationCount;

        if (_AnonymityText != null)
            _AnonymityText.text = "Anonymity" + _AnonymityCount;

        if (_GuiltText != null)
            _GuiltText.text = "Guilt: " + _GuiltCount;

    }

    public void OnVideoEnd(VideoPlayer cs1) 
    {
        /*
        _CutscenePanel.SetActive(false);
        Time.timeScale = 1f;
        */
    }
}
