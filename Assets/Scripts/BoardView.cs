using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;


public class BoardView : MonoBehaviour
{
    [SerializeField] private GameObject _whiteFigure;
    [SerializeField] private GameObject _blackFigure;
    [SerializeField] private GameObject _whiteSquare;
    [SerializeField] private GameObject _blackSquare;
    [SerializeField] private GameObject _highlightSquare;

    private GameObject[,] _figures = new GameObject[8, 8];

    [HideInInspector] public int PosX;
    [HideInInspector] public int PosY;

    public event Action MouseButtonDown;

 




    public void GenerateBoard(FigureModel[,] array)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (array[i, j] != null)
                {
                    if (array[i, j].Color == Color.white)
                    {
                        _figures[i, j] = Instantiate(_whiteFigure, new Vector3(j, i, 0f), Quaternion.identity);
                    }
                    else if (array[i, j].Color == Color.black)
                    {
                        _figures[i, j] = Instantiate(_blackFigure, new Vector3(j, i, 0f), Quaternion.identity);
                    }
                }
            }
        }
    }
    public void DrawTiles(FigureModel[,] array)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
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

        Instantiate(_blackSquare, new Vector3(3.5f, 3.5f, 0f), Quaternion.identity);

    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 CursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            PosX = (int)Mathf.Round(CursorPosition.x);
            PosY = (int)Mathf.Round(CursorPosition.y);


            if (PosX >= 0 && PosX < 8 && PosY >= 0 && PosY < 8)
            {
                MouseButtonDown?.Invoke();
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
