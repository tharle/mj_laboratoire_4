using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDScoreInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_TextMoneyValue;

    public void OnMoneyChange(int money)
    {
        m_TextMoneyValue.text = money.ToString("00");
    }
}
