using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureModel 
{
    private Color _color;
    public  Color Color => _color;


    public FigureModel(Color color)
    {
        _color = color;
    }

}
