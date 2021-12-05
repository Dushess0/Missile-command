using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Texture2D cursorImage;

    private PlayerControls playerControls;
    public List<AntiAirGun> guns;
    void Start()
    {
        ChangeCursor();
    }

    void Update()
    {
        if (playerControls.Default.FireLeft.triggered) Fire(0);
        if (playerControls.Default.FireMiddle.triggered) Fire(1);
        if (playerControls.Default.FireRight.triggered) Fire(2);

    }
    public void Awake()
    {
        playerControls = new PlayerControls();
        ChangeCursor();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
    public void Fire(int index)
    {
        var position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        guns[index].Fire(position);
    }
    void ChangeCursor()
    {
        Cursor.visible = true;
        Vector2 hotspot = new Vector2(cursorImage.width / 2, cursorImage.height / 2);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.SetCursor(cursorImage, hotspot, CursorMode.Auto);
    }
}
