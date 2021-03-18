using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;


public class BoardView : MonoBehaviour
{
    [SerializeField] private GameObject _highlightSquare;
    [SerializeField] private GameObject _whiteSquare;
    [SerializeField] private GameObject _backgorund;

    private GameObject _selection;

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
    }

    public void HighlightCell(Vector2Int position)
    {
        _selection.SetActive(true);
        _selection.transform.position = new Vector3(position.x, position.y, 0f);
    }

    public void HideCellSelection()
    {
        _selection.SetActive(false);
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
