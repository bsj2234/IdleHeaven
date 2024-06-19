using System;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public Action<Transform> OnFoundTarget;
    public Action<Transform> LooseTargetHandler;

    private List<Transform> _targetsInDetector = new List<Transform>();

    [SerializeField] string _targetTag;

    public Func<bool> additionalConditionForDelete;
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"{name} Trigger Enter");
        if (other.CompareTag(_targetTag))
        {
            if(OnFoundTarget != null)
            {
                Transform target = other.transform;
                OnFoundTarget.Invoke(target);

                _targetsInDetector.Add(target);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_targetTag))
        {
            if(LooseTargetHandler != null)
            {
                Transform target = other.transform;
                LooseTargetHandler(other.transform);

                _targetsInDetector.Remove(target);
            }
        }
    }


    public Transform GetNearestTarget()
    {
        if (_targetsInDetector.Count == 0)
        {
            return null;
        }
        _targetsInDetector.Sort(SortTarget);
        _targetsInDetector.RemoveAll((item) => item == null);
        if (_targetsInDetector.Count == 0)
        {
            return null;
        }


        int i = _targetsInDetector.Count - 1;
        var result = _targetsInDetector[i];


        if (additionalConditionForDelete != null)
        {
            while (additionalConditionForDelete.Invoke())
            {
                _targetsInDetector.RemoveAt(i--);
                result = _targetsInDetector[i];
            }
        }
        return result;
    }

    private void DebugTarget()
    {
        string targetArray = "";
        foreach (var enemy in _targetsInDetector)
        {
            targetArray = $"{targetArray} ,{enemy.transform.name}";
        }
        Debug.Log(targetArray);
    }


    private int SortTarget(Transform lhs, Transform rhs)
    {
        if (lhs == null)
        {
            return -1;
        }
        if (rhs == null)
        {
            return 1;
        }
        float DistanceToLhs = Vector3.Distance(lhs.position, transform.position);
        float DistanceToRhs = Vector3.Distance(rhs.position, transform.position);
        return DistanceToRhs.CompareTo(DistanceToLhs);
    }



    public void RemoveTarget(Transform target)
    {
        if (_targetsInDetector.Count == 0)
        {
            Debug.Log("Tried pop but empty");
            return;
        }
        _targetsInDetector.Remove(target);
    }
}
