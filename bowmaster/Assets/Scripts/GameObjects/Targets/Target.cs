using UnityEngine;

/// <summary>
/// Copomnent required for all targets in game
/// </summary>
public class Target : MonoBehaviour
{
    /// <summary>
    /// sound on first hit
    /// </summary>
    public AudioClip hitSound;
    /// <summary>
    /// false if never hit
    /// </summary>
    bool targeted = false;

    /// <summary>
    /// set targeted to true, create flyingstar, play sound
    /// </summary>
    public virtual void Hit()
    {
        if (targeted || GameController.isGameDone)
            return;
        targeted = true;
        GameObject star = Instantiate(Resources.Load("UI/Star/Star"),
                                      transform.position,
                                      Quaternion.identity) as GameObject;
        if (hitSound)
            SoundsManager.PlaySound(hitSound);
        else
            Debug.LogWarning("Target:Hit() hitSound is not assigned.");
    }
}
