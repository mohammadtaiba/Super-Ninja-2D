using UnityEngine;

[System.Serializable]
public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // The object you want to spawn
    public Transform spawnPoint1; // The first spawn point
    public Transform spawnPoint2; // The second spawn point

    public float minSpawnInterval = 2f; // Minimum time between spawns
    public float maxSpawnInterval = 5f; // Maximum time between spawns

    private float nextSpawnTime; // Time when the next object will be spawned

    void Start()
    {
        // Set the initial spawn time
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void Update()
    {
        // Check if it's time to spawn a new object
        if (Time.time >= nextSpawnTime)
        {
            SpawnObject();
            // Set the next spawn time within the specified interval
            nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    void SpawnObject()
    {
        // Generate a random position between the two spawn points
        Vector3 spawnPosition = Vector3.Lerp(spawnPoint1.position, spawnPoint2.position, Random.Range(0f, 1f));

        // Spawn the object at the calculated position
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }
}
