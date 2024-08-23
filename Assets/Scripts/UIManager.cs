using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GridManager gridManager;
    public ShapeManager shapeManager;
    public SolutionChecker solutionChecker;

    private Dictionary<string, string> colorHexCodes = new Dictionary<string, string>
    {
        { "TealButton", "#045D5D" },
        { "GreenButton", "#00FF00" },
        { "CyanButton", "#26DBCB" },
        { "YellowButton", "#FFFF00" }
    };

    public void OnShapeButtonClicked(int shapeIndex)
    {
        shapeManager.SelectShape(shapeIndex);
        gridManager.SetSelectedShape(shapeManager.GetSelectedShape());
    }

    public void OnColorButtonClick(Button button)
    {
        string buttonName = button.gameObject.name;
        if (colorHexCodes.ContainsKey(buttonName))
        {
            string hexCode = colorHexCodes[buttonName];
            Color buttonColor;
            if (ColorUtility.TryParseHtmlString(hexCode, out buttonColor))
            {
                gridManager.SetSelectedColor(buttonColor);
            }
        }
    }

    public void OnSubmitButtonClick()
    {
        solutionChecker.CheckSolution();
    }
}
