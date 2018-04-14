using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeFactory {

    public static Shape CreateRandom() {
        int choice = Random.Range(0, 4);

        switch (choice) {
            case 0:
                return CreateSymmetric();
            case 1:
                return CreateStick();
            case 2:
                return CreateSquare();
            case 3:
                return CreateL();
        }

        return CreateSymmetric();
    }

    private static Shape CreateSymmetric() {
        return new Shape(
            new int[,] {
            { 1, 0 },
            { 1, 1 },
            { 1, 0 },
        },

            new int[,] {
            { 1, 1, 1 },
            { 0, 1, 0 },
        },
            new int[,] {
            { 0, 1 },
            { 1, 1 },
            { 0, 1 },
        },
            new int[,] {
            { 0, 1, 0 },
            { 1, 1, 1 },
        });
    }

    private static Shape CreateStick() {
        return new Shape(
           new int[,] {
            { 1, 1, 1, 1},
       },

           new int[,] {
            { 1 },
            { 1 },
            { 1 },
            { 1 },
       });
    }

    private static Shape CreateSquare() {
        return new Shape(
            new int[,] {
            { 1, 1 },
            { 1, 1 },
        });
    }

    private static Shape CreateL() {
        return new Shape(
            new int[,] {
            { 1, 1 },
            { 1, 0 },
            { 1, 0 },
        },

            new int[,] {
            { 1, 1, 1 },
            { 0, 0, 1 },
        },
            new int[,] {
            { 0, 1 },
            { 0, 1 },
            { 1, 1 },
        },
            new int[,] {
            { 1, 0, 0 },
            { 1, 1, 1 },
        });
    }
}
