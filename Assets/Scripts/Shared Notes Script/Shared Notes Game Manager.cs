using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class Shared_Notes_Manager : MonoBehaviour
{
    [SerializeField] private GameObject text_fragmentPrefab;
    [SerializeField] private RectTransform text_fragmentParent;
    [SerializeField] private Transform[] notebookSlots;

    [SerializeField] private string[] text_inFragments;
    [SerializeField] private string[] right_answers;

    private List<GameObject> activefragments = new List<GameObject>();
    private List<RectTransform> fragmentRectTransforms = new List<RectTransform>();
    private List<Vector2> startPos = new List<Vector2>();
    private List<float> offsets = new List<float>();
    private List<bool> isPlacedInNotebook = new List<bool>(); // NEW: Track placed fragments

    private Dictionary<TextFragment, int> fragment_to_Index = new Dictionary<TextFragment, int>();
    private int nextNotebookSlotIndex = 0;

    private int rows = 5;
    private int cols = 2;
    private float xStart = -150f;
    private float yStart = 400f;
    private float xSpacing = 400f;
    private float ySpacing = 150f;

    void Start()
    {
        if (text_fragmentPrefab == null)
        {
            Debug.LogError("text_fragmentPrefab is not assigned!");
            return;
        }
        if (text_fragmentParent == null)
        {
            Debug.LogError("text_fragmentParent is not assigned!");
            return;
        }
        if (notebookSlots == null || notebookSlots.Length == 0)
        {
            Debug.LogError("notebookSlots is not assigned or empty!");
            return;
        }

        SpawnKeywords();
    }

    void Update()
    {
        for (int i = 0; i < activefragments.Count; i++)
        {
            // Skip fragments that have been placed in the notebook
            if (isPlacedInNotebook[i])
                continue;

            RectTransform rt = fragmentRectTransforms[i];
            Vector2 currentPos = rt.anchoredPosition;

            float O = offsets[i];
            O = Mathf.Lerp(offsets[i], 0f, Time.deltaTime * 5f);
            offsets[i] = O;

            currentPos.y = startPos[i].y + O;
            rt.anchoredPosition = currentPos;
        }
    }

    void Shuffle(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            string temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    void SpawnKeywords()
    {
        List<string> shuffled = new List<string>(text_inFragments);
        Shuffle(shuffled);

        int max = Mathf.Min(rows * cols, shuffled.Count);

        for (int count = 0; count < max; count++)
        {
            int col = count / rows;
            int row = count % rows;

            Vector2 position = new Vector2(xStart + (col * xSpacing), yStart - (row * ySpacing));

            GameObject fragment = Instantiate(text_fragmentPrefab, text_fragmentParent);

            RectTransform rt = fragment.GetComponent<RectTransform>();
            rt.anchoredPosition = position;

            TextFragment text_Fragment = fragment.GetComponent<TextFragment>();

            bool isCorrect = System.Array.Exists(right_answers, answer => answer == shuffled[count]);
            text_Fragment.Initialize(shuffled[count], isCorrect, OnFragmentClicked);

            int index = activefragments.Count;
            activefragments.Add(fragment);
            fragmentRectTransforms.Add(rt);
            startPos.Add(position);
            offsets.Add(0f);
            isPlacedInNotebook.Add(false); // NEW: Initialize as not placed
            fragment_to_Index[text_Fragment] = index;
        }
    }

    void OnFragmentClicked(TextFragment frag)
    {
        if (!fragment_to_Index.TryGetValue(frag, out int index))
        {
            Debug.LogWarning($"Clicked fragment not found in map: {frag.name}");
            return;
        }

        if (index < 0 || index >= activefragments.Count)
        {
            Debug.LogWarning($"Index out of range for fragment: {index} (activeFragments.Count={activefragments.Count})");
            return;
        }

        if (nextNotebookSlotIndex >= notebookSlots.Length)
        {
            Debug.LogWarning("No more notebook slots available!");
            return;
        }

        frag.SetInteractable(false);
        GameObject fragmentObj = activefragments[index];

        startPos[index] = fragmentObj.GetComponent<RectTransform>().anchoredPosition;

        if (frag.isCorrect)
        {
            StartCoroutine(MoveToNotebook(fragmentObj, fragment_to_Index, frag, index, notebookSlots[nextNotebookSlotIndex]));
            nextNotebookSlotIndex++;
        }
    }

    private IEnumerator MoveToNotebook(GameObject fragMove, Dictionary<TextFragment, int> mapping, TextFragment frag, int index, Transform targetSlot)
    {
        RectTransform fragRT = fragMove.GetComponent<RectTransform>();
        RectTransform targetSlotRT = targetSlot.GetComponent<RectTransform>();
        RectTransform textFragmentsParent = fragRT.parent as RectTransform;
        Canvas rootCanvas = fragRT.GetComponentInParent<Canvas>();

        // Convert target slot world position to screen point
        Vector3 targetWorldPos = targetSlotRT.TransformPoint(Vector3.zero);
        Vector2 targetScreenPos = RectTransformUtility.WorldToScreenPoint(
            rootCanvas.worldCamera,
            targetWorldPos
        );

        // Convert screen pos to fragment's CURRENT parent space (for smooth movement)
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            textFragmentsParent,
            targetScreenPos,
            rootCanvas.worldCamera,
            out Vector2 endPosInTextFragments
        );

        Vector2 startPosInTextFragments = fragRT.anchoredPosition;

        // Animate in current parent
        float duration = 0.45f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / duration);
            fragRT.anchoredPosition = Vector2.Lerp(startPosInTextFragments, endPosInTextFragments, t);
            yield return null;
        }

        // --- RE-PARENT TO NOTEBOOK PANEL ---
        RectTransform notebookPanelRT = targetSlotRT.parent as RectTransform;

        // Store the target position BEFORE re-parenting
        Vector2 targetAnchoredPos = targetSlotRT.anchoredPosition;

        fragRT.SetParent(notebookPanelRT, false);

        // Apply all transform properties from the slot
        fragRT.anchorMin = targetSlotRT.anchorMin;
        fragRT.anchorMax = targetSlotRT.anchorMax;
        fragRT.pivot = targetSlotRT.pivot;
        fragRT.anchoredPosition = targetAnchoredPos;
        fragRT.sizeDelta = targetSlotRT.sizeDelta;

        fragRT.localRotation = Quaternion.identity;
        fragRT.localScale = Vector3.one;

        // Ensure it's visible and active
        fragMove.SetActive(true);

        // Make sure it renders on top
        fragMove.transform.SetAsLastSibling();

        // NEW: Mark this fragment as placed so Update() stops modifying it
        isPlacedInNotebook[index] = true;

        // Disable interactions
        frag.SetInteractable(false);
    }
}