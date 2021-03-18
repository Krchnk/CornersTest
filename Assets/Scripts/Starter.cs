using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{

    [SerializeField] BoardView _boardView;

    void Start()
    {
        var boardModel = new BoardModel();

        var boardPresenter = new BoardPresenter(boardModel, _boardView);

        boardModel.BoardGeneration();
        



    }

    
}
