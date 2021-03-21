using UnityEngine;
using System.Collections.Generic;

public class BoardModel
{
    private readonly int _boardSize;
    private FigureFactory _figureFactory;
    private FigureModel[,] _board;

    public bool DiagonalRool=false;
    public bool HorisontalAndVerticalRool = false;
    

    public BoardModel(int boardSize, FigureFactory figureFactory)
    {
        _boardSize = boardSize;
        _figureFactory = figureFactory;
        _board = new FigureModel[boardSize, boardSize];
    }

    public FigureModel[,] Board => _board;
    public int BoardSize => _boardSize;

    public void GenerateBoard()
    {
        var figuresLineWidth = 3;
        for (int i = 0; i < figuresLineWidth; i++)
        {
            for (int j = _boardSize - figuresLineWidth; j < _boardSize; j++)
            {
                _board[i, j] = _figureFactory.Create(new Vector2Int(i, j), Checker.Color.Black);
            }
        }

        for (int i = _boardSize - figuresLineWidth; i < _boardSize; i++)
        {
            for (int j = 0; j < figuresLineWidth; j++)
            {
                _board[i, j] = _figureFactory.Create(new Vector2Int(i, j), Checker.Color.White);
            }
        }
    }

    public bool TryMoveFigure(Vector2Int origin, Vector2Int destination)
    {
        var movingFigure = _board[origin.x, origin.y];

        if (movingFigure != null)
        {
            var availableMoves = GetAvailableMoves(movingFigure);

            if ((availableMoves.Contains(destination)))
            {
                _board[destination.x, destination.y] = movingFigure;
                _board[origin.x, origin.y] = null;

                movingFigure.Position = destination;
                return true;
            }
        }
 
            return false;
    }

    public List<Vector2Int> GetAvailableMoves(FigureModel figureModel)
    {
        var result = new List<Vector2Int>();

        var directions = figureModel.Orientation == Vector2Int.up ?
            new Vector2Int[] { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right, Vector2Int.one, -Vector2Int.one, new Vector2Int(-1, 1), new Vector2Int(1, -1) } :
            new Vector2Int[] { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right, Vector2Int.one, -Vector2Int.one, new Vector2Int(-1, 1), new Vector2Int(1, -1) };

        foreach (var direction in directions)
        {
            var move = GetAvailableMoveInDirection(figureModel, direction);

            if (move != null)
            {
                result.Add((Vector2Int)move);
            }
        }

        return result;
    }

    private Vector2Int? GetAvailableMoveInDirection(FigureModel figureModel, Vector2Int direction)
    {

        var shortMove = figureModel.Position + direction;
        var wideMove = shortMove + direction;
        var isDiagonalMove = direction.x == direction.y || direction.x == -direction.y;


        if (!IsInsideBounds(shortMove))
            return null;
        
        if (_board[shortMove.x, shortMove.y] == null && !isDiagonalMove)
            return shortMove;

        if (!IsInsideBounds(wideMove))
            return null;

        if (DiagonalRool)
        {
            if (_board[shortMove.x, shortMove.y] != null &&
           _board[shortMove.x, shortMove.y].Color != figureModel.Color &&
           _board[wideMove.x, wideMove.y] == null &&
           isDiagonalMove)
                return wideMove;
        }

        if (HorisontalAndVerticalRool)
        {
            if (_board[shortMove.x, shortMove.y] != null &&
           _board[shortMove.x, shortMove.y].Color != figureModel.Color &&
           _board[wideMove.x, wideMove.y] == null &&
           !isDiagonalMove)
                return wideMove;
        }


        return null;
    }

    public bool IsInsideBounds(Vector2Int position)
    {
        return position.x >= 0 && position.x < _boardSize &&
                position.y >= 0 && position.y < _boardSize;
    }
}
