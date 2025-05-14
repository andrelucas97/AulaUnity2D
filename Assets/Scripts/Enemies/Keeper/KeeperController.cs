using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeperController : MonoBehaviour
{
    private CapsuleCollider2D colliderKeeper;
    private Animator anim;
    private bool goRight;

    public int life;
    public float speed;
    public GameObject range;

    public Transform a;
    public Transform b;


    void Start()
    {
        colliderKeeper = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        if (life <= 0)
        {
            this.enabled = false;
            colliderKeeper.enabled = false;
            range.SetActive(false);
            anim.Play("Die", -1);
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            return;
        }

        if(goRight == true)
        {
            MovimentacaoKeeper(false, b, 0f);            
        }
        else
        {
            MovimentacaoKeeper(true, a, 180f);            
        }

        
    }
    
    void MovimentacaoKeeper(bool v, Transform x, float v1)
    {
        if (Vector2.Distance(transform.position, x.position) < 0.1f)
        {
            goRight = v;
        }

        transform.eulerAngles = new Vector3(0f, v1, 0f);
        transform.position = Vector2.MoveTowards(transform.position, x.position, speed * Time.deltaTime);
    }
}
