using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeController {
    private Shape model;
    private ShapeView view;


    public ShapeController(Shape model, ShapeView view) {
        this.model = model;
        this.view = view;
    }

    public void ReceiveInput() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            DoAction((Shape sh) => sh.Move(new Cell(0, -1)));
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            DoAction((Shape sh) => sh.Move(new Cell(0, 1)));
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            DoAction((Shape sh) => sh.Rotate());
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            DoAction((Shape sh) => sh.Drop());
        }
    }

    public IEnumerator Fall(Action<bool> onFall) {
        if (!model.Falling) {
            model.Falling = true;
            yield return new WaitForSeconds(1 / model.Speed);

            var moved = model.Move(new Cell(1, 0));
            view.PlaceBlocks(model.GetCurrentBlocks());

            onFall(!moved);

            model.Falling = false;
        }
    }

    private void DoAction(Func<Shape, bool> action) {
        var actionPerformed = action(model);

        if (actionPerformed) {
            view.PlaceBlocks(model.GetCurrentBlocks());
        }
    }

}
