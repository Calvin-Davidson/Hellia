using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGridSpawner : MonoBehaviour
{
    [SerializeField] private int length = 7;
    [SerializeField] private int width = 7;
    [SerializeField] private float tileSize = 0.4f;
    [SerializeField] private GameObject prefab;

    [SerializeField] private bool spawnWalls = true;
    [SerializeField] private GameObject wall;
    [SerializeField] private float wallOffset = 2;
    [SerializeField] private Vector3 wallSize = new Vector3(4,4,4);
    [SerializeField] private Vector2 placeLocation = new Vector2(0, 0);
    private List<GameObject> gameObjects;

    private Vector3 location;
    public void Generate()
    {
        if (gameObject != null)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                DestroyImmediate(gameObject);
            }
        }
        gameObjects.Clear();
        this.transform.position = new Vector3(0, 0, 0);
        for (int x = 0; x < length; x++)
        {
            location.x = x * tileSize;
            for (int y = 0; y < width; y++)
            {
                location.z = y * tileSize;
                GameObject newObject = Instantiate(prefab, location, Quaternion.identity);
                newObject.transform.localScale = new Vector3(tileSize, 1, tileSize);
                newObject.transform.parent = this.transform;
                gameObjects.Add(newObject);
            }
        }
        if (spawnWalls)
        {   
            Vector3 wallLocation = location;
            wallLocation.y += wallOffset;
            for (int x = 0; x < length; x++)
            {
                wallLocation.x = x * tileSize;
                for (int y = 0; y < width; y++)
                {
                    wallLocation.z = y * tileSize;
                    if(x == 0 || x == length - 1 || y == 0 || y == width - 1)
                    {
                        GameObject newObject = Instantiate(wall, wallLocation, Quaternion.identity);
                        newObject.transform.localScale = wallSize;
                        newObject.transform.parent = this.transform;
                        gameObjects.Add(newObject);
                    }
                }
            }
        }
    }
    public void PlaceWall()
    {
        Vector3 location = new Vector3(0, 0, 0);
        location.y += wallOffset;
        location.x = placeLocation.x * tileSize;
        location.z = placeLocation.y * tileSize;
        GameObject newObject = Instantiate(wall, location, Quaternion.identity);
        newObject.transform.localScale = wallSize;
        newObject.transform.parent = this.transform;
        gameObjects.Add(newObject);
    }
}
