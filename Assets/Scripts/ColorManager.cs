using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static ColorManager Instance { get; private set; }

    public Color selectedColor = Color.white;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetColorRed()
    {
        selectedColor = Color.red;
    }

    public void SetColorGreen()
    {
        selectedColor = Color.green;
    }

    public void SetColorBlue()
    {
        selectedColor = Color.blue;
    }

    public void SetColorYellow()
    {
        selectedColor = Color.yellow;
    }
}
