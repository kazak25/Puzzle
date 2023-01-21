using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CubesSelectt : MonoBehaviour
{
    public List<GameObject> selectedCubes { get; } = new List<GameObject>();
    private List<Color> _startColorCube = new List<Color>();
    private List<GameObject> _coordinates = new List<GameObject>();
    public List<Color> _startCoordinateColors { get; } = new List<Color>();
    private float _currentTime;
    private float _speed = 100;
    private Vector3 _target;
    private int n;

    private void Raycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10000))
        {
            if (hit.collider.gameObject.CompareTag("P"))
            {
                SelectPoint(hit.collider.gameObject);
            }


            if (hit.collider.gameObject.CompareTag("Cube"))
            {
                SelectCube(hit.collider.gameObject);
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Raycast();
        }

        if (selectedCubes.Count != 0 && _coordinates.Count != 0)
        {
            _target = selectedCubes[0].transform.position;

            var distance = Vector3.Distance(_target, _coordinates[0].transform.position);
            var travelTime = distance / _speed;

            _currentTime += Time.deltaTime;
            var progress = _currentTime / travelTime;

            var result = Vector3.Lerp(_target, _coordinates[0].transform.position, progress);
            selectedCubes[0].transform.position = result;
            if (selectedCubes[0].transform.position == _coordinates[0].transform.position)
            {
                _coordinates.Clear();

                _currentTime = 0;
            }
        }
    }

    private void SelectPoint(GameObject gameObject)
    {
        if (_coordinates.Count > 0)
        {
            _coordinates.Clear();
        }

        _coordinates.Add(gameObject);
    }

    private void SelectCube(GameObject gameObject)
    {
        if (selectedCubes.Count > 0)
        {
            selectedCubes[0].GetComponent<MeshRenderer>().material.color = _startColorCube[0];
            selectedCubes.Clear();
            _startColorCube.Clear();
        }

        selectedCubes.Add(gameObject);
        _startColorCube.Add(gameObject.GetComponent<MeshRenderer>().material.color);
        gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        // Debug.Log(selectedCubes.Count);
    }
}