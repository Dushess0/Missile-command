using System.Collections;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Score = 0;

    public static GameManager instance = null;
    public BoxCollider2D nukeSpawnArea;
    public GameObject nuke;
    public City[] cities;
    public LevelData[] levels;
    public AntiAirGun[] guns;
    public UIManager uIManager;

    public Statistics statistics;


    [HideInInspector]
    public int currentLevel = 0;




    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }
    void Start()
    {
        StartNewGame();
    }
    void StartLevel()
    {
        LevelData level;
        if (currentLevel >= levels.Length)
        {
            level = ScriptableObject.CreateInstance<LevelData>();
            level.additional_missiles = Random.Range(6,10);
            level.initialWave = Random.Range(2, 10);
            level.launchInterval = Random.Range(1, 2);
            level.repair = currentLevel % 10 == 0;
            level.restockAmmo = currentLevel % 5 == 0;
        }
        else
        {
            level = levels[currentLevel];
        }
        uIManager.AnounceLevel(currentLevel + 1, level);
        if (level.restockAmmo)
        {
            foreach (AntiAirGun gun in guns) gun.RestockAmmo();
        }
        if (level.repair)
        {
            foreach (var gun in guns) gun.Destroyed = false;
            foreach (var city in cities) city.Destroyed = false;
        }

        for (int i = 0; i < level.initialWave; i++)
        {
            StartCoroutine(SpawnNuke());
        }
        for (int i = 0; i < level.additional_missiles; i++)
        {
            StartCoroutine(SpawnNuke(level.launchInterval * i));

        }
        StartCoroutine(EndLevel(level.launchInterval * level.additional_missiles + 5));
    }
    IEnumerator EndLevel(float delay)
    {
        yield return new WaitForSeconds(delay);

        currentLevel++;
        foreach (AntiAirGun gun in guns)
        {
            Score += gun.CurrentAmmo * 50;
        }

        int citiesAlive = cities.Where(city => city.Destroyed == false).Count();
        if (citiesAlive == 0)
        {
            uIManager.ShowGameEnd();
        }
        else
        {
            Score += citiesAlive * 50;
            StartLevel();
        }

    }

    IEnumerator SpawnNuke(float delay = 0)
    {
        yield return new WaitForSeconds(delay);
        var min = nukeSpawnArea.bounds.min;
        var max = nukeSpawnArea.bounds.max;
        var point = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        var gameObject = Instantiate(nuke, point, Quaternion.identity);
        var target = PickTarget();
        gameObject.GetComponent<NukeProjectile>().Setup(target.transform.position);
        statistics.NukesFired++;
    }
    GameObject PickTarget()
    {
        var targets = guns.Cast<Building>().Concat(cities.Cast<Building>()).Where(building => building.Destroyed == false).ToArray();
        if (targets.Length == 0) return guns[1].gameObject;
        return targets[Random.Range(0, targets.Count())].gameObject;

    }
    public void StartNewGame()
    {
        statistics = new Statistics();
        foreach (var gun in guns)
        {
            gun.Destroyed = false;
            gun.RestockAmmo();
        }
        foreach (var city in cities)
        {
            city.Destroyed = false;
        }
        currentLevel = 0;
        Score = 0;
        uIManager.HideGameEnd();
        StartLevel();
    }




}
