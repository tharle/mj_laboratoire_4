using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [SerializeField] private float m_DistanceInteraction = 1f;
    [SerializeField] Vector2 m_RangeCoinValue = new Vector2(15, 50);
    private int m_CoinValue;
    private bool m_IsOpened = false;
    // Start is called before the first frame update
    void Start()
    {
        m_CoinValue = (int) GetRandomCoinValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private float GetRandomCoinValue()
    {
        return Random.Range(m_RangeCoinValue.x, m_RangeCoinValue.y);
    }

    public float GetDistanceInteraction()
    {
        return m_DistanceInteraction;
    }

    public bool IsOpened()
    {
        return m_IsOpened;
    }

    public int Open()
    {
        if (m_IsOpened) return 0;

        m_IsOpened = true;

        Debug.Log("DO ACTION: "+name);
        Debug.Log("Add: " + m_CoinValue);

        // TODO do animation

        return m_CoinValue;
    }
}
