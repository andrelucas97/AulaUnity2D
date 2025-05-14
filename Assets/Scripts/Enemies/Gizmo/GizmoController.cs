using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoController : MonoBehaviour
{
    private CapsuleCollider2D colliderGizmo;
    private Animator anim;
    private float sideSign;
    private string side;

    public GameObject range;
    public int life;
    public float speed;
    public Transform player;

    void Start()
    {
        colliderGizmo = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();

    }

    void Update()
    {

        if (life <= 0)
        {
            this.enabled = false;
            colliderGizmo.enabled = false;
            range.SetActive(false);
            anim.Play("Die", -1);
        }


        if (anim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            return;
        }


        sideSign = Mathf.Sign(transform.position.x - player.position.x);

        if (Mathf.Abs(sideSign) == 1.0f)
        {
            side = sideSign == 1.0f ? "right" : "left";
        }

        // sideSign retorna -1 ou 1 (posição positiva ou negativa)
        // se for positivo(1), é right, se for negativo(0), é left

        switch (sideSign)
        {
            case -1: // right
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                break;

            case 1: //left
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
                break;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < 7f && distanceToPlayer > 2.5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, transform.position.y), speed* Time.deltaTime);
            anim.Play("Run", -1);
        }
        else
        {
            anim.Play("Idle", -1);
        }

    }
}
