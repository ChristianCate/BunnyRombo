using UnityEngine;

public class GridLines : MonoBehaviour
{
    public int columns = 5;
    public int rows = 5;
    public float cellSize = 1f;

    public float lineWidth = 0.03f;
    public Color lineColor = Color.white;

    private void Start()
    {
        CreateGridLines();

        if (GameManager.Instance != null &&
    !GameManager.Instance.GameStarted)
        {
            SetGridVisible(false);
        }
    }

    private void CreateGridLines()
    {
        for (int x = 0; x <= columns; x++)
        {
            Vector3 start = transform.position +
                new Vector3(x * cellSize, 0f, 0f);

            Vector3 end = transform.position +
                new Vector3(x * cellSize, rows * cellSize, 0f);

            CreateLine(start, end);
        }

        for (int y = 0; y <= rows; y++)
        {
            Vector3 start = transform.position +
                new Vector3(0f, y * cellSize, 0f);

            Vector3 end = transform.position +
                new Vector3(columns * cellSize, y * cellSize, 0f);

            CreateLine(start, end);
        }
    }

    private void CreateLine(Vector3 start, Vector3 end)
    {
        GameObject lineObject = new GameObject("GridLine");
        lineObject.transform.SetParent(transform);

        LineRenderer line = lineObject.AddComponent<LineRenderer>();

        line.useWorldSpace = true;
        line.positionCount = 2;

        line.SetPosition(0, start);
        line.SetPosition(1, end);

        line.startWidth = lineWidth;
        line.endWidth = lineWidth;

        line.startColor = lineColor;
        line.endColor = lineColor;

        line.material = new Material(
            Shader.Find("Sprites/Default")
        );

        line.sortingOrder = 10;
    }

    public void SetGridVisible(bool visible)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(visible);
        }
    }
}