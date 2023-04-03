#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapEditor : MonoBehaviour
{

    [SerializeField] private int mapNumber;

    public void CreateDefaultMap()
    {

        if (mapNumber <= 0) return;

        #region ����Ʈ �� ����

        GameObject mainMap = CreateObject(mapNumber.ToString());
        mainMap.AddComponent<Grid>();

        #endregion

        #region Ÿ�ϸ� ������Ʈ ����

        GameObject defaultTilemapObject = CreateObject("TileMap");
        defaultTilemapObject.AddComponent<Tilemap>();
        defaultTilemapObject.AddComponent<TilemapRenderer>();

        #endregion

    }

    private GameObject CreateObject(string name = "Obj")
    {

        return new GameObject(name);

    }

}
#endif