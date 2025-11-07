using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PoliticsFolder : MonoBehaviour
{
    public List<string> _PoliticsList = new List<string>();

    private void Awake()
    {
        if (gameObject.tag != "PoliticsFolder")
            gameObject.tag = "PoliticsFolder";
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

                _PoliticsList.Add(text.text);
                draggedObject.SetActive(false);
            }
        }
    }
}
