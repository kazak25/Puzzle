using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelSettings : MonoBehaviour
{
    [SerializeField] private InputProcessing _input;
    [SerializeField] private GameObject _point;
    [SerializeField] private GameObject _chip;


    [SerializeField] private LineRenderer _line;
    [SerializeField] private List<Color> _colors = new List<Color>();

    public List<GameObject> _pointsList { get; } = new List<GameObject>();
    private List<GameObject> _chipsList = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        PointCreation();
        SetChipsPosition();
        DrawLine();
    }

    private void PointCreation()
    {
        var coordinates = _input.PointsCoordinates;
        foreach (var n in _input.PointsCoordinates)
        {
            var point = Instantiate(_point);
            point.GetComponent<MeshRenderer>().material.color = Color.black;
            _pointsList.Add(point);
            point.transform.position = n;
        }
    }

    private void SetChipsPosition()
    {
        foreach (var VARIABLE in _input.startPositionsOfChips)
        {
            // Debug.Log(VARIABLE);
        }

        for (int i = 1; i <= _input._chipsCount; i++)
        {
            var chip = Instantiate(_chip);
            chip.GetComponent<MeshRenderer>().material.color = _colors[i - 1];

            _chipsList.Add(chip);
            var point = _pointsList[_input.startPositionsOfChips[i - 1] - 1];
            chip.transform.position = point.transform.position;
        }
    }

    private void DrawLine()
    {
        _line.startWidth = 2f;
        _line.endWidth = 2f;


        var points = _pointsList;
        for (int i = 0; i < _input.connectionsCount; i++)
        {
            var point = points[i];
            var line = point.GetComponent<LineRenderer>();
            line.positionCount = 2;

            var connections = _input.ConnectionsBetweenPoints;
            int x = (int)connections[i].x;
            int y = (int)connections[i].y;

            line.SetPosition(0, points[x - 1].transform.position);
            line.SetPosition(1, points[y - 1].transform.position);
        }
    }
}