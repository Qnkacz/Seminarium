using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator mapGenerator;
    public Transform tilePrefab;
    public Transform obstacleprefab;
    public Transform[] trees;
    public Transform[,] tilesArr;
    public GameObject tile_botleft;
    public GameObject tile_toplight;
    public float diagonal;
    private ObstacleInfo ob;
    public Vector2 mapSize;

    List<Coord> allTileCoords;
    Queue<Coord> shuffledTileCoords;
    public UIManager uimanager;

    public int seed;
    private int obstacleCount;
    [Range(0.1f, .5f)] 
    public float obstaclePercentage;
    private int treeCount;
    private void Start()
    {
        mapGenerator = this;
        tilesArr = new Transform[(int)mapSize.x,(int) mapSize.y];
        GenerateMap();
        tile_botleft = tilesArr[0, 0].gameObject;
        tile_toplight = tilesArr[tilesArr.GetLength(0)-1,tilesArr.GetLength(1)-1].gameObject;
        diagonal = Vector3.Distance(tile_botleft.transform.position, tile_toplight.transform.position);
    }

    public void GenerateMap()
    {
        obstacleCount = Mathf.FloorToInt((mapSize.x * mapSize.y) * obstaclePercentage);
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

        for (int i = 0; i < 3; i++)
        {
            GenerateFertilizers();
        }
        /// adding the tiles
        for (int x=0; x<mapSize.x;x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Vector3 tilePos = CoordToPos(x, y);
                Transform newTile = Instantiate(tilePrefab, tilePos, Quaternion.Euler(Vector3.right*90)) as Transform;
                newTile.GetComponent<tileInfo>().setCoords((int)tilePos.x, (int)tilePos.z);
                newTile.name = (int)tilePos.x + " " + (int)tilePos.z;
                newTile.parent = mapHolder;
                tilesArr[x,y] = newTile;
                newTile.GetComponent<tileInfo>().MyPlaceInArray(x, y);
            }
        }
       


        for (int i = 0; i < obstacleCount; i++)
        {
            Coord randomCoord = getRandCoord();
            Vector3 obstaclePos = CoordToPos(randomCoord.x, y: randomCoord.y);
            Transform newObstacle = Instantiate(obstacleprefab, obstaclePos + Vector3.up * .3f, Quaternion.identity) as Transform;
            ob = newObstacle.GetComponent<ObstacleInfo>();
            ob.setCoords((int)obstaclePos.x, (int)obstaclePos.z);
            ob.boulder.transform.rotation = Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f));

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
        return new Vector3(-mapSize.x/2 + .5f + x*2, 0, -mapSize.y/2  + .5f + y*2);
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
    public void GenerateFertilizers()
    {
        GameObject SoilGrader = GameObject.CreatePrimitive(PrimitiveType.Cube);
        SoilGrader.tag = "fertilizer";
        SoilGrader.transform.position = CoordToPos(Mathf.FloorToInt(Random.Range(0, mapSize.x)), Mathf.FloorToInt(Random.Range(0, mapSize.y)));
        SphereCollider collider = SoilGrader.AddComponent<SphereCollider>();
        collider.enabled = true;
        collider.radius = 280;
        Fertilizer fert = SoilGrader.AddComponent<Fertilizer>();
        Destroy(SoilGrader);
    }
}
