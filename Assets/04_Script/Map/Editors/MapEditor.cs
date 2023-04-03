#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditor : MonoBehaviour
{

    [SerializeField] private int mapNumber;

    public void CreateDefaultMap()
    {

        if (mapNumber <= 0) return;

        #region ����Ʈ �� ����

        GameObject mainMap = CreateObject(mapNumber.ToString());

        #endregion

    }

    private GameObject CreateObject(string name = "Obj")
    {

        return new GameObject(name);

    }

}
#endif