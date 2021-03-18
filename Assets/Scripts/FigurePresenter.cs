using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FigurePresenter
{
    private FigureModel _figureModel;
    private FigureView _figureView;

    public FigurePresenter(FigureModel figureModel, FigureView figureView)
    {
        _figureModel = figureModel;
        _figureView = figureView;

        _figureModel.Moved += OnMoved;   
    }

    private void OnMoved(Vector2Int position)
    {
        _figureView.Move(position);
    }
}
