using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class FollowPath : MonoBehaviour
{
    public enum MovementType
    {
        Lerp,
        Constant
    }

    [SerializeField] private float _speed;
    [SerializeField][Tooltip("The object will reach the end of the path within the specified time interval")] private bool _moveInTime;
    [SerializeField][Tooltip("the period of time for which the object will reach the end of the path, if it moves depending on it")] private float _time;
    [SerializeField] private float _minDistance = .1f;
    [SerializeField] private DrawManager _drawManager;
    [SerializeField] private MovementType movementType;

    private Coroutine _coroutine;
    private IEnumerator<Vector3> _pointInPath;

    public Action EndMoving;

    public void StartMoving()
    {
        _pointInPath = _drawManager.MovingPath.GetNextPathPoint();
        _pointInPath.MoveNext();

        if (_pointInPath.Current == null)
        {
            Debug.LogError($"The {name} object does not have enough points to move along the way");
            return;
        }

        StopMoving();
        StartCoroutine(MoveAlongThePath());
    }

    public void StopMoving()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator MoveAlongThePath()
    {
        if (_pointInPath == null || _pointInPath.Current == null)
        {
            Debug.LogError($"The path of the {name} object is missing");
            yield break;
        }

        if (_moveInTime)
        {
            _speed = _drawManager.MovingPath.Length / _time;
        }

        while (true)
        {
            yield return new WaitForEndOfFrame();

            switch (movementType)
            {
                case MovementType.Constant:
                    {
                        transform.position = Vector2.MoveTowards(transform.position, _pointInPath.Current, Time.deltaTime * _speed);
                        break;
                    }
                case MovementType.Lerp:
                    {
                        transform.position = Vector2.Lerp(transform.position, _pointInPath.Current, Time.deltaTime * _speed);
                        break;
                    }
            }

            var distance = Vector2.Distance(transform.position, _pointInPath.Current);
            if (distance < _minDistance)
            {
                if (!_pointInPath.MoveNext())
                {
                    EndMoving?.Invoke();
                    yield break;
                }
            }
        }
    }
}
