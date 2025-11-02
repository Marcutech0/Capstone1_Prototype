using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject text_fragmentPrefab;
    [SerializeField] private RectTransform text_fragmentParent;
    [SerializeField] private Transform[] notebookSlots;

    [SerializeField] private string[] text_inFragments;
    [SerializeField] private string[] right_answers;

    private List<GameObject> activefragments = new List<GameObject>();
    private List<Vector2> startPos = new List<Vector2>();
    private List<float> offsets = new List<float>();

    private Dictionary<TextFragment, int> fragment_to_Index = new Dictionary<TextFragment, int>();
    private int nextNotebookSlotIndex = 0;

    private int rows = 5;
    private int cols = 2;
    private float xStart = -150f;
    private float yStart = 400f;
    private float xSpacing = 400f;
    private float ySpacing = 150f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnKeywords();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < activefragments.Count; i++)
        {
            GameObject Fragments = activefragments[i];
            {
                RectTransform rt = Fragments.GetComponent<RectTransform>();
                Vector2 currentPos = rt.anchoredPosition;

                float O = offsets[i];
                O = Mathf.Lerp(offsets[i], 0f, Time.deltaTime * 5f);
                offsets[i] = O;

                currentPos.y = startPos[i].y + O;
                rt.anchoredPosition = currentPos;
            }
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
            startPos.Add(position);
            offsets.Add(0f);
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

            frag.SetInteractable(false);
            GameObject fragmentObj = activefragments[index];

            startPos[index] = fragmentObj.GetComponent<RectTransform>().anchoredPosition;

            if (frag.isCorrect)
            {
                int safeIndex = nextNotebookSlotIndex % notebookSlots.Length;

                StartCoroutine(MoveToNotebook(fragmentObj, fragment_to_Index, frag, index, notebookSlots[safeIndex]));

                nextNotebookSlotIndex++;
            }
            else
            {
                if (index >= 0 && index < offsets.Count) offsets[index] = 10f;
            }
        }

    private IEnumerator MoveToNotebook(GameObject fragMove, Dictionary<TextFragment, int> mapping, TextFragment frag, int index, Transform targetSlot)
    {
        RectTransform rt = fragMove.GetComponent<RectTransform>();
        RectTransform targetParent = targetSlot.GetComponent<RectTransform>();

        Vector3 startPos = rt.anchoredPosition;
        Vector3 end = targetParent.anchoredPosition;
        float duration = 0.5f;
        float time = 0f;

        while (time < duration)
        {
           time += Time.deltaTime;
            rt.position = Vector3.Lerp(startPos, end, time / duration);
            yield return null;
        }
        rt.position = end;

        rt.SetParent(targetSlot, false);

        rt.anchoredPosition = Vector2.zero;
        rt.localRotation = Quaternion.identity;
        rt.localScale = Vector3.one;


        yield break;
    }



}





