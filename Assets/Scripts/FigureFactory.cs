using System;
using UnityEngine;

public class FigureFactory
{
    private GameObject _whiteFigure;
    private GameObject _blackFigure;

    public FigureFactory(GameObject whiteFigure, GameObject blackFigure)
    {
        _whiteFigure = whiteFigure;
        _blackFigure = blackFigure;
    }

    public FigureModel Create(Vector2Int position, Checker.Color color)
    {
        var figure = color == Checker.Color.White ? _whiteFigure : _blackFigure;
        var figureView = UnityEngine.Object.Instantiate(figure, new Vector3(position.x, position.y, 0f), Quaternion.identity).GetComponent<FigureView>();
        var figureModel = new FigureModel(color, position);
        var figurePresenter = new FigurePresenter(figureModel, figureView);
        return figureModel;
    }
}
