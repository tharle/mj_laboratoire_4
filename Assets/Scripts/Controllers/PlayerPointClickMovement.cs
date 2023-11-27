using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.AI;

public class PlayerPointClickMovement : MonoBehaviour
{
    [SerializeField] float m_Range = 10f;
    
    private NavMeshAgent m_Agent;

    private float m_VisionAngle = Mathf.PI / 4; // en degrais

    private EnemyController m_EnemyTarget;

    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        MoveBy();
        CheckVision();
        
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
    }

    private EnemyController CheckAndReturnEnemyInRange() 
    {
        EnemyController enemy = null;
        Debug.DrawRay(transform.position, transform.forward * m_Range, Color.yellow);
        for (float angle = -m_VisionAngle; angle <= m_VisionAngle; angle+= Mathf.PI / 36)
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

    void OnDrawGizmosSelected()
    {
        DebugRange();
    }

    private void DebugRange()
    {
        Color red = Color.red;
        red.a = 0.2f;
        Gizmos.color = red;
        Gizmos.DrawSphere(transform.position, m_Range);
    }
}
