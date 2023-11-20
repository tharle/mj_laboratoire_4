using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(GameParametres.TagName.PLAYER))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.TouchEnemy(this);
        }
    }
}
