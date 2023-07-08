using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public enum PathTypes
    {
        Linear,
        LinearLoop,
        Loop
    }

    public enum MovementDirection
    {
        Forward = 1,
        Back = -1
    }

    public PathTypes _pathType = PathTypes.LinearLoop;
    [SerializeField] private MovementDirection _movementDirection = MovementDirection.Forward;

    [SerializeField] private List<Vector3> _pathPositions;

    private int _movingTo;
    public float Length { get; private set; }

    private void OnDrawGizmos()
    {
        if(_pathPositions == null || _pathPositions.Count < 2)
        {
            Debug.LogError("Specify the path for the {name} object");
            return;
        }

        for(int i = 0; i < _pathPositions.Count - 1; i++)
        {
            Gizmos.DrawLine(_pathPositions[i], _pathPositions[i + 1]);

            if(_pathType == PathTypes.Loop)
            {
                Gizmos.DrawLine(_pathPositions[0], _pathPositions[_pathPositions.Count - 1]);
            }
        }
    }

    public IEnumerator<Vector3> GetNextPathPoint()
    {
        if (_pathPositions == null || _pathPositions.Count < 1)
            yield break;

        while (true)
        {
            yield return _pathPositions[_movingTo];

            if(_pathPositions.Count == 1)
                continue;

            switch (_pathType) 
            {
                case PathTypes.Linear:
                    {
                        if((_movementDirection == MovementDirection.Forward && _movingTo >= (_pathPositions.Count - 1))
                            ||
                            (_movementDirection == MovementDirection.Back && _movingTo <= 0))
                        {
                            yield break;
                        }

                        _movingTo += (int)_movementDirection;
                        break;
                    }
                case PathTypes.LinearLoop:
                    {
                        if (_movingTo <= 0)
                            _movementDirection = MovementDirection.Forward;
                        else if (_movingTo >= _pathPositions.Count - 1)
                            _movementDirection = MovementDirection.Back;

                        _movingTo += (int)_movementDirection;
                        break;
                    }
                case PathTypes.Loop:
                    {
                        _movingTo += (int)_movementDirection;

                        if (_movingTo >= _pathPositions.Count)
                        {
                            _movingTo = 0;
                        }

                        if (_movingTo < 0)
                        {
                            _movingTo = _pathPositions.Count - 1;
                        }
                        break;
                    }
            }
        }
    }

    public void SetPosition(Vector2 position)
    {
        if (!CanAppend(position)) return;

        _pathPositions.Add(position);
        if (_pathPositions.Count > 1)
            Length += Vector3.Distance(_pathPositions[_pathPositions.Count - 2], _pathPositions[_pathPositions.Count - 1]);
    }

    private bool CanAppend(Vector2 position)
    {
        if (_pathPositions.Count == 0) return true;

        return Vector2.Distance(_pathPositions[_pathPositions.Count - 1], position) > DrawManager.RESOLURION;
    }
}
