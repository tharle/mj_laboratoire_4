using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerPointClickMovement : MonoBehaviour
{
    [SerializeField] float m_Range = 10f;
    [SerializeField] GameObject m_Fireball;
    [SerializeField] float m_ForceFireball = 10f;

    private NavMeshAgent m_Agent;

    private float m_VisionAngle = Mathf.PI / 4; // en rad (45 degrais)
    private float m_VisionAmountRayCast = 8; // si se sont 8 rayons, seront 16 rayons au total

    private EnemyController m_EnemyTarget;
    private HUDEnemyInfo m_HUDEnemyInfo;

    private float m_ElapseFire = 0;
    private float m_CooldownFire = 0.5f;

    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_HUDEnemyInfo = FindAnyObjectByType<HUDEnemyInfo>();
    }

    void Update()
    {
        MoveBy();
        CheckVision();
        Combat();
    }

    private void MoveBy()
    {
        if (m_EnemyTarget != null) return;

        if (Input.GetMouseButtonDown((int) MouseButton.Left))
        {
            Ray rayFromCamera = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(rayFromCamera, out RaycastHit hitInfo))
            {
                m_Agent.SetDestination(hitInfo.point);
            }
        }
    }

    private void CheckVision()
    {
        if (m_EnemyTarget == null)
        {
            m_Agent.isStopped = false;
            m_EnemyTarget = CheckAndReturnEnemyInRange();
        }

        if (m_EnemyTarget != null)
        {
            m_Agent.isStopped = true;
            m_Agent.ResetPath();
        }

        m_HUDEnemyInfo.UpdateInfo(m_EnemyTarget);
    }

    private EnemyController CheckAndReturnEnemyInRange() 
    {
        EnemyController enemy = null;
        for (float angle = -m_VisionAngle; angle <= m_VisionAngle; angle+= m_VisionAngle / m_VisionAmountRayCast)
        {
            Vector3 direction = Vector3.zero;
            direction.x = transform.forward.x * Mathf.Cos(angle) - transform.forward.z * Mathf.Sin(angle);
            direction.z = transform.forward.x * Mathf.Sin(angle) + transform.forward.z * Mathf.Cos(angle);

            enemy = GetEnemyInRange(direction);

            if (enemy != null) break;

            Vector3.Distance(transform.position, transform.forward);

        }

        return enemy;

    }

    private EnemyController GetEnemyInRange(Vector3 direction)
    {
        EnemyController enemy = null;
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, m_Range))
            enemy = hit.collider.GetComponent<EnemyController>();

        return enemy;
    }

    private void Combat()
    {
        if (m_EnemyTarget == null) return;

        m_ElapseFire += Time.deltaTime;
        if (m_ElapseFire < m_CooldownFire) return;
        m_ElapseFire -= m_CooldownFire;

        Vector3 directionToEnemy = GetDirectionToEnemy();
        GameObject fireBall = Instantiate(m_Fireball, transform.position, Quaternion.identity);
        fireBall.GetComponent<FireBallController>().SetDistanceMax(m_Range);

        fireBall.GetComponent<Rigidbody>().AddForce(directionToEnemy * m_ForceFireball, ForceMode.Impulse);
    }

    private Vector3 GetDirectionToEnemy()
    {
        return (m_EnemyTarget.transform.position - transform.position).normalized;
    }
}
