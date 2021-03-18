using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    [SerializeField] BoardView _boardView;
    [SerializeField] GameObject _blackFigure;
    [SerializeField] GameObject _whiteFigure;

    #region Configuration
    [SerializeField] private int _boardSize;
    #endregion

    void Start()
    {
        var figureFactory = new FigureFactory(_whiteFigure, _blackFigure);

        var boardModel = new BoardModel(_boardSize, figureFactory);
        var boardPresenter = new BoardPresenter(boardModel, _boardView);
        boardModel.GenerateBoard();
    }
}
