using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPresenter 
{
    private readonly BoardModel _boardModel;
    private readonly BoardView _boardView;

    public BoardPresenter(BoardModel boardModel, BoardView boardView)
    { 
        _boardModel = boardModel;
        _boardView = boardView;

        _boardModel.GenerateBoard += OnGenerateBoard;
        _boardModel.GenerateBoard += OnDrawTiles;
        _boardModel.FirstClick += FirstClick;
        _boardModel.UpTileFill += UpTileFill;
        _boardModel.LeftTileFill += LeftTileFill;
        _boardModel.MoveFigure += MoveFigure;
        _boardView.MouseButtonDown += GetCursorPosition;
    }

    private void MoveFigure()
    {
        _boardView.MoveFigure(_boardModel.PosXHighlight, _boardModel.PosYHighlight,_boardModel.PosXFirst, _boardModel.PosYFirst);
    }

    private void LeftTileFill()
    {
        _boardView.HighlightTile(_boardModel.PosXHighlight-1, _boardModel.PosYHighlight);
    }

    private void UpTileFill()
    {
        _boardView.HighlightTile(_boardModel.PosXHighlight, _boardModel.PosYHighlight + 1);
    }

    private void FirstClick()
    {
        _boardView.HighlightTile(_boardModel.PosXHighlight, _boardModel.PosYHighlight);
    }

    private void GetCursorPosition()
    {
        _boardModel.MouseClick(_boardView.PosX, _boardView.PosY);
    }
     
    private void OnGenerateBoard()
    {
        _boardView.GenerateBoard(_boardModel.Board);
    }

    private void OnDrawTiles()
    {
        _boardView.DrawTiles(_boardModel.Board);
    }


}
