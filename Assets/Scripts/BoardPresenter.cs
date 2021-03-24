using UnityEngine;

public class BoardPresenter
{
    private readonly BoardModel _boardModel;
    private readonly BoardView _boardView;

    private FigureModel _lastSelectedFigure;

    private bool _switcher = true;

    public BoardPresenter(BoardModel boardModel, BoardView boardView)
    {
        _boardModel = boardModel;
        _boardView = boardView;

        _boardView.Clicked += OnClicked;
        _boardView.DiagonalRulesSet += OnDiagonalRulesSet;
        _boardView.HorisontalAndVerticalRoolSet += OnHorisontalAndVerticalRool;
        _boardView.NoWideMoveRoolSet += OnNoWideMoveRool;
        _boardView.Initialize(_boardModel.BoardSize);
    }

    private void OnNoWideMoveRool()
    {
        _boardModel.HorisontalAndVerticalRool = false;
        _boardModel.DiagonalRool = false;
    }

    private void OnHorisontalAndVerticalRool()
    {
        _boardModel.HorisontalAndVerticalRool = true;
    }

    private void OnDiagonalRulesSet()
    {
        _boardModel.DiagonalRool = true;
    }

    private void ChangePlayer()
    {
        _switcher = !_switcher;

        var col = LastSelectedFigure.Color;
        foreach (var item in _boardModel.Board)
        {
            if (item != null)
            {
                if (item.Color != col)
                    item.HighlightColor = new Color(1f, 0.8f, 0.8f, 1f);
            }

        }
    }


    private void OnClicked(Vector2Int position)
    {
        var isNoFigureSelected = LastSelectedFigure == null;

        if (isNoFigureSelected)
        {
            var clickedFigure = _boardModel.Board[position.x, position.y];

            if (clickedFigure != null)
            {
                var _figureColorWhite = clickedFigure.Color == Checker.Color.White;
                var _figureColorBlack = clickedFigure.Color == Checker.Color.Black;
                var _playerSelector = _switcher ? _figureColorWhite : _figureColorBlack;

                if (_playerSelector)
                {
                    if (clickedFigure != null)
                    {
                        LastSelectedFigure = _boardModel.Board[position.x, position.y];

                        foreach (var item in _boardModel.Board)
                        {
                            if (item != null)
                            {
                                item.HighlightColor = Color.white;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            var resMove = _boardModel.TryMoveFigure(LastSelectedFigure.Position, position);

            if (resMove)
            {
                ChangePlayer();
                if (_boardModel.CheckVictoryWhite() == true)
                {
                    _boardView.startGame = false;
                    Debug.Log("WhiteVictory");
                    _boardView._canvas.enabled = true;
                    _boardView.whiteWin.SetActive(true);
                    
                }
                   

                if (_boardModel.CheckVictoryBlack() == true)
                {
                    _boardView.startGame = false;
                    Debug.Log("BlackVictory");
                    _boardView._canvas.enabled = true;
                    _boardView.blackWin.SetActive(true);
                    
                }
                   

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
                _boardView.ClearAvailableMoves();
            }
            else
            {
                foreach (var move in _boardModel.GetAvailableMoves(value))
                {
                    _boardView.DisplayAvailableMove(move);
                }
                _boardView.DisplayCellSelection(value.Position);
            }
        }
    }

}
