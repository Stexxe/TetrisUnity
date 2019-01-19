using UnityEngine;

public static class ShapeFactory {

    public static Shape CreateRandom()
    {
        var choice = Random.Range(0, 5);

        switch (choice) {
            case 0:
                return CreateSymmetric();
            case 1:
                return CreateStick();
            case 2:
                return CreateSquare();
            case 3:
                return CreateL();
            case 4:
                return CreateS();
            default:
                return CreateSymmetric();
        }
    }

    private static Shape CreateSymmetric() {
        return new Shape(
            new [,] {
            { 1, 0 },
            { 1, 1 },
            { 1, 0 }
        },

            new [,] {
            { 1, 1, 1 },
            { 0, 1, 0 }
        },
            new [,] {
            { 0, 1 },
            { 1, 1 },
            { 0, 1 }
        },
            new [,] {
            { 0, 1, 0 },
            { 1, 1, 1 }
        });
    }

    private static Shape CreateStick() {
        return new Shape(
           new [,] {
            { 1, 1, 1, 1}
       },

           new [,] {
            { 1 },
            { 1 },
            { 1 },
            { 1 }
       });
    }

    private static Shape CreateSquare() {
        return new Shape(
            new [,] {
            { 1, 1 },
            { 1, 1 }
        });
    }

    private static Shape CreateL() {
        return new Shape(
            new [,] {
            { 1, 1 },
            { 1, 0 },
            { 1, 0 }
        },

            new [,] {
            { 1, 1, 1 },
            { 0, 0, 1 }
        },
            new [,] {
            { 0, 1 },
            { 0, 1 },
            { 1, 1 }
        },
            new [,] {
            { 1, 0, 0 },
            { 1, 1, 1 }
        });
    }
    
    private static Shape CreateS()
    {
        return new Shape(
            new[,]
            {
                {0, 1, 1},
                {1, 1, 0}
            },

            new[,]
            {
                {1, 0},
                {1, 1},
                {0, 1}
            });
    }
}
