using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEditor : MonoBehaviour
{

    [SerializeField] private string enemyName;

    private GameObject mainObject;

    public void CreateEnemy()
    {

        if (enemyName == "")
        {

            Debug.LogError("enemyName�� �߸���");
            return;

        }

        #region ���� ������Ʈ ����

        mainObject = CreateObject(enemyName);
        mainObject.AddComponent<SpriteRenderer>();
        mainObject.AddComponent<Animator>();

        #endregion

    }

    private GameObject CreateObject(string name = "Object")
    {

        return new GameObject(name);

    }

}
