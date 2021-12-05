using UnityEngine;
public class City : Building
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        GameManager.instance.statistics.CitiesDestroyed++;
    }
}