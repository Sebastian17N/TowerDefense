using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Vector3[] points;
    public Vector3[] Points => points;
    private Vector3 _currentPositions;
    public Vector3 CurrentPositions => _currentPositions;
    private bool _gameStarted;

    void Start()
    {
        _gameStarted = true;
        _currentPositions = transform.position;
    }

    public Vector3 GetWaypointPosition(int index)
    {
        return CurrentPositions + Points[index];
    }
    private void OnDrawGizmos()
    {
        if (!_gameStarted && transform.hasChanged)
        {
            _currentPositions = transform.position;
        }
        for (int i = 0; i < points.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(points[i] + _currentPositions, 0.5f);

            if (i < points.Length -1)
            {
                Gizmos.color = Color.grey;
                Gizmos.DrawLine(points[i] + _currentPositions, points[i + 1] + _currentPositions);
            }
        }
    }
}
