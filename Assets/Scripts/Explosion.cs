using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float duration = 1.5f;
    void Start()
    {
        Destroy(gameObject, duration);
    }
}
