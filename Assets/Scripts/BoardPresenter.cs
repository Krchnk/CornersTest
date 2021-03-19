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
                /*
                #region HighLightMove  
                if (position.y < 7)
                {
                    if (_boardModel.Board[position.x, position.y + 1] == null)
                    {
                        var pos = new Vector2Int(position.x, position.y + 1);
                        _boardView.HighlightUpMoveCell(pos);
                    }
                }
                if (position.y > 0)
                {
                    if (_boardModel.Board[position.x, position.y - 1] == null)
                    {
                        var pos = new Vector2Int(position.x, position.y - 1);
                        _boardView.HighlightDownMoveCell(pos);
                    }
                }
                if (position.x > 0)
                {
                    if (_boardModel.Board[position.x - 1, position.y] == null)
                    {
                        var pos = new Vector2Int(position.x - 1, position.y);
                        _boardView.HighlightLeftMoveCell(pos);
                    }
                }
                if (position.x < 7)
                {
                    if (_boardModel.Board[position.x + 1, position.y] == null)
                    {
                        var pos = new Vector2Int(position.x + 1, position.y);
                        _boardView.HighlightRightMoveCell(pos);
                    }
                }
                #endregion
                */
            }
        }
        else
        {

            //фишка не может перепрыгивать, а делает только один шаг в любом направлении.
            if (((LastSelectedFigure.Position.y + 1 == position.y||LastSelectedFigure.Position.y - 1 == position.y) 
                && LastSelectedFigure.Position.x == position.x)
                ||
                ((LastSelectedFigure.Position.x + 1 == position.x || LastSelectedFigure.Position.x - 1 == position.x)
                && LastSelectedFigure.Position.y == position.y))
                
            {
                _boardModel.TryMoveFigure(LastSelectedFigure.Position, position);
            }
            //фишка перепрыгивает по вертикали и горизонтали.
            if(
               ( LastSelectedFigure.Position.y + 2 == position.y&&
                _boardModel.Board[LastSelectedFigure.Position.x, LastSelectedFigure.Position.y+ 1]!=null)||
                (LastSelectedFigure.Position.y - 2 == position.y &&
                _boardModel.Board[LastSelectedFigure.Position.x, LastSelectedFigure.Position.y - 1] != null)||
                (LastSelectedFigure.Position.x + 2 == position.x &&
                _boardModel.Board[LastSelectedFigure.Position.x+1, LastSelectedFigure.Position.y] != null) ||
                (LastSelectedFigure.Position.x - 2 == position.x &&
                _boardModel.Board[LastSelectedFigure.Position.x - 1, LastSelectedFigure.Position.y] != null)
                )
            {
                _boardModel.TryMoveFigure(LastSelectedFigure.Position, position);
            }
            //фишка может перепрыгивать через другую как в шашках, по диагонали.
            if (
               (LastSelectedFigure.Position.y + 2 == position.y && LastSelectedFigure.Position.x + 2 == position.x&&
                 _boardModel.Board[LastSelectedFigure.Position.x+1, LastSelectedFigure.Position.y + 1] != null)||
               (LastSelectedFigure.Position.y + 2 == position.y && LastSelectedFigure.Position.x - 2 == position.x &&
                 _boardModel.Board[LastSelectedFigure.Position.x - 1, LastSelectedFigure.Position.y + 1] != null) 
               )
            {
                _boardModel.TryMoveFigure(LastSelectedFigure.Position, position);
            }



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
