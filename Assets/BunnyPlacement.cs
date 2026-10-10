using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//Used AI assistance heavily for this script
public class BunnyPlacement : MonoBehaviour
{
    public Transform gridOrigin;
    public GameObject bunnyPrefab;

    public float cellSize = 1f;
    public int columns = 5;
    public int rows = 5;
    public int towerCost = 50;

    private GameObject selectedBunnyPrefab;
    private int selectedBunnyCost;

    private GameObject previewBunny;

    private HashSet<Vector3Int> occupiedCells = new HashSet<Vector3Int>();

    private void Update()
    {
        if (GameManager.Instance == null || !GameManager.Instance.GameStarted || !GameManager.Instance.IsBuildPhase)
        {
            HidePreview();
            return;
        }

        UpdatePreviewPosition();

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            TryPlaceBunny();
        }
    }

    public void SelectBunny(GameObject prefab, int cost)
    {
        if (prefab == null)
        {
            return;
        }

        selectedBunnyPrefab = prefab;
        selectedBunnyCost = cost;

        if (previewBunny != null)
        {
            Destroy(previewBunny);
        }

        previewBunny = Instantiate(selectedBunnyPrefab);

        Bunny bunnyScript = previewBunny.GetComponent<Bunny>();

        if (bunnyScript != null)
        {
            bunnyScript.enabled = false;
        }

        Collider2D[] colliders = previewBunny.GetComponentsInChildren<Collider2D>();

        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
    }

    private void UpdatePreviewPosition()
    {
        if (previewBunny == null)
        {
            return;
        }

        Vector3Int cell;
        Vector3 bunnyPosition;

        if (!TryGetMouseCell(out cell, out bunnyPosition))
        {
            previewBunny.SetActive(false);
            return;
        }

        previewBunny.SetActive(true);
        previewBunny.transform.position = bunnyPosition;
    }

    private void TryPlaceBunny()
    {
        if (selectedBunnyPrefab == null)
        {
            return;
        }

        Vector3Int clickedCell;
        Vector3 bunnyPosition;

        if (!TryGetMouseCell(
                out clickedCell,
                out bunnyPosition))
        {
            return;
        }

        if (occupiedCells.Contains(clickedCell))
        {
            Debug.Log("A bunny is already in this cell.");
            return;
        }

        if (!GameManager.Instance.SpendMoney(selectedBunnyCost))
        {
            return;
        }

        Instantiate(selectedBunnyPrefab, bunnyPosition, selectedBunnyPrefab.transform.rotation);

        occupiedCells.Add(clickedCell);

        Destroy(previewBunny);
        previewBunny = null;

        selectedBunnyPrefab = null;
        selectedBunnyCost = 0;
    }

    private bool TryGetMouseCell(
        out Vector3Int cell,
        out Vector3 worldPosition)
    {
        cell = Vector3Int.zero;
        worldPosition = Vector3.zero;

        if (Camera.main == null || Mouse.current == null || gridOrigin == null)
        {
            return false;
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        mousePosition.z = 0f;

        cell = new Vector3Int(Mathf.FloorToInt((mousePosition.x - gridOrigin.position.x)/ cellSize), Mathf.FloorToInt((mousePosition.y - gridOrigin.position.y) / cellSize), 0);

        if (!IsInsideGrid(cell))
        {
            return false;
        }

        worldPosition = gridOrigin.position + new Vector3((cell.x + 0.5f) * cellSize, (cell.y + 0.5f) * cellSize, 0f);

        return true;
    }

    private void HidePreview()
    {
        if (previewBunny != null)
        {
            previewBunny.SetActive(false);
        }
    }

    private bool IsInsideGrid(Vector3Int cell)
    {
        return cell.x >= 0 && cell.x < columns && cell.y >= 0 && cell.y < rows;
    }
}