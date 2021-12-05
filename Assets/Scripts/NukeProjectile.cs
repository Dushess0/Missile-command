using UnityEngine;

public class NukeProjectile : Projectile
{
    public int PointsForDestroy = 50;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("explosion") || collision.gameObject.layer == LayerMask.NameToLayer("antiAir"))
        {
            GameManager.instance.Score += PointsForDestroy;
        }
        else
        {
            GameManager.instance.statistics.NukesReachedTarget++;
        }
        base.OnTriggerEnter2D(collision);
        
    }

}
