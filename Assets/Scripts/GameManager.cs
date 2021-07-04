using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Prefabs")] 
    public GameObject towerRangePrefab;
    public GameObject towerDamagePrefab;
    public GameObject towerSlowPrefab;
    public GameObject enemyPrefab;

    [Header("Scene References")] public MapManager mapManager;

    public Transform enemyParent;

    [Header("UI References")] public Canvas menuCanvas;

    public Text titleText;
    public Canvas gameCanvas;
    public Text hpText;
    public Text enemiesLeftText;
    public Text energyText;
    public GameObject howToPlayPanel;

    [Header("Game Options")] public int maxEnemies;

    public int goalHP;
    public float spawnMinTime = 0.5f;
    public float spawnMaxTime = 1.5f;
    public int startEnergy = 100;
    public int energyPerSecond = 1;
    public int energyPerEnemy = 5;

    [HideInInspector] public int currentHP;

    public List<Enemy> enemies = new List<Enemy>();
    public List<Tower> towers = new List<Tower>();
    private bool endSpawn;
    private int energy;
    private bool gameStarted;
    private int spawned;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        OpenUI("Welcome to my Simple TD!!");
    }

    private void Update()
    {
        hpText.text = "HP: " + currentHP;
        enemiesLeftText.text = "Remaining: " + (maxEnemies - spawned + enemies.Count);
        energyText.text = "Energy: " + energy;

        if (gameStarted)
            CheckGameOver();
    }

    public void GainEnergy()
    {
        energy += energyPerSecond;
    }

    public void GainEnergyOnKill()
    {
        energy += energyPerEnemy;
    }

    public void StartGame()
    {
        InvokeRepeating(nameof(GainEnergy), 0, 1f);
        gameCanvas.gameObject.SetActive(true);

        foreach (var enemy in enemies)
            Destroy(enemy.gameObject);
        foreach (var tower in towers)
            Destroy(tower.gameObject);

        towers.Clear();
        enemies.Clear();

        energy = startEnergy;
        gameStarted = true;
        menuCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
        endSpawn = false;
        spawned = 0;
        currentHP = goalHP;
        StartCoroutine(Spawn());
    }

    public void OpenTutorial()
    {
        howToPlayPanel.SetActive(true);
    }

    public void CheckGameOver()
    {
        if (endSpawn && enemies.Count == 0) Win();

        if (currentHP <= 0) Lose();
    }

    public void Lose()
    {
        OpenUI("YOU LOSE");
    }

    public void Win()
    {
        OpenUI("YOU WIN");
    }

    public void OpenUI(string message)
    {
        CancelInvoke(nameof(GainEnergy));
        gameCanvas.gameObject.SetActive(false);
        gameStarted = false;
        Time.timeScale = 0;
        menuCanvas.gameObject.SetActive(true);
        titleText.text = message;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public IEnumerator Spawn()
    {
        spawned = 0;
        while (spawned++ < maxEnemies)
        {
            yield return new WaitForSeconds(Random.Range(spawnMinTime, spawnMaxTime));
            var enemy = Instantiate(enemyPrefab, mapManager.map.spawn, Quaternion.identity, enemyParent)
                .GetComponent<Enemy>();
            enemies.Add(enemy);
        }

        endSpawn = true;
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

        if (prefab.GetComponent<Tower>().cost > energy)
            return;

        var tower = Instantiate(prefab, t.transform);
        tower.transform.localPosition = new Vector3(0, 1, 0);
        t.tower = tower.GetComponent<Tower>();
        energy -= t.tower.cost;
        towers.Add(t.tower);
    }
}