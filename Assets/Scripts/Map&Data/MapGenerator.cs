using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BrickManager))]
public class MapGenerator : MonoBehaviour {

    [HideInInspector]
    public List<MapData> generatedMaps = new List<MapData>();

    [Header("Map customizables")]
    [Tooltip("Maximum number of bricks spawned in one row")]
    public int maxBricksAtXaxis = 13;
    [Tooltip("Maximum number of bricks spawned in one column")]
    public int maxBricksAtYaxis = 10;
    [Tooltip("Distance between middle of each brick at X axis")]
    [SerializeField] float distanceX = 1.3f;
    [Tooltip("Distance between middle of each brick at Y axis")]
    [SerializeField] float distanceY = 0.4f;
    [Tooltip("Empty space between each brick at X axis")]
    [SerializeField] float xSpace = 0.1f;
    [Tooltip("Empty space between each brick at Y axis")]
    [SerializeField] float ySpace = 0;
    [Tooltip("Increasing probability scaling with number of completed levels durign one run")]
    [SerializeField] int difficultyMulitplier = 5;
    [Tooltip("Starting point of first brick, bricks generates from left to right")]
    [SerializeField] Vector3 mapOrigin = new Vector3(-8, 9, 0);

    [Header("Serializables")]
    [SerializeField] GameObject brickPrefab;
    [SerializeField] BrickManager brickManager;

    int[,] map;
    int numberOfGenerations;

    public struct MapData {
        public string seed;
        public float fillPercent;
    }


    public void NewMap() {
        GenerateMap(GenerateNewMapData());
    }

    public void LoadOrGenerateMap(int level) {
        if (level < generatedMaps.Count) {
            GenerateMap(generatedMaps[level]);
        }
        else {
            NewMap();
        }
    }

    public void GenerateMap(MapData data) {
        brickManager.bricks = new List<Brick>();
        map = new int[maxBricksAtXaxis, maxBricksAtYaxis];
        CleanMap();
        System.Random pseudoRandom = new System.Random(data.seed.GetHashCode());
        for (int y = 0; y < maxBricksAtYaxis; y++) {
            for (int x = 0; x <= maxBricksAtXaxis / 2; x++) {

                map[x, y] = (pseudoRandom.Next(0, 100) < data.fillPercent) ? 1 : 0;
                map[maxBricksAtXaxis - 1 - x * 1, y] = map[x, y];

                if (map[x, y] == 1) {
                    int healthPoints = BrickHealthRandomizer();
                    GenerateBrickAt(x, y, healthPoints);
                    if (x != maxBricksAtXaxis - 1 - x) {
                        GenerateBrickAt(maxBricksAtXaxis - 1 - x * 1, y, healthPoints);
                    }
                }
            }
        }
        //PositionBricks();
    }

    public MapData GenerateNewMapData() {
        MapData data = new MapData();
        data.seed = Random.Range(0f, 1f).ToString();
        System.Random pseudoRandom = new System.Random(data.seed.GetHashCode());
        data.fillPercent = pseudoRandom.Next(Mathf.Clamp(10 + GameplayManager.Instance.levelsCompleted * 5, 1, 100), Mathf.Clamp(20 + GameplayManager.Instance.levelsCompleted * 5, 1, 100));
        generatedMaps.Add(data);
        numberOfGenerations++;
        return data;
    }

    public Vector3 GetPositionFromCoords(Vector2 coords) {
        return mapOrigin + new Vector3(distanceX * coords.x + xSpace, -distanceY * coords.y + ySpace, 0);
    }

    public void CleanMap() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
    }

    public void GenerateBrickAt(int x, int y, int healthPoints) {
        GameObject go = Instantiate(brickPrefab, transform);
        Brick brick = go.GetComponent<Brick>();
        brick.Init(new Vector2(x, y), healthPoints);
        brick.transform.position = GetPositionFromCoords(new Vector2(x, y));
        brickManager.bricks.Add(brick);
    }

    public void PositionAllBricks(int[,] mapToLoad) {
        brickManager.bricks = new List<Brick>();
        for (int y = 0; y < maxBricksAtYaxis; y++) {
            for (int x = 0; x < maxBricksAtXaxis; x++) {
                if (mapToLoad[x, y] > 0) {
                    GenerateBrickAt(x, y, mapToLoad[x, y]);
                }
            }
        }
    }

    int BrickHealthRandomizer() {
        return Random.Range(1, Mathf.Clamp(3 + GameplayManager.Instance.levelsCompleted, 3, 6));
    }
}
