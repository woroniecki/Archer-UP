using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Button to set power of shoot and shooting
/// </summary>
public class PowerBarButton : MonoBehaviour {

    private Scrollbar powerBar;
    private PlayerController playerController;
    private bool isPressed = false;

    void Start () {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        powerBar = Utility.FindChildByName("PowerBar", transform).GetComponent<Scrollbar>();
    }

    void OnGUI() {
        if (GameController.isGameDone)
        {
            if (isPressed)
                powerBar.size = 0;
            return;
        }
        if (isPressed)
        {
            powerBar.value = 0;
            powerBar.size += 0.8f * Time.deltaTime;
            playerController.setPowerShoot(powerBar.size);
        }
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
        if (GameController.isGameDone)
            return;
        isPressed = false;
        playerController.Shoot();
        powerBar.size = 0;
    }

}
