using UnityEngine;

public class City : Building
{
    public static int PointsForSaving = 200;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        GameManager.instance.statistics.CitiesDestroyed++;
    }
}