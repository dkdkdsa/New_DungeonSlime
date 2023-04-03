using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    public int currentStageNum { get; private set; } = 1;

    public void SetCurrentStageNumber(int number)
    {

        currentStageNum = number;

    }

    public void CreateStage()
    {

        var map = Instantiate(Resources.Load<GameObject>($"Map/{currentStageNum}")).GetComponent<Map>();

        var player = GameObject.Find("Player");
        player.transform.position = map.StartPos.position;

    }

}
