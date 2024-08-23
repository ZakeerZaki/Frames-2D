using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolutionChecker : MonoBehaviour
{
    public GridManager gridManager;

    // Predefined solution: positions (x, y), colors, and shapes
    private struct BoxSolution
    {
        public int x, y;
        public Color color;
        public Sprite shape;

        public BoxSolution(int x, int y, Color color, Sprite shape)
        {
            this.x = x;
            this.y = y;
            this.color = color;
            this.shape = shape;
        }
    }

    private List<BoxSolution> solution = new List<BoxSolution>();

    void Start()
    {
        ShapeManager shapeManager = gridManager.shapeManager;

        solution.Add(new BoxSolution(2, 0, gridManager.color4, shapeManager.shape1)); // Yellow box with Heart shape
        solution.Add(new BoxSolution(0, 1, gridManager.color1, shapeManager.shape4)); // Teal box above yellow with Square shape
        solution.Add(new BoxSolution(0, 2, gridManager.color3, shapeManager.shape2)); // Cyan box with Star shape
        solution.Add(new BoxSolution(2, 1, gridManager.color2, shapeManager.shape4)); // Green box with square shape
        solution.Add(new BoxSolution(1, 0, gridManager.color1, shapeManager.shape3)); // Teal box with circle shape
    }

    public void CheckSolution()
    {
        foreach (var boxSolution in solution)
        {
            Box box = gridManager.GetBox(boxSolution.x, boxSolution.y);

            if (box.GetColor() != boxSolution.color || box.GetShape() != boxSolution.shape)
            {
                Debug.Log("Solution is incorrect");
                return;
            }
        }

        Debug.Log("Solution is correct!");
    }
}
