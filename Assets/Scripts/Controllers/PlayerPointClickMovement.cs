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

    private int m_VisionAngle = 45; // en degrais

    private EnemyController m_EnemyTarget;

    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        MoveBy();
        CheckEnemyInRange();
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

    private void CheckEnemyInRange() 
    {
        for (int angle = -m_VisionAngle; angle <= m_VisionAngle; angle+=5)
        {
            Vector3 direction = transform.forward;
            direction.x = direction.x * Mathf.Cos(angle);
            direction.z = direction.z * Mathf.Sin(angle);

            Debug.DrawRay(transform.position, direction * m_Range, Color.blue);
            m_EnemyTarget = GetEnemyInRange(direction);

            if (m_EnemyTarget != null) Debug.Log($"We find an ennemy looking at {direction}");
            if (m_EnemyTarget != null) break;

        }

        if (m_EnemyTarget != null)
        {

            //TODO Add code pour faire de dammage au ennemy
            m_Agent.isStopped = true;
            m_Agent.ResetPath();

            Debug.Log($"WE FIND A ENNEMY: {m_EnemyTarget.gameObject.name}!");
        }else
        {
            // Resume
            m_Agent.isStopped = false;
        }
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
