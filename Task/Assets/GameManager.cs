using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public Renderer planeRenderer;      // drag your Plane here (its Renderer)
    public GameObject ballPrefab;       // drag your ball prefab here

    [Header("Settings")]
    public int ballCount = 5;
    public float ballYOffset = 0.2f;    // how high above the plane to spawn

 private void Start()
    {
       GenerateLevel();
    }

    private void GenerateLevel()
    {
         if (planeRenderer == null)
        {
            Debug.LogError("GameManager: planeRenderer is not assigned!");
            return;
        }

        if (ballPrefab == null)
        {
            Debug.LogError("GameManager: ballPrefab is not assigned!");
            return;
        }

        // 1) Get plane bounds
        Bounds bounds = planeRenderer.bounds;

        float minX = bounds.min.x;
        float maxX = bounds.max.x;
        float minZ = bounds.min.z;
        float maxZ = bounds.max.z;

        // (Optional) If you need width/height numbers:
        float width  = bounds.size.x;
        float height = bounds.size.z;
        Debug.Log($"Plane size: width={width}, height={height}");

        // 2) Spawn balls
        for (int i = 0; i < ballCount; i++)
        {
            float x = Random.Range(minX, maxX);
            float z = Random.Range(minZ, maxZ);
            float y = bounds.max.y + ballYOffset;

            Vector3 spawnPos = new Vector3(x, y, z);
            Instantiate(ballPrefab, spawnPos, Quaternion.identity);
        }
    }
    }



}
