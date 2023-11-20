using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float DISTANCE_CHECK_COLLISION_FEET = 0.3f;

    [SerializeField] float m_Speed = 2;
    [SerializeField] float m_JumpForce= 15;
    [SerializeField] Transform m_Foot;
    private Rigidbody m_RigidBody;
    private HUDManager m_HUDManager;

    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_HUDManager = FindAnyObjectByType<HUDManager>();
    }

    void Update()
    {
        Debug.DrawRay(m_Foot.position, Vector3.down * DISTANCE_CHECK_COLLISION_FEET, Color.red);
        Move();
        if(IsGround() && Input.GetKeyDown(GameParametres.InputName.KEY_JUMP)) Jump();
    }

    private void Jump()
    {
        m_RigidBody.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
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
        if(Physics.Raycast(m_Foot.position, Vector3.down, out RaycastHit hitInfo, DISTANCE_CHECK_COLLISION_FEET))
        {
            return hitInfo.collider.CompareTag(GameParametres.TagName.GROUND);
        }

        return false;
    }

    public void TouchEnemy(EnemyController enemy) {
        if (IsSmashingEnemy(enemy))
        {
            Destroy(enemy.gameObject);
            Jump();
        } else
        {
            m_HUDManager.ShowGameOver();
            Destroy(gameObject);
        }
    }

    private bool IsSmashingEnemy(EnemyController enemy) {
        if (Physics.Raycast(m_Foot.position, Vector3.down, out RaycastHit hitInfo, DISTANCE_CHECK_COLLISION_FEET))
        {
            EnemyController enemyToucheByRayCast = hitInfo.collider.GetComponent<EnemyController>();
            return enemyToucheByRayCast == enemy;
        }

        return false;
    }
}
