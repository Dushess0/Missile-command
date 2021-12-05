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



    [HideInInspector]
    public int currentLevel = 0;




    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }
    void Start()
    {
        StartLevel();

    }
    void StartLevel()
    {
        uIManager.AnounceLevel(currentLevel + 1);
        var level = levels[currentLevel];
        if (level.restockAmmo) foreach (AntiAirGun gun in guns) gun.RestockAmmo();

        if (level.rebuildCities) foreach (City city in cities) city.Destroyed = false;

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
        yield return new  WaitForSeconds(delay);
        var min = nukeSpawnArea.bounds.min;
        var max = nukeSpawnArea.bounds.max;
        var point = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        var gameObject = Instantiate(nuke, point, Quaternion.identity);
        var target = PickTarget();
        gameObject.GetComponent<NukeProjectile>().Setup(target.transform.position);
    }
    GameObject PickTarget()
    {

        
        return target.gameObject;
    }



}
