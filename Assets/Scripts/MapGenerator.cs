using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Transform tilePrefab;
    public Transform obstacleprefab;
    public Transform[] trees;

    public Vector2 mapSize;

    List<Coord> allTileCoords;
    Queue<Coord> shuffledTileCoords;

    public int seed;
    public int obstacleCount;
    private int treeCount;
    private void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        treeCount = ((int)mapSize.x * (int)mapSize.y) - obstacleCount;
        allTileCoords = new List<Coord>();
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                allTileCoords.Add(new Coord(x, y));
            }
        }

        shuffledTileCoords = new Queue<Coord> ( Utility.ShuffleArray(allTileCoords.ToArray(),seed));
        string holderName = "Generated Map";

        if(transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = this.transform;

        /// adding the tiles
        for(int x=0; x<mapSize.x;x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Vector3 tilePos = CoordToPos(x, y);
                Transform newTile = Instantiate(tilePrefab, tilePos, Quaternion.Euler(Vector3.right*90)) as Transform;
                newTile.GetComponent<tileInfo>().setCoords((int)tilePos.x, (int)tilePos.z);
                newTile.name = (int)tilePos.x + " " + (int)tilePos.z;
                newTile.parent = mapHolder;
            }
        }


        
        for (int i = 0; i < obstacleCount; i++)
        {

            Coord randomCoord = getRandCoord();
            Vector3 obstaclePos = CoordToPos(randomCoord.x, y: randomCoord.y);
            Transform newObstacle = Instantiate(obstacleprefab, obstaclePos + Vector3.up * .3f, Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f))) as Transform;
            newObstacle.GetComponent<ObstacleInfo>().setCoords((int)obstaclePos.x, (int)obstaclePos.z);
            
        }

        for (int i = 0; i < treeCount; i++)
        {
            Coord randomCoord = getRandCoord();
            Vector3 obstaclePos = CoordToPos(randomCoord.x, y: randomCoord.y);
            Transform tree = Instantiate(trees[Random.Range(0,trees.Length)], obstaclePos, Quaternion.identity) as Transform;
        }
    }

    Vector3 CoordToPos(int x, int y)
    {
        return new Vector3(-mapSize.x / 2 + .5f + x, 0, -mapSize.y / 2 + .5f + y);
    }

    public Coord getRandCoord()
    {
        Coord randomCoord = shuffledTileCoords.Dequeue();
        shuffledTileCoords.Enqueue(randomCoord);
        return randomCoord;
    }
    public struct Coord
    {
        public int x, y;
        public Coord(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public int getX() { return this.x; }
        public int gety() { return this.y; }
    }
}
