using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private string m_NameEnemy = "Roberto";
    [SerializeField] private float m_HPMax = 10;
    [SerializeField] private Sprite m_Portrait;
    private float m_HP;

    private void Start()
    {
        m_HP = m_HPMax;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(GameParametres.TagName.PLAYER))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.TouchEnemy(this);
        }
    }

    public string GetNameEnemy()
    {
        return m_NameEnemy;
    }

    public float GetPercentHP()
    {
        return m_HP / m_HPMax;
    }

    public Sprite GetPortrait()
    {
        return m_Portrait;
    }

    public void TakeDamage(float damage)
    {
        m_HP -= damage;

        if(m_HP <= 0) {
            m_HP = 0;
            Destroy(gameObject);
        }
    }


}
