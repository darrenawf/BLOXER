using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MoveBlock : MonoBehaviour
{
    private GameObject[] obstacles, redBlocks;

    void Start()
    {
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        redBlocks = GameObject.FindGameObjectsWithTag("redBlock");
    }

    public bool Move(Vector2 direction)
    {
        Vector2 targetPosition = (Vector2)transform.position + direction;
        if (IsBlocked(targetPosition)) return false;

        transform.Translate(direction);
        return true;
    }

    private bool IsBlocked(Vector2 targetPosition)
    {
        foreach (var obj in obstacles)
            if (IsPositionMatch(obj, targetPosition)) return true;

        foreach (var obj in redBlocks)
            if (IsPositionMatch(obj, targetPosition)) return true;

        return false;
    }

    private bool IsPositionMatch(GameObject obj, Vector2 targetPosition) =>
        obj != null && Vector2.Distance((Vector2)obj.transform.position, targetPosition) < 0.01f;
}