using UnityEngine;
using System;

public class BoardModel
{
    private bool firstClick = true;
    private const int boardSize = 8;
    private FigureModel[,] _board = new FigureModel[boardSize, boardSize];
    public int PosXHighlight;
    public int PosYHighlight;
    public int PosXFirst;
    public int PosYFirst;

    public event Action GenerateBoard;
    public event Action FirstClick;
    public event Action UpTileFill;
    public event Action LeftTileFill;
    public event Action MoveFigure;

    public FigureModel[,] Board => _board;

    public void BoardGeneration()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 5; j < boardSize; j++)
            {
                _board[i, j] = new FigureModel(Color.white);
            }
        }

        for (int i = 5; i < boardSize; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                _board[i, j] = new FigureModel(Color.black);
            }
        }


        GenerateBoard?.Invoke();
    }

    public void MouseClick(int PosX, int PosY)
    {
        PosXHighlight = PosX;
        PosYHighlight = PosY;


        if (firstClick)
        {
            if (Board[PosYHighlight, PosXHighlight] != null)
            {
                PosXFirst = PosXHighlight;
                PosYFirst = PosYHighlight;

                // if (Board[PosYHighlight, PosXHighlight].Color == Color.white)

                if (_board[PosYHighlight + 1, PosXHighlight] == null)
                {
                    UpTileFill?.Invoke();
                }
                if (_board[PosYHighlight, PosXHighlight - 1] == null)
                {
                    LeftTileFill?.Invoke();
                }
                FirstClick?.Invoke();


            }
            firstClick = false;
        }
        else
        {
            _board[PosYHighlight, PosXHighlight] = _board[PosYFirst, PosXFirst];

            _board[PosYFirst, PosXFirst] = null;

            MoveFigure?.Invoke();

            firstClick = true;

        }



    }


}
