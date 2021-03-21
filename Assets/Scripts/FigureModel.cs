using UnityEngine;
using System;

public class FigureModel
{
    private Checker.Color _color;
    private Vector2Int _position;
    private Color _highlightColor;

    public Vector2Int Orientation { get; set; }

    public FigureModel(Checker.Color color, Vector2Int position)
    {
        _color = color;
        this._position = position;
    }

    public event Action<Vector2Int> Moved;
    public event Action<Color> ChangedHighlightColor;

    public Checker.Color Color => _color;

    public Vector2Int Position 
    {
        get => _position; 
        set
        {
            _position = value;
            Moved(_position);
        } 
    }

    public Color HighlightColor
    {
        get => _highlightColor;
        set
        {
            _highlightColor = value;
            ChangedHighlightColor?.Invoke(_highlightColor);
        }
    }
}
