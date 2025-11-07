using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EducationFolder : MonoBehaviour
{
    public List<string> _EducationList = new List<string>();

    private void Awake()
    {
        if (gameObject.tag != "EducationFolder")
            gameObject.tag = "EducationFolder";
    }

    public void CheckDrop(GameObject draggedObject)
    {
        RectTransform folderRect = GetComponent<RectTransform>();
        RectTransform draggedRect = draggedObject.GetComponent<RectTransform>();

        if (RectTransformUtility.RectangleContainsScreenPoint(
            folderRect,
            Input.mousePosition,
            null))
        {

            TextMeshProUGUI text = draggedObject.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null)
            {
                Debug.Log("Detected text: " + text.text + " on folder: " + gameObject.name);

                _EducationList.Add(text.text);
                draggedObject.SetActive(false);
            }
        }
    }
}
