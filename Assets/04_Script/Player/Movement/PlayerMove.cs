using Struct;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerMovementRoot
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private float increseMoveSpeed;
    [SerializeField] private float maxMoveSpeed;
    private float addMoveSpeed;

    protected override void Awake()
    {

        base.Awake();

        AddEvent();

    }

    private void Move(float value)
    {
        if(value == 0) addMoveSpeed = 1;
        if (addMoveSpeed <= maxMoveSpeed) addMoveSpeed += increseMoveSpeed/100f;

        rigid.velocity = new Vector2(value * moveSpeed * addMoveSpeed, rigid.velocity.y);
    }

    public override void AddEvent()
    {
        
        input.OnMovementEvent += Move;

    }

    public override void RemoveEvent()
    {

        input.OnMovementEvent -= Move;

    }

}
