using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardView : View {
    public int Rows;
    public int Columns;
    public Vector2 Position;

    public GameObject Block;
    public GameObject Grid;

    private List<GameObject> staticBlocks = new List<GameObject>();

    void Start()
    {
        // Grid
        for (int row = 0; row < Rows; row += 1)
        {
            for (int column = 0; column < Columns; column += 1)
            {
                CreateBlock(new Cell(row, column), Grid);
            }
        }
    }

    public void PutBlocks(List<Cell> blocks) {
        staticBlocks.AddRange(CreateBlocks(blocks, Block));
    }

    public void EraseBlocks() {
        staticBlocks.ForEach(Destroy);
        staticBlocks.Clear();
    }
	
}
