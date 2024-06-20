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

    public void GrabToTarget(Transform target)
    {
        _target = target;
        _initialPos = transform.position;
    }

    private void FixedUpdate()
    {
        Debug.Log(_target != null);
        if (_target == null)
            return;
        //ó�� ��ġ���� Ÿ�� ��ġ���� �̵���Ŵ
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