using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour
{
    [SerializeField] private Vector2 m_RangeDamage = new Vector2(1, 5);
    private float m_Damage;

    // Start is called before the first frame update
    void Start()
    {
        m_Damage = GetRandomDamage();
    }

    private float GetRandomDamage()
    {
        return Random.Range(m_RangeDamage.x, m_RangeDamage.y);
    }
}
