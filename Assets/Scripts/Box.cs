using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private GridManager gridManager;
    private ShapeManager shapeManager;
    private int x;
    private int y;
    private bool isHighlighted = false;
    private SpriteRenderer spriteRenderer;
    private Transform shapeTransform;
    private SpriteRenderer shapeRenderer;

    public bool isPreassigned = false;
    public bool isShapeLocked = false; // New property to lock shape
    public Color highlightColor = Color.yellow; // Color to use for highlighting
    private Color originalColor;

    public void Initialize(GridManager manager, int gridX, int gridY, ShapeManager shapeMgr)
    {
        gridManager = manager;
        x = gridX;
        y = gridY;
        shapeManager = shapeMgr;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        shapeTransform = transform.Find("shape");

        if (shapeTransform != null)
        {
            shapeRenderer = shapeTransform.GetComponent<SpriteRenderer>();
            if (shapeRenderer == null)
            {
                shapeRenderer = shapeTransform.gameObject.AddComponent<SpriteRenderer>();
            }
        }
        else
        {
            Debug.LogError("Shape transform not found. Make sure the Box prefab has a child named 'shape'.");
        }
    }


    public Sprite GetShape()
    {
        return shapeRenderer.sprite;
    }


    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
        originalColor = color;
    }

    public Color GetColor()
    {
        return spriteRenderer.color;
    }

    public void ToggleHighlight()
    {
        if (isHighlighted)
        {
            spriteRenderer.color = originalColor; // Revert to original color
        }
        else
        {
            spriteRenderer.color = highlightColor; // Highlight color
        }
        isHighlighted = !isHighlighted;
    }

    public void InstantiateShape(Sprite shape)
    {
        if (shapeRenderer != null && !isShapeLocked)
        {
            shapeRenderer.sprite = shape;
        }
    }
}
