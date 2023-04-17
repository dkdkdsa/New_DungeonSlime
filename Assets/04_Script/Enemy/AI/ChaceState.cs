using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FD.AI.FSM;
using DG.Tweening;

public class ChaceState : FAED_FSMState
{


    [SerializeField] private float speed;
    private Rigidbody2D rigid;
    private Transform player;

    private void Awake()
    {

        player = GameObject.Find("Player").transform;
        rigid = transform.parent.GetComponent<Rigidbody2D>();

    }

    public override void EnterState()
    {


    }

    public override void UpdateState()
    {
        
        float value = player.position.x > transform.position.x ? speed : -speed;

        rigid.velocity = new Vector2(value, rigid.velocity.y);

    }

    public override void ExitState()
    {
        
        rigid.velocity = Vector2.zero;

    }

}
