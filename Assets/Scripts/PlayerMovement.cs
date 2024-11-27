using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private GameObject[] obstacles, redBlocks;
    private bool readyToMove;

    void Start()
    {
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        redBlocks = GameObject.FindGameObjectsWithTag("redBlock");
    }

    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput.Normalize();

        if (moveInput.sqrMagnitude > 0.5f)
        {
            if (readyToMove)
            {
                readyToMove = false;
                Move(moveInput);
            }
        }
        else
        {
            readyToMove = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetLevel();
        }
    }

    public bool Move(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) < 0.5f)
        {
            direction.x = 0;
        }
        else
        {
            direction.y = 0;
        }
        direction.Normalize();

        if (IsBlocked(transform.position, direction))
        {
            return false;
        }

        transform.Translate(direction);
        return true;
    }

    public bool IsBlocked(Vector3 position, Vector2 direction)
    {
        Vector2 targetPosition = new Vector2(position.x, position.y) + direction;

        if (obstacles != null)
        {
            foreach (var obstacle in obstacles)
            {
                if (obstacle != null &&
                    Mathf.Approximately(obstacle.transform.position.x, targetPosition.x) &&
                    Mathf.Approximately(obstacle.transform.position.y, targetPosition.y))
                {
                    return true;
                }
            }
        }

        if (redBlocks != null)
        {
            foreach (var redBlock in redBlocks)
            {
                if (redBlock != null &&
                    Mathf.Approximately(redBlock.transform.position.x, targetPosition.x) &&
                    Mathf.Approximately(redBlock.transform.position.y, targetPosition.y))
                {
                    MoveBlock pushComponent = redBlock.GetComponent<MoveBlock>();
                    if (pushComponent != null && pushComponent.Move(direction))
                    {
                        return false;
                    }

                    return true;
                }
            }
        }

        return false;
    }

    private void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}