using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Map
{
    public int sizeX;
    public int sizeZ;
    public Vector3 goal;
    public Vector3 spawn;
        
    [SerializeField]
    public List<Tile> tiles;

    public Map(int sizeX, int sizeZ)
    {
        this.sizeX = sizeX;
        this.sizeZ = sizeZ;
        tiles = new List<Tile>();
    }

    public Tile GetTile(int x, int z)
    {
        return tiles[CoordinatesToIndex(x, z)];
    }
    
    public int CoordinatesToIndex(int x, int z)
    {
        return x * sizeZ + z;
    }
}
