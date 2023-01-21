using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField] private InputProcessing _input;

    [SerializeField] private CubesSelectt _cubesSelectt;
    [SerializeField] private LevelSettings _levelSettings;


    private List<GameObject> _list=new List<GameObject>();
    private int[,] graph;
    private int index;
    private int n;
    // Start is called before the first frame update
    void Start()
    {
        graph = new int[(_input._chipsCount+3)*2, (_input._chipsCount+3)*2];

        graph[0, 3] = 1;
        graph[3, 0] = 1;
        
        graph[1, 4] = 1;
        graph[4, 1] = 1;
        
        graph[2, 5] = 1;
        graph[5, 2] = 1;
        
        graph[3, 4] = 1;
        graph[4, 3] = 1;
        
        graph[4, 5] = 1;
        graph[5, 4] = 1;
        
        graph[3, 6] = 1;
        graph[6, 3] = 1;
        
        graph[4, 7] = 1;
        graph[7, 4] = 1;
        
        graph[5, 8] = 1;
        graph[8, 5] = 1;
        
      
    }

    private int FindMyPoint(List<GameObject> list,List<GameObject> list2)
    {
        for (var i = 0; i < list2.Count; i++)
        {
            if (list[0].transform.position == list2[i].transform.position)
            {
                return i;
            }
        }

        return 0;
    }

    private void Way()
    {
        
            for (var i1 = 0; i1 < graph.GetLength(1); i1++)
            {
                if (graph[index, i1] == 1)
                {
                    _levelSettings._pointsList[i1].GetComponent<MeshRenderer>().material.color = Color.white;
                    _list.Add(_levelSettings._pointsList[i1]);
                }
                 
            }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_cubesSelectt.selectedCubes.Count > 0)
        {
          index=  FindMyPoint(_cubesSelectt.selectedCubes,_levelSettings._pointsList);
          if (_list.Count > 0)
          {
              foreach (var VARIABLE in _list)
              {
                  VARIABLE.GetComponent<MeshRenderer>().material.color = Color.black;
              }
              _list.Clear();
          }

          
          Way();
        }
    }
}
