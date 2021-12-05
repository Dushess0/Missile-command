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

    public void AnounceLevel(int level)
    {
        levelText.text = $"Level {level}";
        levelText.gameObject.SetActive(true);
        StartCoroutine(DisableAfterDelay(textDelay,levelText.gameObject));
    }
    IEnumerator DisableAfterDelay(float delay, GameObject gameObject)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
    
    public void AnounceAmmoRestocking()
    {
        ammoText.gameObject.SetActive(true);
        StartCoroutine(DisableAfterDelay(textDelay, ammoText.gameObject));
    }
    public void AnounceRepairs()
    {
        RepairsText.gameObject.SetActive(true);
        StartCoroutine(DisableAfterDelay(textDelay, RepairsText.gameObject));
    }
    public void ShowGameEnd()
    {

    }

    private void Update()
    {
        scoreText.text = $"Score: {GameManager.instance.Score}";
    }

}
