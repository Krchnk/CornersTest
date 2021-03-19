using UnityEngine;
using System;

public class BoardModel
{
    private int _boardSize ;
    private FigureFactory _figureFactory;
    private FigureModel[,] _board;

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
        for (int i = 0; i < 3; i++)
        {
            for (int j = 5; j < _boardSize; j++)
            {
                _board[i, j] = _figureFactory.Create(new Vector2Int(i, j), Checker.Color.Black);
            }
        }

        for (int i = 5; i < _boardSize; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                _board[i, j] = _figureFactory.Create(new Vector2Int(i, j), Checker.Color.White);
            }
        }
    }

    public void TryMoveFigure(Vector2Int origin, Vector2Int destination)
    {
        var movingFigure = _board[origin.x, origin.y];

        if (movingFigure != null)
        {
            if (_board[destination.x, destination.y] == null)
            {
                    _board[destination.x, destination.y] = movingFigure;
                    _board[origin.x, origin.y] = null;

                    movingFigure.Position = destination;
            }    
        }
    }
}
