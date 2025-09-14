using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int collumns = 0;
    public int rows = 0;
    public float squaresGap = 0.1f;
    public GameObject gridSquare;
    public Vector2 startPostion = new Vector2(0.0f, 0.0f);
    public float squareScale = 0.5f;
    public float everySquareOffset = 0.0f;

    private Vector2 _offset = new Vector2(0.0f, 0.0f);
    private List<GameObject> _gridSquare = new List<GameObject>();


    void Start()
    {
        CreateGrid();
    }
    private void CreateGrid()
    {
        SpawnGridSpares();
        SetGridSquaresPositions();
    }
    private void SpawnGridSpares()
    {
        int square_index = 0;
        for (var row = 0; row < rows; ++row)
        {
            for (var collumn = 0; collumn < collumns; ++collumn)
            {
                _gridSquare.Add(Instantiate(gridSquare) as GameObject);
                _gridSquare[_gridSquare.Count - 1].transform.SetParent(this.transform);
                _gridSquare[_gridSquare.Count - 1].transform.localScale = new Vector3(squareScale, squareScale, squareScale);
                _gridSquare[_gridSquare.Count - 1].GetComponent<GridSquare>().SetImage(square_index % 2 == 0);
                square_index++;
            }
        }
    }
    private void SetGridSquaresPositions()
    {
        int collumn_number = 0;
        int row_number = 0;
        Vector2 square_gap_number = new Vector2(0.0f, 0.0f);
        bool row_move = false;
        var square_rect = _gridSquare[0].GetComponent<RectTransform>();

        _offset.x = square_rect.rect.width * square_rect.transform.localScale.x + everySquareOffset;
        _offset.y = square_rect.rect.height * square_rect.transform.localScale.y + everySquareOffset;
        foreach (GameObject square in _gridSquare)
        {
            if (collumn_number + 1 > collumns)
            {
                square_gap_number.x = 0;
                collumn_number = 0;
                row_number++;
                row_move = false;
            }
            var pos_x_offset = _offset.x * collumn_number + (square_gap_number.x * squaresGap);
            var pos_y_offset = _offset.y * row_number + (square_gap_number.y * squaresGap);

            if (collumn_number > 0 && collumn_number % 3 == 0)
            {
                square_gap_number.x++;
                pos_x_offset += squaresGap;
            }
            if (row_number > 0 && row_number % 3 == 0 & row_move == false)
            {
                row_move = true;
                square_gap_number.y++;
                pos_y_offset += squaresGap;
            }
            square.GetComponent<RectTransform>().anchoredPosition = new Vector2(startPostion.x + pos_x_offset,
            startPostion.y - pos_y_offset);
            square.GetComponent<RectTransform>().localPosition = new Vector3(startPostion.x + pos_x_offset,
            startPostion.y - pos_y_offset, 0.0f);
            collumn_number++;
        }
    }
}
