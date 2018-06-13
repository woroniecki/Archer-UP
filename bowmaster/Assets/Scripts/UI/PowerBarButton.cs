using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Button to set power of shoot and shooting
/// </summary>
public class PowerBarButton : MonoBehaviour {

    private Scrollbar powerBar;
    private PlayerController playerController;
    private bool isExecuting = false;
    private bool isPressed = false;

    void Start () {
        playerController = PlayerController.instance as PlayerController;
        powerBar = Utility.FindChildByName("PowerBar", transform).GetComponent<Scrollbar>();
    }

    void OnGUI() {
        if (GameController.isGameDone)
        {
            if (isExecuting)
                powerBar.size = 0;
            return;
        }

        if (playerController.isCharged && isPressed)
        {
            isExecuting = true;
        }

        if (isExecuting)
        {
            powerBar.value = 0;
            powerBar.size += 0.8f * Time.deltaTime;
            playerController.SetPowerShoot(powerBar.size);
        }
#if UNITY_EDITOR
        if (Input.GetJoystickNames().Length > 0)
        {
            if (Input.GetKey("joystick button 0"))
                PressDown();
            if (isExecuting && !Input.GetKey("joystick button 0"))
                PressUp();
        }
#endif
    }

    /// <summary>
    /// on press down button
    /// </summary>
    public void PressDown()
    {
        isPressed = true;
    }

    /// <summary>
    /// on press up button
    /// </summary>
    public void PressUp()
    {
        isPressed = false;
        if (GameController.isGameDone)
            return;
        isExecuting = false;
        playerController.Shoot();
        powerBar.size = 0;
    }

}
