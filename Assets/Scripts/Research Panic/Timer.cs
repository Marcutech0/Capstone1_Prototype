using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class Timer : MonoBehaviour
{
    public Slider _TimerSlider;
    public TextMeshProUGUI _TimerText;
    public float _MinigameDuration;
    public bool _StopTimer;

    void Start()
    {
        _StopTimer = false;
        _TimerSlider.maxValue = _MinigameDuration;
        _TimerSlider.value = _MinigameDuration;
    }

    // Update is called once per frame
    void Update()
    {
        float _Time = _MinigameDuration - Time.time;
        int _Minutes = Mathf.FloorToInt(_Time / 60);
        int _Seconds = Mathf.FloorToInt(_Time - _Minutes * 60f);
        string _TextTime = string.Format("{0:0}:{1:00}", _Minutes, _Seconds);

        if ( _Time <= 0) 
        {
            _StopTimer = true;
        }

        if (_StopTimer == false) 
        {
            _TimerText.text = _TextTime;
            _TimerSlider.value = _Time;
        }
    }
}
