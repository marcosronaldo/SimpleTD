using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject towerRangePrefab;
    public GameObject towerDamagePrefab;
    public GameObject towerSlowPrefab;

    public MapManager mapManager;
    public AIManager AIManager;

    public static GameManager Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PutTower(Tile t, Tower.Type type)
    {
        var prefab = type switch
        {
            Tower.Type.Range => towerRangePrefab,
            Tower.Type.Damage => towerDamagePrefab,
            Tower.Type.Slow => towerSlowPrefab,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

        var tower = Instantiate(prefab, t.transform);
        tower.transform.localPosition = new Vector3(0, 1, 0);
        t.tower = tower.GetComponent<Tower>();
    }
}
