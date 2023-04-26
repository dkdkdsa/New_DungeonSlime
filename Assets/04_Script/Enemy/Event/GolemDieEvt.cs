using FD.Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemDieEvt :DieEvent
{

    public override void Die()
    {

        base.Die();

        //�� ����

        if(GetComponent<DieSensor>().dieTag == "Water")
        {

            FAED.Pop("GolemBlock", transform.position - new Vector3(0, 0.75f * 2), Quaternion.identity);

        }
        else
        {

            FAED.Pop("GolemBlock", transform.position - new Vector3(0, 0.75f), Quaternion.identity);

        }

        gameObject.SetActive(false);

    }

}