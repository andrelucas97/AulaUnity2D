using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeperRange : MonoBehaviour
{
    public float attackCD;

    private float lastAttackTime = 0f;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            if (Time.time >= lastAttackTime + attackCD)
            {
                GetComponentInParent<Animator>().Play("Attack", -1);
                lastAttackTime = Time.time;

            }

        }
    }
}
