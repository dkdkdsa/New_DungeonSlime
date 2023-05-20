using Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{

    [SerializeField] private List<string> jumpAbleTag = new List<string>();
    [SerializeField] private Vector2 posVector, size, ofs;
    [SerializeField] private float bouncePower;
    [SerializeField] private bool ativeMoveable = true;

    public void Jump()
    {

        var v = Physics2D.OverlapBox(transform.position + (Vector3)ofs, size, 0);

        if (!v) return;

        foreach (var tag in jumpAbleTag)
        {

            if (v.CompareTag(tag))
            {

                if (!ativeMoveable && v.TryGetComponent<IMoveAbleObject>(out var move))
                {

                    move.moveAble = false;
                    move.SetValMoveAble();

                }

                v.GetComponent<Rigidbody2D>().velocity += posVector * bouncePower;

            }

        }
    }

}
