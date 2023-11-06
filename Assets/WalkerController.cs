using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class WalkerController : MonoBehaviour
{
    [SerializeField] private Grid grid; // Your grid reference
    [SerializeField] private Tilemap[] tilemaps; // Array of your Tilemaps
    private List<Vector3Int> validDirections = new List<Vector3Int>();
    private float moveSpeed = 1f; // Adjust the speed as needed
    private Vector3 targetPosition;

    private void Start()
    {
        SnapToCenter();
        SetNewTargetPosition();
    }

    private void SnapToCenter()
    {
        Vector3Int currentCell = grid.WorldToCell(transform.position);
        Vector3 center = grid.GetCellCenterWorld(currentCell);
        transform.position = center;
    }

    private void FindValidDirections()
    {
        validDirections.Clear();
        Vector3Int currentCell = grid.WorldToCell(transform.position);

        foreach (Vector3Int direction in new List<Vector3Int> { Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right })
        {
            if (!IsTileAtCell(currentCell + direction))
            {
                validDirections.Add(direction);
            }
        }
    }

    private bool IsTileAtCell(Vector3Int cell)
    {
        foreach (Tilemap tilemap in tilemaps)
        {
            if (tilemap.HasTile(cell))
            {
                return true;
            }
        }
        return false;
    }

    private void SetNewTargetPosition()
    {
        FindValidDirections();
        if (validDirections.Count > 0)
        {
            Vector3Int randomDirection = validDirections[Random.Range(0, validDirections.Count)];
            Vector3Int currentCell = grid.WorldToCell(transform.position);
            targetPosition = grid.GetCellCenterWorld(currentCell + randomDirection);
        }
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            SetNewTargetPosition();
        }
    }
}