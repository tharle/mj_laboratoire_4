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
    }
}
