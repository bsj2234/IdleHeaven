using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrabber : MonoBehaviour
{
    [SerializeField] Transform _target;
    private Vector3 _initialPos;
    private float _time;
    [SerializeField]private float _duration;

    private void Grab()
    {
        _initialPos = transform.position;
    }

    private void FixedUpdate()
    {
        Debug.Assert(_target != null);
        //처음 위치에서 타겟 위치까지 이동시킴
        Vector3 tempPos = Vector3.Lerp(_initialPos, _target.position, _time / _duration);
        //처음엔 높이 0 이었다가 중간에 1 타겟위치에서는 0
        // 0 ~ 1  -.5f abs

        float yTime = Mathf.Abs(_time - _duration * .5f);
        Vector3 yOffset = Vector3.Slerp(Vector3.zero, Vector3.up * 5f, yTime);

        _target.position = tempPos;

        _time += Time.fixedDeltaTime;
    }
}
