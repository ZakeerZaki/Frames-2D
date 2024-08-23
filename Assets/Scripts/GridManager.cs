using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject boxPrefab;
    public GameObject platform;
    public int gridWidth = 3;
    public int gridHeight = 3;
    public Vector2 gridStartPosition = new Vector2(0, 0);
    public Vector2 spacing = new Vector2(1.0f, 1.0f);

    public Color color1;
    public Color color2;
    public Color color3;
    public Color color4;

    private GameObject[,] grid;
    private Color selectedColor = Color.clear;
    private Sprite selectedShape;

    public ShapeManager shapeManager;

    private Ray ray;
    private RaycastHit hit;

    void Start()
    {
        grid = new GameObject[gridWidth, gridHeight];
        CreateGrid();
        AssignColorsAndShapes();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider != null)
                {
                    Box box = hit.collider.GetComponent<Box>();
                    if (box != null && !box.isPreassigned)
                    {
                        // Set the color of the main sprite
                        if (selectedColor != Color.clear)
                        {
                            box.SetColor(selectedColor);
                            selectedColor = Color.clear; // Reset the selected color after assigning it to the box
                        }

                        // Instantiate the shape if selected
                        if (selectedShape != null)
                        {
                            box.InstantiateShape(selectedShape);
                            selectedShape = null; // Reset the selected shape after assigning it to the box
                        }
                    }
                }
            }
        }
    }

    public void SetSelectedColor(Color color)
    {
        selectedColor = color;
    }

    public void SetSelectedShape(Sprite shape)
    {
        selectedShape = shape;
    }

    void CreateGrid()
    {
        Vector3 platformPosition = platform.transform.position;

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 boxPosition = new Vector3(gridStartPosition.x + (x * spacing.x), gridStartPosition.y + (y * spacing.y), 0) + platformPosition;
                GameObject newBox = Instantiate(boxPrefab, boxPosition, Quaternion.identity);
                newBox.transform.parent = transform;

                newBox.GetComponent<Box>().Initialize(this, x, y, shapeManager);
                grid[x, y] = newBox;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawRay(ray.origin, ray.origin + ray.direction * 1000f);
    }

    void AssignColorsAndShapes()
    {
        // Color assignment based on 1D index mapping
        int[] boxIndices = { 8, 4, 7, 0 };
        Color[] colors = { color1, color2, color3, color4 };

        for (int i = 0; i < boxIndices.Length; i++)
        {
            int index = boxIndices[i];
            int x = index % gridWidth;
            int y = index / gridWidth;

            if (x < gridWidth && y < gridHeight)
            {
                Box box = grid[x, y].GetComponent<Box>();
                if (box != null)
                {
                    box.SetColor(colors[i]);
                    box.isPreassigned = true;
                }
            }
        }

        // Assign shapes to preassigned boxes
        AssignShapesToPreassignedBoxes();
    }

    void AssignShapesToPreassignedBoxes()
    {
        // Teal and cyan boxes (heart shapes)
        AssignShapeToColor(color1, shapeManager.shape1); // Heart shape
        AssignShapeToColor(color2, shapeManager.shape2); // Heart shape

        // Green box (star shape)
        AssignShapeToColor(color3, shapeManager.shape1); // Star shape

        // Yellow box (circle shape)
        AssignShapeToColor(color4, shapeManager.shape3); // Circle shape

        // Box above the yellow box (square shape)
        int yellowBoxIndex = 0; // Assuming the yellow box is at index 0
        int x = yellowBoxIndex % gridWidth;
        int y = yellowBoxIndex / gridWidth;

        if (y + 1 < gridHeight)
        {
            Box boxAboveYellow = grid[x, y + 1].GetComponent<Box>();
            if (boxAboveYellow != null)
            {
                boxAboveYellow.InstantiateShape(shapeManager.shape4); // Square shape
                boxAboveYellow.isShapeLocked = true; // Lock only the shape
            }
        }
    }

    public Box GetBox(int x, int y)
    {
        if (x < 0 || x >= gridWidth || y < 0 || y >= gridHeight)
        {
            return null;
        }
        return grid[x, y].GetComponent<Box>();
    }


    void AssignShapeToColor(Color color, Sprite shape)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Box box = grid[x, y].GetComponent<Box>();
                if (box != null && box.GetColor() == color)
                {
                    box.InstantiateShape(shape);
                    box.isPreassigned = true;
                }
            }
        }
    }
}
