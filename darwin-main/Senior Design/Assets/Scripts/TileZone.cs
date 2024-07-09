using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileZone {

    private static List<TileZone> allTileZones = new List<TileZone>();

    private int zoneIndex = 0;

    private List<Tile> tiles = new List<Tile>();

    public TileZone() {

        allTileZones.Add(this);

        this.zoneIndex = allTileZones.Count - 1;
    }


    public static void InitTileZones() {

        for(int i = 0; i < MapGenerator.GetMapSize(); i++) {

            for(int j = 0; j < MapGenerator.GetMapSize(); j++) {

                Tile curr = MapGenerator.GetTileAt(j, i);

                if(curr.IsBarrier() || curr.IsInZone()) { continue; }

                TileZone newZone = new TileZone();

                newZone.AddTile(curr);
                newZone.FillZone(j, i);
            }
        }
    }

    private void FillZone(int xindex, int yindex) {

        for(int i = -1; i < 2; i++) {

            for(int j = -1; j < 2; j++) {

                if(i + j == 0 || i + j == -2 || i + j == 2) { continue; }

                if (xindex + j < 0 ||
                    xindex + j > MapGenerator.GetMapSize() - 1 ||
                    yindex + i < 0 ||
                    yindex + i > MapGenerator.GetMapSize() - 1
                    ) { continue; }

                Tile t = MapGenerator.GetTileAt(xindex + j, yindex + i);

                if (t.IsInZone() || t.IsBarrier()) { continue; }

                AddTile(t);

                FillZone(xindex + j, yindex + i);
                
            }
        }
    }

    public static void RemoveAllButLargestZone() {

        TileZone largest = TileZone.GetLargestZone();

        for(int i = 0; i < allTileZones.Count; i++) {

            if(allTileZones[i].Equals(largest)) { continue; }

            for(int j = 0; j < allTileZones[i].GetTiles().Count; j++) {

                allTileZones[i].GetTiles()[j].SetBarrier(true);
            }
        }

        allTileZones.Clear();
        allTileZones.Add(largest);
    }


    public static TileZone GetLargestZone() {

        if(allTileZones.Count == 0) { return null; }

        if(allTileZones.Count == 1) { return allTileZones[0]; }

        TileZone largest = allTileZones[0];

        for(int i = 1; i < allTileZones.Count; i++) {

            if(allTileZones[i].GetTiles().Count > largest.GetTiles().Count) {

                largest = allTileZones[i];
            }
        }

        return largest;
    }



    public List<Tile> GetTiles() { return tiles; }


    public int GetZoneIndex() { return zoneIndex; }


    public void AddTile(Tile tile) {

        if(tiles.Contains(tile)) { return; }

        tiles.Add(tile);
        tile.SetZoneIndex(zoneIndex);
    }


    public void RemoveTile(Tile tile) {

        if(!tiles.Contains(tile)) { return; }

        tiles.Remove(tile);
        tile.SetZoneIndex(-1);
    }
}
