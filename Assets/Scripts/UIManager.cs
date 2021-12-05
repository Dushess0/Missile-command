using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public float textDelay = 5f;
    public TMP_Text levelText;
    public TMP_Text RepairsText;
    public TMP_Text ammoText;
    public TMP_Text scoreText;
    public TMP_Text endGameText;
    public GameObject newGameButton;

    public void AnounceLevel(int number, LevelData level)
    {

        DisplayNewLevel(number);
        if (level.restockAmmo) AnounceAmmoRestocking();
        if (level.repair) AnounceRepairs();


    }
    IEnumerator DisableAfterDelay(float delay, GameObject gameObject)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
    void DisplayNewLevel(int level)
    {
        levelText.text = $"Level {level}";
        levelText.gameObject.SetActive(true);
        StartCoroutine(DisableAfterDelay(textDelay, levelText.gameObject));
    }
    void AnounceAmmoRestocking()
    {
        ammoText.gameObject.SetActive(true);
        StartCoroutine(DisableAfterDelay(textDelay, ammoText.gameObject));
    }
    void AnounceRepairs()
    {
        RepairsText.gameObject.SetActive(true);
        StartCoroutine(DisableAfterDelay(textDelay, RepairsText.gameObject));
    }
    public void ShowGameEnd()
    {
        var statistics = GameManager.instance.statistics;
        string text = "Nukes fired: {0}\n Nukes reached targets: {1}\n AA missiles fired: {2}\n Cities destroyed: {3}\n Rounds survided: {4}\n Total Score: {5}";
        endGameText.text = string.Format(text, statistics.NukesFired, statistics.NukesReachedTarget, statistics.AntiAirFired, statistics.CitiesDestroyed, GameManager.instance.currentLevel, GameManager.instance.Score);
        endGameText.gameObject.SetActive(true);
        newGameButton.SetActive(true);

    }
    public void HideGameEnd()
    {
        newGameButton.SetActive(false);
        endGameText.gameObject.SetActive(false);
    }

    void Update()
    {
        scoreText.text = $"Score: {GameManager.instance.Score}";
    }

}
