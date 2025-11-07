using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggingText : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private RectTransform _TextTransform;
    public Canvas _MinigameScreen;
    public ResearchTitleSpawner _Spawner; 

    private void Awake()
    {
        _TextTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer down on " + gameObject.name);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin dragging " + gameObject.name);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _TextTransform.anchoredPosition += eventData.delta / _MinigameScreen.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        List<RaycastResult> _Results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, _Results);
        foreach (RaycastResult _Result in _Results)
        {
            var _Politics = _Result.gameObject.GetComponent<PoliticsFolder>();
            var _Culture = _Result.gameObject.GetComponent<CultureFolder>();
            var _Education = _Result.gameObject.GetComponent<EducationFolder>();
            if (_Politics != null)
            {
                CheckTextTagAndScore("Politics");
                PlaceInFolder(_Politics.transform);
                return;
            }

            if (_Culture != null)
            {
                CheckTextTagAndScore("Culture");
                PlaceInFolder(_Culture.transform);
                return;
            }

            if (_Education != null)
            {
                CheckTextTagAndScore("Education");
                PlaceInFolder(_Education.transform);
                return;
            }
        }
    }

    private void PlaceInFolder(Transform _Folder)
    {
        Debug.Log($"Dropped {gameObject.name} on {_Folder.name}");
        transform.SetParent(_Folder);
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
        _Spawner?.OnTitlePlaced(gameObject);
    }

    private void CheckTextTagAndScore(string _FolderType)
    {
        Timer _Timer = FindAnyObjectByType<Timer>();
        if (_Timer == null) return;

        // Compare the tag of the text with folder type
        if (gameObject.CompareTag(_FolderType))
        {
            _Timer.AddCorrectPoints();
        }
        else
        {
            _Timer.AddWrongPoints();
        }
    }
}