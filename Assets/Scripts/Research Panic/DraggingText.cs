using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.WSA;
public class DraggingText : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField ]private RectTransform _TextTransform;
    [SerializeField] private Canvas _MinigameScreen;
    private void Awake()
    {
        _TextTransform = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        _TextTransform.anchoredPosition += eventData.delta / _MinigameScreen.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.GetComponent<PoliticsFolder>() != null)
            {
                transform.SetParent(result.gameObject.transform);
                transform.localPosition = Vector3.zero;
                gameObject.SetActive(false);
                break;
            }
            else if (result.gameObject.GetComponent<CultureFolder>() != null)
            {
                transform.SetParent(result.gameObject.transform);
                transform.localPosition = Vector3.zero;
                gameObject.SetActive(false);
                break;
            }
            else if (result.gameObject.GetComponent<EducationFolder>() != null)
            {
                transform.SetParent(result.gameObject.transform);
                transform.localPosition = Vector3.zero;
                gameObject.SetActive(false);
                break;
            }
        }
    }
    public void OnPointerDown(PointerEventData eventData) 
    {
        Debug.Log("OnPointerDown");
    }
}
