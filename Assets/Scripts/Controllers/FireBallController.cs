using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour
{
    [SerializeField] private Vector2 m_RangeDamage = new Vector2(1, 5);
    private float m_Damage;
    private float m_DistanceMax = 50f;
    private Vector3 m_StartPosition;
    private float m_ElapseTime = 0f;
    private float m_TimerCheckDistance = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        m_StartPosition = transform.position;
        m_Damage = GetRandomDamage();
    }

    private void Update()
    {
        m_ElapseTime += Time.deltaTime;
        if (m_ElapseTime >= m_TimerCheckDistance)
        {
            m_ElapseTime -= m_TimerCheckDistance;

            if (GetTravelledDistance() >= m_DistanceMax) Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameParametres.TagName.ENEMY))
        {
            other.GetComponent<EnemyController>().TakeDamage(m_Damage);

            Destroy(gameObject);
        }
    }

    private float GetTravelledDistance()
    {
        return Vector3.Distance(m_StartPosition, transform.position);    
    }

    private float GetRandomDamage()
    {
        return Random.Range(m_RangeDamage.x, m_RangeDamage.y);
    }

    public void SetDistanceMax(float distanceMax)
    {
        m_DistanceMax = distanceMax;
    }
}
