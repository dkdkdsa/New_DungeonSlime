using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

    [SerializeField] private Transform startPos;

    public Transform StartPos => startPos;

    public void SetStartPos(Transform startPos)
    {

        this.startPos = startPos;

    }

}