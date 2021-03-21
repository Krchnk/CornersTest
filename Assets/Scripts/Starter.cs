using System;
using UnityEngine;

public class Starter : MonoBehaviour
{
    [SerializeField] BoardView _boardView;
    [SerializeField] GameObject _blackFigure;
    [SerializeField] GameObject _whiteFigure;
    [SerializeField] private int _boardSize;
   

    void Start()
    {
        _boardView.StartGame += OnStart;
        var figureFactory = new FigureFactory(_whiteFigure, _blackFigure);
        var boardModel = new BoardModel(_boardSize, figureFactory);
        var boardPresenter = new BoardPresenter(boardModel, _boardView);
        boardModel.GenerateBoard();
    }

    private void OnStart()
    {
       //
    }
}
