using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public Vector2 destination;
    public GameObject explosion;
    public static float GetAngleFromVectorFloat(Vector2 dir)
    {
        dir = dir.normalized;
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        if (angle < 0)
            angle += 360;
        return angle;
    }
    public void Setup(Vector2 destination)
    {
        this.destination = destination;
        Vector2 direction = destination - new Vector2(transform.position.x, transform.position.y);
        var angle = GetAngleFromVectorFloat(direction);
        transform.rotation = Quaternion.Euler(0, 0, 90 - angle);
        float time = direction.magnitude / (speed);
        Invoke("Explode", time);
    }

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    public void Explode()
    {
        if (explosion)
            Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Explode();
    }
}
