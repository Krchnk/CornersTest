using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPresenter
{
    private readonly BoardModel _boardModel;
    private readonly BoardView _boardView;

    private FigureModel _lastSelectedFigure;

    public BoardPresenter(BoardModel boardModel, BoardView boardView)
    {
        _boardModel = boardModel;
        _boardView = boardView;

        _boardView.Clicked += OnClicked;

        _boardView.Initialize(_boardModel.BoardSize);
    }

    private void OnClicked(Vector2Int position)
    {
        var isNoFigureSelected = LastSelectedFigure == null;

        if (isNoFigureSelected)
        {
            var clickedFigure = _boardModel.Board[position.x, position.y];
            if (clickedFigure != null)
            {
                LastSelectedFigure = _boardModel.Board[position.x, position.y];
            }
        }
        else
        {
            _boardModel.TryMoveFigure(LastSelectedFigure.Position, position);
            LastSelectedFigure = null;
        }
    }

    private FigureModel LastSelectedFigure 
    {
        get => _lastSelectedFigure;
        set 
        {
            _lastSelectedFigure = value;

            if (value == null)
            {
                _boardView.HideCellSelection();
            }
            else
            {
                _boardView.HighlightCell(value.Position);
            }
        }
    }

}
