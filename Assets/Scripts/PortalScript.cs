using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PortalScript : MonoBehaviour
{
    private GameObject[] redBlocks;

    void Start()
    {
        redBlocks = GameObject.FindGameObjectsWithTag("redBlock");
    }

    void Update()
    {
        foreach (var redBlock in redBlocks)
        {
            if (redBlock != null && Vector3.Distance(redBlock.transform.position, transform.position) < 0.1f)
            {
                EndLevel();
                break;
            }
        }
    }

    private void EndLevel()
    {
        if (SceneController.Instance != null)
        {
            SceneController.Instance.LoadNextLevel();
        }
    }
}