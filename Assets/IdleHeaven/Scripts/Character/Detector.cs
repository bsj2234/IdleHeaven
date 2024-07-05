using System;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public Action<Transform> OnFoundTarget;
    public Action<Transform> LooseTargetHandler;

    private List<Transform> _targetsInDetector = new List<Transform>();

    [SerializeField] string _targetTag;

    public List<Func<Transform, bool>> additionalConditionForCleanup = new List<Func<Transform, bool>>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_targetTag))
        {
            if (OnFoundTarget != null)
            {
                Transform target = other.transform;

                _targetsInDetector.Add(target);
                OnFoundTarget.Invoke(target);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_targetTag))
        {
            if (LooseTargetHandler != null)
            {
                Transform target = other.transform;
                LooseTargetHandler(other.transform);

                _targetsInDetector.Remove(target);
            }
        }
    }
    public Transform GetNearestTarget()
    {
        CleanupTargets();
        if (_targetsInDetector.Count == 0)
        {
            return null;
        }
        _targetsInDetector.Sort(SortTarget);
        var result = _targetsInDetector[0];
        return result;
    }

    public List<Transform> GetSortedEnemys()
    {
        if (_targetsInDetector.Count == 0)
        {
            return null;
        }
        _targetsInDetector.Sort(SortTarget);
        return _targetsInDetector;
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
        return DistanceToLhs.CompareTo(DistanceToRhs);
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

    public void CleanupTargets()
    {
        for (int i = 0; i < _targetsInDetector.Count; i++)
        {

            bool anyContidionFailed = false;

            foreach (var condition in additionalConditionForCleanup)
            {
                anyContidionFailed = condition?.Invoke(_targetsInDetector[i]) ?? false;
                if (anyContidionFailed)
                {
                    break;
                }
            }

            bool isRemove = _targetsInDetector[i] == null || anyContidionFailed;

            if (isRemove)
            {
                _targetsInDetector.RemoveAt(i);
                if (i > 0)
                {
                    i--;
                }
            }
        }
    }
}
