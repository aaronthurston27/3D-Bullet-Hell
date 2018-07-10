using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour {

    #region Main Variables

    public Transform[] spawnPoints;
    public MovingBlock projectileToSpawn;

    public float minSpeed = 2f;
    public float maxSpeed = 5f;

    public int blocksSpawned;
    public int maxBlocks = 6;

    public float spawnInterval = 1.5f;
    private float elapsedTime = 0;

    #endregion

    #region Static Variables

    private static BlockSpawner instance;

    public static BlockSpawner Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<BlockSpawner>();

            return instance;
        }

    }

    #endregion

    #region monobehavior methods
    // Use this for initialization
    void Start() {
        blocksSpawned = 0;
    }

    void Update() {
        if (elapsedTime >= spawnInterval && blocksSpawned < maxBlocks) // Make sure enough time has passed before spawning a block, as well as we don't spawn too many.
        {
            createblock(spawnPoints[Random.Range(0, spawnPoints.Length)]); // Spawn an incoming block at a randonmly chosen spawn point we defined.
            elapsedTime = 0;
        }
        elapsedTime += Time.deltaTime;
      
    }

    #endregion

    #region methods

    void createblock(Transform startingPoint)
    {
        Vector3 startingPos = new Vector3(startingPoint.transform.position.x, startingPoint.transform.position.y, startingPoint.transform.position.z);
        projectileToSpawn.transform.position = startingPos;
        projectileToSpawn.speed = Random.Range(minSpeed, maxSpeed);
        projectileToSpawn.followtime = Random.Range(0, 5);
        Instantiate(projectileToSpawn);
        ++blocksSpawned;
        elapsedTime = 0;
    }

    #endregion

}


