using UnityEngine;
using TMPro;
public class ReleaseManager : MonoBehaviour
{
    public TextMeshProUGUI _ReleasedTracker;
    public TextMeshProUGUI _OverfillTracker;
    public int _ReleasedCount;
    public int _OverfillCount;
    
    public void ReleaseCup() 
    {
        // Gets the current selected cup
        if (CupClickManager._CurrentlySelectedCup == null) return;

        var _CurrentCup = CupClickManager._CurrentlySelectedCup;

        float _Fill = CupClickManager._CurrentlySelectedCup._FillPercent;
        // if fill percent is 80 - 100% add 1 to the release count
        if (_Fill >= 80f && _Fill <= 100f)
        {
            _ReleasedCount++;
            _ReleasedTracker.text = $"Released: {_ReleasedCount}";
            Destroy(_CurrentCup.gameObject);
            Debug.Log("Cup Released Successfully");
        }

        // if fill percent is less than 100% add 1 to overfill count
        else if (_Fill < 100f)
        {
            _OverfillCount++;
            _OverfillTracker.text = $"Overfilled: {_OverfillCount}";
            Destroy(_CurrentCup.gameObject);
            Debug.Log("Cup Overfilled");
        }
    }
}
