using UnityEngine;
using System;


public class BoardView : MonoBehaviour
{
    [SerializeField] private GameObject _selectionSquare;
    [SerializeField] private GameObject _highlightSquare;
    [SerializeField] private GameObject _whiteSquare;
    [SerializeField] private GameObject _backgorund;
    [SerializeField] private Transform _highlightsRoot;
    [SerializeField] private Canvas _canvas;

   private GameObject _selection;
    private int _size;

    public event Action<Vector2Int> Clicked;
    public event Action DiagonalRulesSet;
    public event Action HorisontalAndVerticalRoolSet;
    public event Action NoWideMoveRoolSet;
    public event Action StartGame;


    public void Initialize(int size)
    {
        _size = size;

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (i % 2 == 0)
                {
                    if (j % 2 != 0)
                    {
                        Instantiate(_whiteSquare, new Vector3(i, j, 0f), Quaternion.identity);
                    }
                }
                if (i % 2 != 0)
                {
                    if (j % 2 == 0)
                    {
                        Instantiate(_whiteSquare, new Vector3(i, j, 0f), Quaternion.identity);
                    }
                }
            }
        }

        Instantiate(_backgorund, new Vector3(3.5f, 3.5f, 0f), Quaternion.identity);

        _selection = Instantiate(_selectionSquare);
        _selection.SetActive(false);
    }

    public void SetDiagonalRules()
    {
        DiagonalRulesSet?.Invoke();
        StartGame?.Invoke();
        _canvas.enabled = false;
    }
    public void SetHorisontalAndVerticalRool()
    {
        HorisontalAndVerticalRoolSet?.Invoke();
        StartGame?.Invoke();
        _canvas.enabled = false;
    }
    public void SetNoWideMoveRool()
    {
        NoWideMoveRoolSet?.Invoke();
        StartGame?.Invoke();
        _canvas.enabled = false;
    }

    public void DisplayCellSelection(Vector2Int position)
    {
        _selection.SetActive(true);
        _selection.transform.position = new Vector3(position.x, position.y, 0f);
    }

    public void HideCellSelection()
    {
        _selection.SetActive(false);
    }

    public void DisplayAvailableMove(Vector2Int position)
    {
        Instantiate(_highlightSquare, _highlightsRoot).transform.position = new Vector3(position.x, position.y, 0f);
    }

    public void ClearAvailableMoves()
    {
        foreach (Transform child in _highlightsRoot)
        {
            Destroy(child.gameObject);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var roundedPosition = new Vector2Int(Mathf.RoundToInt(cursorPosition.x), Mathf.RoundToInt(cursorPosition.y));

            if (roundedPosition.x >= 0 && roundedPosition.x < _size &&
                roundedPosition.y >= 0 && roundedPosition.y < _size)
            {
                Clicked?.Invoke(roundedPosition);
            }
        }
    }
}
