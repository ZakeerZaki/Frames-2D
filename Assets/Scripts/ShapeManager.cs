using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeManager : MonoBehaviour
{
    public Sprite shape1; // Heart shape
    public Sprite shape2; // Star shape
    public Sprite shape3; // Circle shape
    public Sprite shape4; // Square shape

    private Sprite selectedShape;

    public void SelectShape(int shapeIndex)
    {
        switch (shapeIndex)
        {
            case 1:
                selectedShape = shape1;
                break;
            case 2:
                selectedShape = shape2;
                break;
            case 3:
                selectedShape = shape3;
                break;
            case 4:
                selectedShape = shape4;
                break;
            default:
                selectedShape = null;
                break;
        }
    }

    public Sprite GetSelectedShape()
    {
        return selectedShape;
    }
}
