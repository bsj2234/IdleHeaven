using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrabber : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _height;
    [SerializeField] float _duration;
    private Vector3 _initialPos;
    private float _time;
    private bool _overHalfway = false;

    public Action OnHalfway { get; internal set; }

    public void GrabToTarget(Transform target)
    {
        _target = target;
        _initialPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (_target == null)
            return;
        if(_time >= _duration * .5f && !_overHalfway)
        {
            _overHalfway = true;
            OnHalfway?.Invoke();
        }
        //처음 위치에서 타겟 위치까지 이동시킴
        Vector3 tempPos = Vector3.Lerp(_initialPos, _target.position, _time / _duration);
        float nomyTime = _time / _duration;
        float halfOfDuration = .5f;
        float yTime = nomyTime - halfOfDuration;
        yTime = Mathf.Min(yTime, halfOfDuration);
        yTime = Mathf.Cos(Mathf.PI * -yTime);
        Vector3 yOffset = Vector3.up * yTime * _height;

        transform.position = tempPos + yOffset;

        _time += Time.fixedDeltaTime;
    }
}
