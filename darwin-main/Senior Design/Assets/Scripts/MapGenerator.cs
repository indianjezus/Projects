using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NoiseTest;

public class MapGenerator : MonoBehaviour {

    public int seed = -1;

    public float terrainDensity = 2f;

    public bool removeAllButLargestZones = true;

    public static int mapSize = 175;

    public int maxCreatures = 10;

    public Tile tile;

    public Food food;

    public GameObject creature;

    public GameObject mapgen;

    public GameObject creaturegen;

    private static Tile[,] map;

    private static float[,] pheromap;

    private static List<Tile> tiles = new List<Tile>();

    private OpenSimplexNoise noiseGen;


    void Start() {

        InitMap();
    }


    public static List<Tile> GetTiles() { return tiles; }


    public static Tile[,] GetTileMap() { return map; }


    public static float[,] GetPheroMap() { return pheromap; }


    public static int GetMapSize() { return mapSize; }


    private void InitMap() {

        if (seed < 0) {

            seed = Random.Range(0, int.MaxValue);

        }

        noiseGen = new OpenSimplexNoise(seed);

        map = new Tile[mapSize, mapSize];
        pheromap = new float[mapSize, mapSize];

        for (int i = 0; i < mapSize; i++) {

            for (int j = 0; j < mapSize; j++) {

                Vector3 pos = new Vector3(
                    (j * tile.transform.localScale.x) - ((mapSize * tile.transform.localScale.x) / 2),
                    (i * tile.transform.localScale.y) - ((mapSize * tile.transform.localScale.y) / 2),
                    1f);

                Tile newtile = Instantiate(tile, pos, Quaternion.identity);
                newtile.transform.SetParent(mapgen.transform, false);

                map[j, i] = newtile;

                if (i == 0 || j == 0 || i == mapSize - 1 || j == mapSize - 1) {

                    map[j, i].SetBarrier(true);
                    continue;
                }

                //double noise = noiseGen.Evaluate(i * terrainDensity, j * terrainDensity) * 100;
                double noise = noiseGen.Evaluate(j * 0.1, i * 0.1);

                if (noise <= terrainDensity) {

                    map[j, i].SetBarrier(true);
                    map[j, i].SetMaxPheroStrength_alpha(0f);

                } else {

                    map[j, i].GetComponent<BoxCollider2D>().enabled = false;
                    map[j, i].SetBarrier(false);

                }
            }
        }

        TileZone.InitTileZones();

        if(removeAllButLargestZones) { 
            TileZone.RemoveAllButLargestZone();
        }

        SimplifyMap();

        AddFood();

        AddCreatures();
    }


    private void AddCreatures() {

        int totalCreatures = 0;

        for(int i = 0; i < maxCreatures; i++) {

            int t = Random.RandomRange(0, tiles.Count);
            GameObject newcre = Instantiate(creature, tiles[t].transform.position, Quaternion.identity);
            newcre.transform.SetParent(creaturegen.transform, false);
            totalCreatures++;

        }
        /*
        for(int i = 0; i < mapSize; i++) {

            for(int j = 0; j < mapSize; j++) {

                if(map[j, i].IsInZone()) {

                    int num = Random.Range(0, 100);

                    if (num == 50) {

                        GameObject newcre = Instantiate(creature, map[j, i].transform.position, Quaternion.identity);
                        newcre.transform.SetParent(creaturegen.transform, false);
                        totalCreatures++;

                        if(totalCreatures >= maxCreatures) { return; }
                    }
                }
            }
        }
        */
    }


    public static Tile GetTileAt(int x, int y) {

        if(x < 0 || x > mapSize - 1 || y < 0 || y > mapSize) {

            Debug.Log("Attempted to access a tile outsize the range of the tilemap: X" + x + "   Y: " + y);
            return null;
        }

        return map[x, y];
    }


    public static int[] GetTileCoords(Tile t) {

        for (int i = 1; i < mapSize - 1; i++) {

            for (int j = 1; j < mapSize - 1; j++) {

                if(map[j, i] != null) {

                    if(map[j, i] == t) {

                        int[] rtrn = { j, i };
                        return rtrn;
                     }
                }
            }
        }

        return null;
    }


    private void SimplifyMap() {

        List<Tile> toDestroy = new List<Tile>();

        for (int i = 1; i < mapSize - 1; i++) {

            for(int j = 1; j < mapSize - 1; j++) {

                if(IsBarrierSurrounded(j, i)) {

                    toDestroy.Add(GetTileAt(j, i));
                } else {

                    if (!GetTileAt(j, i).IsBarrier()) {

                        tiles.Add(GetTileAt(j, i));
                    }
                }
            }
        }
        
        for(int i = 0; i < toDestroy.Count; i++) {
            Destroy(toDestroy[i].gameObject);
        }
        
    }


    private bool IsBarrierSurrounded(int x, int y) {

        for (int i = -1; i < 2; i++) {

            for (int j = -1; j < 2; j++) {

                if(!GetTileAt(x + j, y + i).IsBarrier()) {
                    return false;
                }
            }
        }
        return true;
    }


    private void AddFood() {
        /*
        Random.seed = seed;
        int foodTile = Random.Range(0, tiles.Count);

        Instantiate(food, tiles[foodTile].transform.position, Quaternion.identity);

        Debug.Log(GetTileCoords(tiles[foodTile])[0] + "   " + GetTileCoords(tiles[foodTile])[1]);
        //InitPheromonesNearFood(tiles[foodTile], 3);
        tiles[foodTile].SetMinPheroStrength_alpha(1f);
        
        //PropogateAlphaPheros(tiles[foodTile], 1f);
        */
        Instantiate(food, tiles[tiles.Count/2].transform.position, Quaternion.identity);
        tiles[tiles.Count / 2].SetMinPheroStrength_alpha(1f);

    }


    private int counter = 0;
    public void Update() {

        if(counter == 250) {

            for(int i = 0; i < tiles.Count; i++) {

                if(tiles[i].GetPheroStrength_alpha() > 0f) {

                    tiles[i].SetPheroStrength_alpha(tiles[i].GetPheroStrength_alpha() - 0.02f);
                }
            }

            counter = 0;
        } else {

            counter += 1;
        }        
    }


    private void PropogateAlphaPheros(Tile t, float strength) {

        if(strength <= 0.1f) { return; }

        int[] coords = GetTileCoords(t);

        for (int i = -1; i < 2; i++) {

            for (int j = -1; j < 2; j++) {

                if (i == 0 && j == 0) { continue; }

                Tile t_ = GetTileAt(coords[0] + j, coords[1] + i);

                if (t_ != null && !t_.IsBarrier()) {

                    if(t_.GetPheroStrength_alpha() < strength) {

                        //t_.SetMinPheroStrength_alpha(strength);
                        //t_.SetMaxPheroStrength_alpha(strength * 1.1f);
                        t_.SetPheroStrength_alpha(strength);
                        PropogateAlphaPheros(t_, strength * 0.8f);
                    }
                }

            }
        }

    }


    private void InitPheromonesNearFood(Tile foodTile, float distance) {

        if (distance <= 0) { return; }

        for(int i = 0; i < tiles.Count; i++) {

            float dist = Vector2.Distance(foodTile.transform.position, tiles[i].transform.position);
            if (dist <= distance) {

                tiles[i].SetMinPheroStrength_alpha((1 - (dist/distance)) * 0.5f);
            }
        }
    }
}
