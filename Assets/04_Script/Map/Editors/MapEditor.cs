#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapEditor : MonoBehaviour
{

    [SerializeField] private int mapNumber;

    private GameObject mainMap;

    public void CreateDefaultMap()
    {

        if (mapNumber <= 0) return;

        if (mainMap != null)
        {

            DestroyImmediate(mainMap);

        }

        #region ����Ʈ �� ����

        mainMap = CreateObject(mapNumber.ToString());
        var map = mainMap.AddComponent<Map>();
        mainMap.AddComponent<Grid>();

        #endregion

        #region Ÿ�ϸ� ������Ʈ ����

        GameObject defaultTilemapObject = CreateObject("TileMap");
        defaultTilemapObject.AddComponent<Tilemap>();
        defaultTilemapObject.AddComponent<TilemapRenderer>();
        defaultTilemapObject.transform.SetParent(mainMap.transform);

        #endregion

        #region ������ġ ������Ʈ ����

        var startPos = CreateObject("StartPos");
        startPos.transform.SetParent(mainMap.transform);
        map.SetStartPos(startPos.transform);

        #endregion

    }

    public void SaveMap()
    {

        if (mapNumber <= 0 || mainMap == null) return;

        PrefabUtility.SaveAsPrefabAsset(mainMap,
            Application.dataPath + $"/Resources/Map/{mapNumber}.prefab");

    }

    public void LoadMap()
    {

        if (mapNumber <= 0) return;

        if(mainMap != null)
        {

            DestroyImmediate(mainMap);

        }

        mainMap = Resources.Load<GameObject>($"Map/{mapNumber}");
        Instantiate(mainMap);
        mainMap.name = mapNumber.ToString();

    }

    private GameObject CreateObject(string name = "Obj")
    {

        return new GameObject(name);

    }

}
#endif