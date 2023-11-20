using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float m_Speed = 10;
    [SerializeField] float m_JumpForce= 5;
    private Rigidbody m_RigidBody;

    // Start is called before the first frame update
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    private void Jump()
    {
        if (Input.GetKeyDown(GameParametres.InputName.KEY_JUMP))
        {
            m_RigidBody.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
        }
    }

    private void Move()
    {
        float axisHorizontal = Input.GetAxis(GameParametres.InputName.AXIS_HORIZONTAL);
        float axisVertical = Input.GetAxis(GameParametres.InputName.AXIS_VERTICAL);
        Vector3 direction = Vector3.right * axisHorizontal + Vector3.forward * axisVertical;
        m_RigidBody.AddForce(direction * m_Speed, ForceMode.Force);
    }
}
