using UnityEngine;

public class FigurePresenter
{
    private FigureModel _figureModel;
    private FigureView _figureView;

    public FigurePresenter(FigureModel figureModel, FigureView figureView)
    {
        _figureModel = figureModel;
        _figureView = figureView;

        _figureModel.Moved += OnMoved;
        _figureModel.ChangedHighlightColor += OnChangedHighlightColor;
    }

    private void OnChangedHighlightColor(Color color)
    {
        _figureView.ChangeHighlightColor(color);
    }

    private void OnMoved(Vector2Int position)
    {
        _figureView.Move(position);
    }
}
