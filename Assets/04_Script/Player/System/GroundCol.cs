using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GroundCol : MonoBehaviour
{
    
    public bool isGround { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        isGround = true;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        isGround = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        isGround = false;

    }

}
