using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class FloatingEffect : MonoBehaviour
{
    [SerializeField] float bob_amplify = 0.5f;
    [SerializeField] float bob_frequency = 1f;
    
    private List<RectTransform> floating_texts = new List<RectTransform>();
    private Dictionary<RectTransform, Vector2> initial_positions = new Dictionary<RectTransform, Vector2>();
    private Dictionary<RectTransform, float> initial_scales = new Dictionary<RectTransform, float>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (RectTransform child in GetComponentsInChildren<RectTransform>())
        {
            if (child != this.transform)
            {
                floating_texts.Add(child);
                initial_positions[child] = child.anchoredPosition;
                initial_scales[child] = Random.Range(0.8f, Mathf.PI * 1.2f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.time;

        foreach (RectTransform rect in floating_texts)
        {
            Vector2 initial_pos = initial_positions[rect];
            initial_pos.y += Mathf.Sin(time * bob_frequency + initial_scales[rect]) * bob_amplify;
            rect.anchoredPosition = initial_pos;
        }



    }

    public void StopFloat(RectTransform text)
    {
        if (floating_texts.Contains(text))
        {
            floating_texts.Remove(text);
            text.anchoredPosition = initial_positions[text];
        }
        {
            
        }
    }
}
