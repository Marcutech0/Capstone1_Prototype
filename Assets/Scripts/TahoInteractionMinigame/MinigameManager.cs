using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
public class MinigameManager : MonoBehaviour
{
    public static MinigameManager _Instance;
    [Header("UI")]
    public TextMeshProUGUI _TimerText;
    public TextMeshProUGUI _OverfillText;
    public TextMeshProUGUI _ReleasedText;
    public TextMeshProUGUI _GameStatusText;
    public CanvasGroup _TahoPanel;

    [Header("MinigameSettings")]
    public float _GameDuration = 20f;
    private float _RemainingTime;
    private bool _GameActive = false;
    public ReleaseManager _OverfillManager;
    public ReleaseManager _ReleaseManager;

    [Header("SpillSettings")]
    public float _MinSpillDelay;
    public float _MaxSpillDelay;
    public float _MinSpillAmount;
    public float _MaxSpillAmount;


    public void Awake()
    {
        _Instance = this;
    }

    public void Start()
    {
        StartCoroutine(StartMinigame());
    }

    IEnumerator StartMinigame()
    {
        _RemainingTime = _GameDuration;
        _GameActive = true;

        StartCoroutine(SpillRoutine());

        while (_GameActive) 
        {
           _RemainingTime -= Time.deltaTime;
           _TimerText.text = $"{Mathf.Max(_RemainingTime, 0f):0}s";

            if (_OverfillManager._OverfillCount >= 3 || _RemainingTime < 0f) 
            {
                _GameActive = false;
                EndMiniGame();
                yield break;
            }

            yield return null;  
        }

    }

    IEnumerator SpillRoutine()
    {
        while (_GameActive)
        {
            float _WaitTime = Random.Range(_MinSpillDelay, _MaxSpillDelay);
            yield return new WaitForSeconds(_WaitTime);

            var _CurrentCup = CupClickManager._CurrentlySelectedCup;
            if (_CurrentCup != null)
            {
                float _SpillAmount = Random.Range(_MinSpillAmount, _MaxSpillAmount);
                _CurrentCup._FillPercent -= _SpillAmount;
                _CurrentCup._FillPercent = Mathf.Clamp(_CurrentCup._FillPercent, 0, 100);
            }
        }
    }

    public void AddOverFill() 
    {
        if (!_GameActive) return;
        _OverfillManager._OverfillCount++;
        _OverfillText.text = $"Overfilled Taho: {_OverfillManager._OverfillCount}";
    }

    public void AddRelease() 
    {
        if (!_GameActive) return;
        _ReleaseManager._ReleasedCount++;
        _ReleasedText.text = $"Released Taho: {_ReleaseManager._ReleasedCount}";
    }

    public void EndMiniGame() 
    {
        _GameActive = false;
        if (_OverfillManager._OverfillCount >= 3)
        {
            _GameStatusText.text = "Game over! spilled to many cups!";
        }

        else 
        {
            _GameStatusText.text = "Time's up, good job!";
        }

        SceneManager.LoadScene("SampleScene");
    }
    public bool _IsGameActive() 
    {
        return _GameActive;
    }
}
