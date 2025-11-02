using UnityEngine;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
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
    public void Start()
    {
        _CutscenePanel.SetActive(true);
        Time.timeScale = 0f;
        _Cutscene1 = _Cutscene1.GetComponent<VideoPlayer>();
        _Cutscene1.loopPointReached += OnVideoEnd;
    }

    public void OnVideoEnd(VideoPlayer cs1) 
    {
        _CutscenePanel.SetActive(false);
        Time.timeScale = 1f;

    }
}
