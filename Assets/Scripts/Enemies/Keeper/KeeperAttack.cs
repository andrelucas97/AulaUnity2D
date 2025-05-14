using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeperAttack : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.life -= damage;
            }
        }
    }
}
