using UnityEngine;
public class Building : MonoBehaviour
{
    public bool Destroyed { get => destroyed; set => SetDestroyed(value); }
    protected bool destroyed;

    protected void SetDestroyed(bool destroyed)
    {
        this.destroyed = destroyed;
        gameObject.SetActive(!destroyed);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Destroyed = true;
    }

}