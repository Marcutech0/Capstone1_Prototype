using System.Collections.Generic;
using UnityEngine;

public class ResearchTitleSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public RectTransform _CanvasRect; 
    public List<GameObject> _ResearchTitles; 
    [SerializeField] public GameObject _CurrentTitle; 

    private bool isSpawning = false;

    void Update()
    {
        if (!isSpawning && _CurrentTitle == null && _ResearchTitles.Count > 0)
        {
            SpawnNextTitle();
        }
    }

    void SpawnNextTitle()
    {
        if (_ResearchTitles.Count == 0) return;

        int randomIndex = Random.Range(0, _ResearchTitles.Count);
        GameObject _ChosenTitle = _ResearchTitles[randomIndex];
        _ResearchTitles.RemoveAt(randomIndex);

        _CurrentTitle = _ChosenTitle;
        _CurrentTitle.SetActive(true);
        _CurrentTitle.transform.SetParent(_CanvasRect, false);

        RectTransform titleRect = _CurrentTitle.GetComponent<RectTransform>();
        float halfWidth = _CanvasRect.rect.width / 2f;
        float halfHeight = _CanvasRect.rect.height / 2f;
        float margin = 150f;
        float randomX = Random.Range(-halfWidth + margin, halfWidth - margin);
        float randomY = Random.Range(-halfHeight + margin, halfHeight - margin);
        titleRect.anchoredPosition = new Vector2(randomX, randomY);

        var drag = _CurrentTitle.GetComponent<DraggingText>();
        if (drag != null)
            drag._Spawner = this;
    }

    public void OnTitlePlaced(GameObject title)
    {
        if (_CurrentTitle == title)
        {
            _CurrentTitle = null; 
        }
    }
}
