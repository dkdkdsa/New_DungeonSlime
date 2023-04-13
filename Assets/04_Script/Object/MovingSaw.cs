using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovingSaw : MonoBehaviour
{
    private int _crtGotoPosIdx = 1;
    private bool _isBack = false;

    [Header("�̵� ������ ����")]
    [SerializeField]
    private List<Vector2> _points = new List<Vector2>();

    [Header("���� ������ ��뿩��")]
    [SerializeField]
    private bool _useLineRenderer = true;
    private LineRenderer _lineRenderer;

    Transform _trm;

    [Header("�ӵ� ����")]
    [SerializeField]
    private float _rotateSpeed;
    [SerializeField]
    private float _moveSpeed;

    private float _rotationValue = 0;

    private void Awake()
    {
        _trm = transform;

        SetPosLineRenderer();
    }

    private void Update()
    {
        RotateSaw();
        MoveSaw();
    }

    private void RotateSaw()
    {
        _trm.rotation = Quaternion.Euler(0, 0, _rotationValue);
        _rotationValue = _rotationValue + _rotateSpeed * Time.deltaTime;
    }

    private void MoveSaw()
    {
        //���� �̵����� ����Ʈ ���� üũ
        Vector3 pos = transform.position;
        Vector3 movePoint = Vector3.zero;
        movePoint = _points[_crtGotoPosIdx % _points.Count];

        movePoint.z = 0;
        pos.z = 0;

        Vector3 dir = (movePoint - pos).normalized;
        Vector3 moveVec = dir * _moveSpeed * Time.deltaTime;

        //�� �̵�
        transform.position = pos + moveVec;

        //���� üũ
        if ((transform.position - movePoint).sqrMagnitude < 0.01)
        {
            transform.position = _points[_crtGotoPosIdx];

            if (_crtGotoPosIdx == _points.Count - 1)
                _isBack = true;
            else if (_crtGotoPosIdx == 0)
                _isBack = false;

            // ����
            if(_isBack == false)
                _crtGotoPosIdx++;
            else
                _crtGotoPosIdx--;
        }
    }

    [ContextMenu("SetPosLineRenderer")]
    public void SetPosLineRenderer()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = _useLineRenderer;

        transform.position = _points[0];

        _lineRenderer.positionCount = _points.Count;
        for (int i = 0; i < _points.Count; i++)
            _lineRenderer.SetPosition(i, _points[i]);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for(int i = 0; i < _points.Count; i++)
        {
            Gizmos.DrawSphere(_points[i], 0.1f);
        }
    }
}
