using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class X2Multiplier : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _SecondsValue;
    void Update()
    {
        int seconds = GameMaster.Instance.getMultiplierTimer();
        _SecondsValue.SetText(seconds.ToString());
    }
}
