using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        if (gameObjects != null)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject != null)
                {
                    DestroyImmediate(gameObject);
                }
            }
            gameObjects.Clear();
        } 
        else
        {
            gameObjects = new List<GameObject>();
        }
        this.transform.position = new Vector3(0, 0, 0);
        for (int x = 0; x < length; x++)
        {
            location.x = x * tileSize;
            for (int y = 0; y < width; y++)
            {
                location.z = y * tileSize;
                GameObject newObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab as Object);
                newObject.transform.position = location;
                newObject.transform.localScale = new Vector3(tileSize, 1, tileSize);
                newObject.transform.parent = this.transform;
                Debug.Log(newObject);
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
                        GameObject newObject = (GameObject) PrefabUtility.InstantiatePrefab(wall as Object);
                        newObject.transform.position = wallLocation;
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
        GameObject newObject = (GameObject)PrefabUtility.InstantiatePrefab(wall as Object);
        newObject.transform.position = location;
        newObject.transform.localScale = wallSize;
        newObject.transform.parent = this.transform;
        gameObjects.Add(newObject);
    }
}
