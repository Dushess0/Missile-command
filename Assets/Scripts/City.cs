using UnityEngine;
public class City : MonoBehaviour
{
    private bool destroyed = false;
    public bool Destroyed { get => destroyed; set => SetDestroyed(value); }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SetDestroyed(true);
    }
    private void SetDestroyed(bool destroyed)
    {
        this.destroyed = destroyed;
        gameObject.SetActive(!destroyed);
    }
    

}
