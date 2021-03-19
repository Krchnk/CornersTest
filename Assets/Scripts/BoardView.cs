using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;


public class BoardView : MonoBehaviour
{
    [SerializeField] private GameObject _highlightSquare;
    [SerializeField] private GameObject _moveOptionHighlightSquare;
    [SerializeField] private GameObject _whiteSquare;
    [SerializeField] private GameObject _backgorund;

    private GameObject _selection;
    private GameObject _upMoveOption;
    private GameObject _downMoveOption;
    private GameObject _leftMoveOption;
    private GameObject _rightMoveOption;

    public event Action<Vector2Int> Clicked;

    public void Initialize(int size)
    {
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

        _selection = Instantiate(_highlightSquare);
        _selection.SetActive(false);
        _upMoveOption = Instantiate(_moveOptionHighlightSquare);
        _upMoveOption.SetActive(false);
        _downMoveOption = Instantiate(_moveOptionHighlightSquare);
        _downMoveOption.SetActive(false);
        _leftMoveOption = Instantiate(_moveOptionHighlightSquare);
        _leftMoveOption.SetActive(false);
        _rightMoveOption = Instantiate(_moveOptionHighlightSquare);
        _rightMoveOption.SetActive(false);
        
    }

    public void HighlightCell(Vector2Int position)
    {
        _selection.SetActive(true);
        _selection.transform.position = new Vector3(position.x, position.y, 0f);
    }

    public void HighlightUpMoveCell(Vector2Int position)
    {
        _upMoveOption.SetActive(true);
        _upMoveOption.transform.position = new Vector3(position.x, position.y, 0f);
    }

    public void HighlightDownMoveCell(Vector2Int position)
    {
        _downMoveOption.SetActive(true);
        _downMoveOption.transform.position = new Vector3(position.x, position.y, 0f);
    }

    public void HighlightLeftMoveCell(Vector2Int position)
    {
        _leftMoveOption.SetActive(true);
        _leftMoveOption.transform.position = new Vector3(position.x, position.y, 0f);
    }

    public void HighlightRightMoveCell(Vector2Int position)
    {
        _rightMoveOption.SetActive(true);
        _rightMoveOption.transform.position = new Vector3(position.x, position.y, 0f);
    }

    public void HideCellSelection()
    {
        _selection.SetActive(false);
        _upMoveOption.SetActive(false);
        _downMoveOption.SetActive(false);
        _leftMoveOption.SetActive(false);
        _rightMoveOption.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var roundedPosition = new Vector2Int(Mathf.RoundToInt(cursorPosition.x), Mathf.RoundToInt(cursorPosition.y));

            if (roundedPosition.x >= 0 && roundedPosition.x < 8 &&
                roundedPosition.y >= 0 && roundedPosition.y < 8)
            {
                Clicked?.Invoke(roundedPosition);
            }
        }
    }
}
