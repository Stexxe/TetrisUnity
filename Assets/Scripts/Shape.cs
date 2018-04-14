using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shape {
    private List<int[,]> molds;
    private int moldIndex = 0;

    public bool Falling = false;
    public float Speed = 1;

    public Cell Pos { get; set; }

    public Predicate<Cell> MovementRestrict;

    public Shape(params int[][,] molds) {
        this.molds = new List<int[,]>();

        foreach (var m in molds) {
            this.molds.Add(m);
        }
    }

    public bool Move(Cell delta) {
        var targetPos = Pos + delta;
        var targetCells = GetAffectedBlocks(targetPos);
        var canMove = targetCells.TrueForAll(MovementRestrict);

        if (canMove) {
            Pos = targetPos;
        }

        return canMove;
    }

    public bool Rotate() {
        var targetCells = GetAffectedBlocks(GetNextMoldIndex());
        var canRotate = targetCells.TrueForAll(MovementRestrict);

        if (canRotate) {
            SwitchNextMold();
        }

        return canRotate;
    }

    public bool Drop() {
        Pos = GetMostDownPos();
        return true;
    }

    private Cell GetMostDownPos() {
        int row = Pos.Row;
        int column = Pos.Column;

        while (true) {
            var currentCells = GetAffectedBlocks(new Cell(row, column));
            var nextCells = GetAffectedBlocks(new Cell(row + 1, column));

            if (!nextCells.TrueForAll(MovementRestrict) || !currentCells.TrueForAll(MovementRestrict)) {
                return new Cell(row, column);
            }

            row += 1;
        }
    }


    public List<Cell> GetCurrentBlocks() {
        return GetAffectedBlocks(Pos, moldIndex);
    }

    private List<Cell> GetAffectedBlocks(int moldIndex) {
        return GetAffectedBlocks(null, moldIndex);
    }

    private List<Cell> GetAffectedBlocks(Cell pos) {
        return GetAffectedBlocks(pos, -1);
    }

    // Returns action based on specified state as parameters
    private List<Cell> GetAffectedBlocks(Cell pos, int moldIndex) {
        var actualPos = pos ?? Pos;
        var actualIndex = moldIndex != -1 ? moldIndex : this.moldIndex;

        var mold = molds[actualIndex];
        var result = new List<Cell>();

        for (int row = 0; row < mold.GetLength(0); row += 1) {
            for (int column = 0; column < mold.GetLength(1); column += 1) {
                var hasBlock = (mold[row, column] == 1);

                if (hasBlock) {
                    var block = new Cell(actualPos.Row + row, actualPos.Column + column);
                    result.Add(block);
                }
            }
        }

        return result;
    }

    public void SwitchNextMold() {
        moldIndex = GetNextMoldIndex();
    }

    private int GetNextMoldIndex() {
        return moldIndex + 1 < molds.Count ? moldIndex + 1 : 0;
    }

    private int GetPreviousMoldIndex() {
        return moldIndex - 1 >= 0 ? moldIndex - 1 : molds.Count - 1;
    }
}
