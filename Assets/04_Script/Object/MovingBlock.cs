using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    enum MoveState
    {
        None,
        MovePoint1,
        MovePoint2,
    }

    private LineRenderer _lineRenderer;

    [Header("���� ��ġ���� �󸶳� �̵��ϴ���")]
    [SerializeField] private Vector2 _moveTo;
    private Vector2 _point1;
    private Vector2 _point2;

    private MoveState _moveState = MoveState.MovePoint2;
    private Vector3 _dir;
    private float _movingSpeed = 3f;

    private BoxCollider2D _colider;
    private readonly float _correctNum = 0.1f;
    [Header("üũ �� �� ������Ʈ �̸� ����Ʈ")]
    [SerializeField] private List<string> _notCheckColList;

    private void Awake()
    {
        _colider = GetComponent<BoxCollider2D>();
        SetPosLineRenderer();
    }

    private void Update()
    {
        MoveBlock();
    }

    private void MoveBlock()
    {
        //���� �̵����� ����Ʈ ���� üũ
        Vector3 movePoint = Vector3.zero;
        Vector3 pos = transform.position;

        if (_moveState == MoveState.MovePoint1)
            movePoint = _point1;
        else if (_moveState == MoveState.MovePoint2)
            movePoint = _point2;

        movePoint.z = 0;
        pos.z = 0;

        _dir = (movePoint - pos).normalized;
        Vector3 moveVec = _dir * _movingSpeed * Time.deltaTime;

        //���� ������Ʈ �̵�, ���Ŀ� �� or ������ ����� �Ͼ�ٸ� LineCastAll�� ����
        RaycastHit2D[] hits = Physics2D.BoxCastAll(pos + Vector3.up * _correctNum, _colider.size - (Vector2.one * _correctNum), 0, Vector2.zero);
        List<Transform> hitTrms = new List<Transform>();

        //���� �ݶ��̴��� ������Ʈ���� ���� ������Ʈ��
        foreach (var hitObj in hits)
        {
            bool notCheck = false;
            for(int i = 0; i < _notCheckColList.Count; i++)
            {
                if(hitObj.transform.name == _notCheckColList[i])
                {
                    notCheck = true;
                    break;
                }
            }

            if (notCheck) continue;

            hitTrms.Add(hitObj.transform);
            hitObj.transform.SetParent(transform);
        }
            

        //�� �̵�
        transform.position = pos + moveVec;

        //���� �ݶ��̴��� ������Ʈ���� ���� ������Ʈ�� ����
        foreach (var hitObj in hitTrms)
            hitObj.SetParent(null);

        //���� üũ
        if ((transform.position - movePoint).sqrMagnitude < 0.025)
        {
            //transform.position = movePoint;
            _moveState = ((_moveState == MoveState.MovePoint1) ? MoveState.MovePoint2 : MoveState.MovePoint1);
        }
    }

    [ContextMenu("SetPosLineRenderer")]
    public void SetPosLineRenderer()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        _point1 = transform.position;
        _point2 = transform.position + (Vector3)_moveTo;

        _lineRenderer.SetPosition(0, _point1);
        _lineRenderer.SetPosition(1, _point2);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        _colider = GetComponent<BoxCollider2D>();
        Gizmos.DrawWireCube(transform.position + Vector3.up * _correctNum, _colider.size - Vector2.one * _correctNum);
    }
}
