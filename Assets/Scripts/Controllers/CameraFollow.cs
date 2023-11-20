using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform m_ToFollow;

    Vector3 m_Offset = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        m_Offset = transform.position - m_ToFollow.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = m_ToFollow.position + m_Offset;
    }
}
