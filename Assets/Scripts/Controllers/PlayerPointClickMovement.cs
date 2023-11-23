using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerPointClickMovement : MonoBehaviour
{
    private NavMeshAgent m_Agent;

    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        MoveBy();
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
}
