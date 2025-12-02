using UnityEngine;

public class EnvironmentGeneration : MonoBehaviour
{
    [Header("Tile Prefabs")]
    public GameObject startTile;
    public GameObject[] middleTiles;
    public GameObject endTile;

    [Header("Generation Settings")]
    public int numberOfMiddleTiles = 5;
    public float tileLength = 10f;  

    [Header("Generator Root")]
    public Transform levelParent;

    private void Start()
    {
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        if (startTile == null || endTile == null)
        {
            Debug.LogError("Start or End tile is missing!");
            return;
        }

        float currentZ = 0f;

        Instantiate(startTile, new Vector3(0, 0, currentZ), Quaternion.identity, levelParent);
        currentZ += tileLength;

        for (int i = 0; i < numberOfMiddleTiles; i++)
        {
            GameObject tileToSpawn = middleTiles[Random.Range(0, middleTiles.Length)];
            Instantiate(tileToSpawn, new Vector3(0, 0, currentZ), Quaternion.identity, levelParent);
            currentZ += tileLength;
        }

        Instantiate(endTile, new Vector3(0, 0, currentZ), Quaternion.identity, levelParent);
    }
}
