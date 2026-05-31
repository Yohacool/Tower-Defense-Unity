using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class TowerPlacer : MonoBehaviour
{
    public Tilemap grassTilemap;
    public int towerCost = 25;

    private GameObject selectedTowerPrefab;

    private GameObject previewTower;

    private bool isPlacing = false;

    private HashSet<Vector3Int> occupiedCells =
        new HashSet<Vector3Int>();

    void Update()
    {
        if (Mouse.current == null)
            return;

        if (!isPlacing)
            return;

        MovePreview();

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            PlaceTower();
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            CancelPlacement();
        }
    }

    public void StartPlacingTower(GameObject prefab)
    {
        if (!MoneyManager.Instance.SpendMoney(towerCost))
        {
            Debug.Log("Not enough gold");
            return;
        }

        if (isPlacing)
            CancelPlacement();

        selectedTowerPrefab = prefab;

        previewTower = Instantiate(prefab);

        isPlacing = true;
    }

    void MovePreview()
    {
        Vector3 mouseWorld =
            Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        mouseWorld.z = 0;

        Vector3Int cell =
            grassTilemap.WorldToCell(mouseWorld);

        Vector3 snappedPosition =
            grassTilemap.GetCellCenterWorld(cell);

        previewTower.transform.position = snappedPosition;
    }

    void PlaceTower()
    {
        Vector3 mouseWorld =
            Camera.main.ScreenToWorldPoint(
                Mouse.current.position.ReadValue()
            );

        mouseWorld.z = 0;

        Vector3Int cell =
            grassTilemap.WorldToCell(mouseWorld);

        if (!grassTilemap.HasTile(cell))
            return;

        if (occupiedCells.Contains(cell))
            return;

        Vector3 position =
            grassTilemap.GetCellCenterWorld(cell);

        GameObject placedTower =
            Instantiate(
                selectedTowerPrefab,
                position,
                Quaternion.identity
            );

        TowerAttack towerAttack =
            placedTower.GetComponent<TowerAttack>();

        if (towerAttack != null)
        {
            towerAttack.Activate();
        }

        occupiedCells.Add(cell);

        Destroy(previewTower);

        isPlacing = false;
    }

    void CancelPlacement()
    {
        if (previewTower != null)
            Destroy(previewTower);

        previewTower = null;

        MoneyManager.Instance.AddMoney(towerCost);

        isPlacing = false;
    }
}