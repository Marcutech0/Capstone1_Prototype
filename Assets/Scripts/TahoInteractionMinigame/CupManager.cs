using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;

public class CupManager : MonoBehaviour
{
    public GameObject _CupPrefab;
    public RectTransform[]_CupSpawnPoints;
    public RectTransform _UiCanvas;
    public TextMeshProUGUI _SelectedCupText; 
    public GameObject[]_Cups;

    void Start()
    {
        // the no of cups are equal to the number of transform spawn points
        _Cups = new GameObject[_CupSpawnPoints.Length];
        StartCoroutine(SpawnCupsOverTime());
    }

    IEnumerator SpawnCupsOverTime()
    {
        // Spawns Cups every 2 secs on the empty spawnpoints
        while (true) 
        {
            yield return new WaitForSeconds(1f);

            for (int i = 0; i < _CupSpawnPoints.Length; i++) 
            {
                if (_Cups[i] == null) 
                {
                    SpawnCupAt(i);
                }
            }
        }
    }

    void SpawnCupAt(int spawnIndex)
    {
        // Spawning cups at specific transform spawn points set, and spawning them inside the canvas minigame panel
        GameObject newCup = Instantiate(_CupPrefab, _UiCanvas, false);
        RectTransform cupRect = newCup.GetComponent<RectTransform>();
        RectTransform spawnRect = _CupSpawnPoints[spawnIndex];

        cupRect.anchorMin = spawnRect.anchorMin;
        cupRect.anchorMax = spawnRect.anchorMax;
        cupRect.pivot = spawnRect.pivot;
        cupRect.anchoredPosition = spawnRect.anchoredPosition;

        CupClickManager clickHandler = newCup.AddComponent<CupClickManager>();
        clickHandler._Index = spawnIndex; 
        clickHandler._SelectedCupText = _SelectedCupText;

        _Cups[spawnIndex] = newCup;
    }
}
public class CupClickManager : MonoBehaviour, IPointerClickHandler
{
    [HideInInspector] public int _Index; 
    [HideInInspector] public TextMeshProUGUI _SelectedCupText; 
    [HideInInspector] public TextMeshProUGUI _FillPercentText;   

    public static CupData _CurrentlySelectedCup = null;

    private CupData _CupData;

    void Awake()
    {
        // refereced the cup data script on start up on the game
        _CupData = GetComponent<CupData>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // currently selected cup by the player is displayed via text, the cups number will be based on it's index in the array, with it;s fill percent text updated based on it's currently stored fill amount data
        _CurrentlySelectedCup = _CupData;

        if (_SelectedCupText != null)
            _SelectedCupText.text = $"Selected Cup: {_Index}";

        if (_FillPercentText != null)
            _FillPercentText.text = $"{_CupData._FillPercent:0}%";
    }
}



