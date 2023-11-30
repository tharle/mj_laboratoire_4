using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerPointClickObject : MonoBehaviour
{
    private NavMeshAgent m_Agent;
    private Rigidbody m_Rigidbody;

    private ObjectController m_ObjetTarget;
    private HUDScoreInfo m_HUDScoreInfo;

    bool m_isCollidedObject = false;
    private int m_Money = 0;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Agent = GetComponent<NavMeshAgent>();
        m_HUDScoreInfo = FindAnyObjectByType<HUDScoreInfo>();
    }

    void Update()
    {
        MoveBy();
        ActionObject();
    }

    private void MoveBy()
    {
        if (Input.GetMouseButtonDown((int)MouseButton.Left))
        {
            Ray rayFromCamera = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(rayFromCamera, out RaycastHit hitInfo))
            {
                CheckObjetMouseClick(hitInfo);
                m_Agent.SetDestination(hitInfo.point);
            }
        }
    }

    private void CheckObjetMouseClick(RaycastHit hit)
    {
        ObjectController objetTarget = hit.collider.gameObject.GetComponent<ObjectController>();

        if (objetTarget == null || objetTarget.IsOpened()) return;

        Debug.Log("WE CLICK OBJECT");

        m_ObjetTarget = objetTarget;
    }

    private void ActionObject()
    {
        if (m_ObjetTarget == null) return;

        if(GetDistanceFromObjetSelected() <= m_ObjetTarget.GetDistanceInteraction())
        {

            Debug.Log("WE ARE NEXT TO OBJECT");
            m_Rigidbody.velocity = Vector3.zero;
            m_Money += m_ObjetTarget.Open();
            m_HUDScoreInfo.OnMoneyChange(m_Money);
            m_ObjetTarget = null;
        }
    }

    private float GetDistanceFromObjetSelected()
    {
        return Vector3.Distance(m_ObjetTarget.transform.position, transform.position);
    }
}
