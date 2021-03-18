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

        _boardModel.FigureMoved += MoveFigure;
        _boardView.Clicked += OnClicked;

        _boardView.Initialize(_boardModel.BoardSize);
    }

    private void OnClicked(Vector2Int position)
    {
        var isNoFigureSelected = _lastSelectedFigure == null;

        if (isNoFigureSelected)
        {
            var clickedFigure = _boardModel.Board[position.x, position.y];
            if (clickedFigure != null)
            {
                _lastSelectedFigure = _boardModel.Board[position.x, position.y];
            }
        }
        else
        {
            _boardModel.TryMoveFigure(_lastSelectedFigure.Position, position);
            _lastSelectedFigure = null;
        }
    }

    private void MoveFigure()
    {
        //_boardView.MoveFigure(_boardModel.PosXHighlight, _boardModel.PosYHighlight,_boardModel.PosXFirst, _boardModel.PosYFirst);
    }
}
