using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FigureModel
{
    private Checker.Color _color;
    private Vector2Int position;

    public FigureModel(Checker.Color color, Vector2Int position)
    {
        _color = color;
        this.position = position;
    }

    public event Action<Vector2Int> Moved;
    public Checker.Color Color => _color;

    public Vector2Int Position 
    {
        get => position; 
        set
        {
            position = value;
            Moved(position);
        } 
    }

}
