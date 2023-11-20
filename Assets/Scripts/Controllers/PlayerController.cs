using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float DISTANCE_CHECK_COLLISION_GROUND = 1f;

    [SerializeField] float m_Speed = 2;
    [SerializeField] float m_JumpForce= 15;
    [SerializeField] Transform m_Foot;
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
        if (IsGround() && Input.GetKeyDown(GameParametres.InputName.KEY_JUMP))
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

    private bool IsGround()
    {
        if(Physics.Raycast(m_Foot.position, Vector3.down, out RaycastHit hitInfo, DISTANCE_CHECK_COLLISION_GROUND))
        {
            return hitInfo.collider.CompareTag(GameParametres.TagName.GROUND);
        }

        return false;
    }
}
