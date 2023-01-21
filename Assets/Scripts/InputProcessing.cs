using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InputProcessing : MonoBehaviour
{
    public List<Vector2> PointsCoordinates { get; } = new List<Vector2>();
    public List<Vector2> ConnectionsBetweenPoints { get; } = new List<Vector2>();
    public List<int> startPositionsOfChips { get; } = new List<int>(); //+
    public int _chipsCount { get; private set; } // +
    private int _pointsCount; // +
    public int connectionsCount { get; private set; }


    private List<int> finalPositionsOfChips = new List<int>(); //+
   

    private List<string> _text = new List<string>(); //+

    private int _connectionsPointsStartIndex;
    private int _connectionsPointsFinishIndex;
    private int _pointsCoordinatesStartIndex = 2;
    private int _pointsCoordinatesFinishindex;
    private int _indexStartPosition; //+
    private int _indexFinalPosition; //+

    private StreamReader _streamReader;
    string path = "E:/UnityProjects/Test.txt";

    private void Awake()
    {
        _streamReader = new StreamReader(path);
        TextInitialize(_streamReader);
    }

    private void TextInitialize(StreamReader streamReader)
    {
        string line = streamReader.ReadLine();
        while (line != null)
        {
            _text.Add(line);
            line = streamReader.ReadLine();
        }

        InitializeIndex();
        PointsCoordinatesInitialize(PointsCoordinates, _text, _pointsCoordinatesStartIndex,
            _pointsCoordinatesFinishindex);
        PositionsInitialize(_text);
        PointsCoordinatesInitialize(ConnectionsBetweenPoints, _text, _connectionsPointsStartIndex,
            _connectionsPointsFinishIndex);
    }

    private void PointsCoordinatesInitialize(List<Vector2> list, List<string> text, int Startindex, int FinishIndex)
    {
        string line;
        string[] words;
        for (var i = Startindex; i <= FinishIndex; i++)
        {
            var x = 0;
            var y = 0;
            line = text[i];
            words = line.Split(new Char[] { ',' });

            x = Convert.ToInt32(words[0]);
            y = Convert.ToInt32(words[1]);
            var vector = new Vector2(x, y);
            list.Add(vector);
        }

        foreach (var VARIABLE in list)
        {
           // Debug.Log(VARIABLE);
        }
    }

    private void PositionsInitialize(List<string> text)
    {
        _indexStartPosition = _pointsCount + 2;
        _indexFinalPosition = _pointsCount + 3;
        _connectionsPointsStartIndex = _indexFinalPosition + 2;
        connectionsCount = Convert.ToInt32(_text[_indexFinalPosition + 1]);
        _connectionsPointsFinishIndex = _indexFinalPosition + 1 + connectionsCount;

        string line;
        string[] words;

        var start = text[_indexStartPosition];
        words = start.Split(new Char[] { ',' });

        foreach (var n in words)
        {
            startPositionsOfChips.Add(Convert.ToInt32(n));
        }

        var finish = text[_indexFinalPosition];
        words = finish.Split(new Char[] { ',' });

        foreach (var n in words)
        {
            finalPositionsOfChips.Add(Convert.ToInt32(n));
        }
    }

    private void InitializeIndex()
    {
        _chipsCount = Convert.ToInt32(_text[0]);
        _pointsCount = Convert.ToInt32(_text[1]);
        _pointsCoordinatesFinishindex = _pointsCount + 1;
    }
}