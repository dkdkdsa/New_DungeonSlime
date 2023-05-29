using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : PlayerMovementRoot
{
    [SerializeField] private float JumpPower;

    protected override void Awake()
    {

        base.Awake();

        AddEvent();

    }

    private void Jump()
    {

        if (groundCol.isGround == false) return;

        AudioManager.Instance.PlayAudio("PlayerJump", audioSource);
        rigid.velocity += Vector2.up * JumpPower;

    }

    public override void AddEvent()
    {

        input.OnJumpEvent += Jump;

    }

    public override void RemoveEvent() 
    { 
        
        input.OnJumpEvent -= Jump;

    }

}
