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
        // Cup Spawner based on Spawner Interval
        while (true)
        {
            yield return new WaitForSeconds(2f);

            bool allFilled = true;
            foreach (GameObject cup in _Cups)
            {
                if (cup == null)
                {
                    allFilled = false;
                    break;
                }
            }
            if (allFilled)
                yield break;

            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, _CupSpawnPoints.Length);
            } while (_Cups[randomIndex] != null);

            SpawnCupAt(randomIndex);
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
    [HideInInspector] public TextMeshProUGUI fillPercentText;   

    public static CupData currentlySelectedCup = null;

    private CupData cupData;

    void Awake()
    {
        // refereced the cup data script on start up on the game
        cupData = GetComponent<CupData>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // currently selected cup by the player is displayed via text, the cups number will be based on it's index in the array, with it;s fill percent text updated based on it's currently stored fill amount data
        currentlySelectedCup = cupData;

        if (_SelectedCupText != null)
            _SelectedCupText.text = $"Selected Cup: {_Index}";

        if (fillPercentText != null)
            fillPercentText.text = $"{cupData._FillPercent:0}%";
    }
}



