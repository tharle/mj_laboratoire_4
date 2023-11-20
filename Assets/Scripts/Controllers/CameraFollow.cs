using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform m_ToFollow;

    Vector3 m_Offset = Vector3.zero;

    void Start()
    {
        m_Offset = transform.position - m_ToFollow.position;
    }

    void Update()
    {
        if(Time.timeScale != 0) transform.position = m_ToFollow.position + m_Offset;
    }
}
