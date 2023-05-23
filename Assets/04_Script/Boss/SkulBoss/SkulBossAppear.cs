using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkulBossAppear : MonoBehaviour
{

    public float appearPos, backPos;
    float shake = -180;
    const float shakeAomunt = 0.025f;

    void Awake()
    {
        transform.DOMoveY(appearPos, 1f).SetEase(Ease.OutBack)
        .OnComplete(() =>
        {
            transform.DOMoveY(backPos, 1f).SetEase(Ease.InCirc);
        });
    }

    void Update()
    {
        shake += shakeAomunt;
        transform.position += new Vector3(0, Mathf.Sin(shake) / 200f, 0);
    }
}
