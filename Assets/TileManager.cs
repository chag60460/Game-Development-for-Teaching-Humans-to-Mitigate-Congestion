using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float zSpawn = 0;
    public float tileLength = 30;

    // Start is called before the first frame update
    void Start()
    {
        spawnTile(0);
        spawnTile(1);
        spawnTile(4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnTile(int tileIndex)
    {
        Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        zSpawn += tileLength;

    }
}
