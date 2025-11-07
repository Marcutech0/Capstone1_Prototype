using NUnit.Framework;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class ImagePopout : MonoBehaviour
{
    public RectTransform _CanvasRect;
    public List <GameObject> _PopoutImages;
    public float _PopInterval = 1f;
    public float _ActiveDuration = 0.5f;

    void Start()
    {
        StartCoroutine(PopOutRoutine());
    }

    IEnumerator PopOutRoutine() 
    {
        while (true)
        {
            if (_PopoutImages.Count > 0) 
            {
                int _Index = Random.Range(0, _PopoutImages.Count);
                GameObject _Image = _PopoutImages[_Index];
                RectTransform _Rect = _Image.GetComponent<RectTransform>();

                float _HalfWidth = _CanvasRect.rect.width / 2f;
                float _HalfHeight = _CanvasRect.rect.height / 2f;

                float _X = Random.Range(-_HalfWidth, _HalfHeight);
                float _Y = Random.Range(-_HalfHeight, _HalfHeight);

                _Rect.SetParent(_CanvasRect, false);
                _Rect.anchoredPosition = new Vector2(_X, _Y);

                _Image.SetActive(true);
                yield return new WaitForSeconds(_ActiveDuration);

                _Image.SetActive(false);
            }
            yield return new WaitForSeconds(_PopInterval);
        }
    }
}
