using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private float m_Distance = 10f;
    private Vector3 m_Direction;

    private void Start()
    {
        m_Direction = transform.forward;
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, m_Direction * m_Distance, Color.red);

        CheckTouchAllObjets();
    }

    private void CheckTouchAllObjets()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, m_Distance);
        if (hits.Length > 0)
        {
            Debug.Log("We Hit " + hits.Length + " objet(s)");
            foreach (RaycastHit hit in hits)
            {
                if(hit.collider.GetComponent<EnemyController>() != null)
                {
                    Debug.Log("Enemy " + hit.collider.gameObject.name + " is dead!");
                    Destroy(hit.collider.gameObject);
                }
            }
        }
        else {
            Debug.Log("We Miss");
        }

    }
}
