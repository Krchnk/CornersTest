using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;


public class BoardView : MonoBehaviour
{

    [SerializeField] private GameObject _highlightSquare;
    [SerializeField] private GameObject _whiteSquare;
    [SerializeField] private GameObject _backgorund;

    private GameObject[,] _figures = new GameObject[8, 8];

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
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            var cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var roundedPosition = new Vector2Int((int)cursorPosition.x, (int) cursorPosition.y);

            if (roundedPosition.x >= 0 && roundedPosition.x < 8 &&
                roundedPosition.y >= 0 && roundedPosition.y < 8)
            {
                Clicked?.Invoke(roundedPosition);
            }
        }
    }
    public void HighlightTile(int posX, int posY)
    {
        Instantiate(_highlightSquare, new Vector3(posX, posY, 0f), Quaternion.identity);
    }
    public void MoveFigure(int targetPosX, int targetPosY, int currentPosX, int currentPosY)
    {
        var selectedFigure = _figures[currentPosY, currentPosX];

        // selectedFigure.transform.position = new Vector3(targetPosX, targetPosY, 0f);


        StartCoroutine(MoveTo(selectedFigure, selectedFigure.transform.position, new Vector3(targetPosX, targetPosY, 0f)));

        StartCoroutine(ScaleTo(selectedFigure, selectedFigure.transform.localScale, new Vector3(0.1f, 0.1f, 0.1f)));
        


        _figures[targetPosY, targetPosX] = _figures[currentPosY, currentPosX];
    }

    private IEnumerator MoveTo(GameObject gameObject, Vector3 from, Vector3 to)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            gameObject.transform.position = Vector3.Lerp(from, to, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }
    private IEnumerator ScaleTo(GameObject gameObject, Vector3 from, Vector3 to)
    {
        float r = 0f;
        if (r <= 1f)
        {
            r += Time.deltaTime;
            gameObject.transform.localScale = Vector3.Lerp(from, to, r);
            yield return null;
        }
       
        
    }




}
