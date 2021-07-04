using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("Generate Map")] 
    public int sizeX;
    public int sizeZ;
    public GameObject tilePrefab;
    
    [Header("CurrentMap")]
    public Map map;
    public Transform mapParent;

    private void Start()
    {
        // map = CreateMap(sizeX, sizeZ);
    }

    private Map CreateMap(int x, int z)
    {
        var map = new Map(x, z);

        for (var i = 0; i < x; i++)
        for (var j = 0; j < z; j++)
        {
            var tile = Instantiate(tilePrefab, mapParent).GetComponent<Tile>();
            tile.name = "Tile " + i + "-" + j;
            tile.transform.localPosition = new Vector3(i, -0.5f, j);
            tile.posX = i;
            tile.posZ = j;

            if (i == 0 && j == 0)
                tile.spawn = true;
            if (i == x - 1 && j == z - 1)
                tile.goal = true;

            map.tiles.Add(tile);
        }

        return map;
    }
}