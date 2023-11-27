using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;

public class PlayerPointClickMovement : MonoBehaviour
{
    [SerializeField] float m_Range = 10f;
    [SerializeField] GameObject m_Fireball;
    [SerializeField] float m_ForceFireball = 10f;
    float m_ElapseTimeFireball = 0;
    float m_TimerFireball = 1;

    private NavMeshAgent m_Agent;

    private float m_VisionAngle = Mathf.PI / 4; // en rad (45 degrais)
    private float m_VisionAmountRayCast = 8; // si se sont 8 rayons, seront 16 rayons au total

    private EnemyController m_EnemyTarget;
    private HUDEnemyInfo m_HUDEnemyInfo;

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
        if (Input.GetMouseButtonDown((int) MouseButton.Left))
        {
            Ray rayFromCamera = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(rayFromCamera, out RaycastHit hitInfo))
            {
                m_Agent.SetDestination(hitInfo.point);
            }
            else
            {
                Debug.Log("We NO MOVE!");
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
            //TODO Add code pour faire de dammage au ennemy
            m_Agent.isStopped = true;
            m_Agent.ResetPath();

            Debug.Log($"WE FIND A ENNEMY: {m_EnemyTarget.gameObject.name}!");
        }

        m_HUDEnemyInfo.UpdateInfo(m_EnemyTarget);
    }

    private EnemyController CheckAndReturnEnemyInRange() 
    {
        EnemyController enemy = null;
        Debug.DrawRay(transform.position, transform.forward * m_Range, Color.yellow);
        for (float angle = -m_VisionAngle; angle <= m_VisionAngle; angle+= m_VisionAngle / m_VisionAmountRayCast)
        {
            Vector3 direction = Vector3.zero;
            direction.x = transform.forward.x * Mathf.Cos(angle) - transform.forward.z * Mathf.Sin(angle);
            direction.z = transform.forward.x * Mathf.Sin(angle) + transform.forward.z * Mathf.Cos(angle);

            Debug.DrawRay(transform.position, direction * m_Range, Color.blue);
            enemy = GetEnemyInRange(direction);

            if (enemy != null) Debug.Log($"We find an ennemy looking at {direction}");
            if (enemy != null) break;

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

        m_ElapseTimeFireball += Time.deltaTime;
        if (m_ElapseTimeFireball < m_TimerFireball) return;

        m_ElapseTimeFireball -= m_TimerFireball;

        GameObject fireBall = Instantiate(m_Fireball, transform.position, Quaternion.identity);
        fireBall.GetComponent<Rigidbody>().AddForce(transform.forward * m_ForceFireball, ForceMode.Impulse);
    }
}
