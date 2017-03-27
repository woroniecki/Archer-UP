using UnityEngine;


/// <summary>
/// Controls arrow shooting
/// </summary>
public class ArrowShooter : MonoBehaviour
{
    /// <summary>
    /// sound during shoot
    /// </summary>
    public AudioClip shootSound;
    /// <summary>
    /// prefab of arrow
    /// </summary>
    private Object arrowPrefab;
    /// <summary>
    /// bow object
    /// </summary>
    private Transform bow;
    /// <summary>
    /// bowscript component of bow
    /// </summary>
    protected Bow bowScript;
    /// <summary>
    /// current charged arrow
    /// </summary>
    private Arrow chargedArrow;
    /// <summary>
    /// end of charged arrow
    /// </summary>
    protected Transform endArrow;
    /// <summary>
    /// if false can't shoot
    /// </summary>
    private bool isCharged = false;

    void Start()
    {
        DoInitialization();
    }

    /// <summary>
    /// Init script
    /// </summary>
    protected void DoInitialization()
    {
        arrowPrefab = Resources.Load("Arrow/arrow");
        bow = Utility.FindInChildsByName("bow", transform);
        if (bow == null)
            Debug.LogError("ArrowShooter:DoInitialization() Couldn't find bow child");
        bowScript = bow.GetComponent<Bow>();
        if (bowScript == null)
            Debug.LogError("ArrowShooter:DoInitialization() Couldn't get bowScript component");
        Charge();
    }

    /// <summary>
    /// Charge arrow
    /// </summary>
    void Charge()
    {
        GameObject arrow = Instantiate(arrowPrefab,
                                       bow.position,
                                       Quaternion.Euler(0, 0, bow.rotation.eulerAngles.z + 180),
                                       bow) as GameObject;
        arrow.transform.rotation = Quaternion.Euler(0, 0, bow.rotation.eulerAngles.z + 180);
        isCharged = true;
        chargedArrow = arrow.GetComponent<Arrow>();
        endArrow = chargedArrow.getEndTransform();
    }

    /// <summary>
    /// set power of charged arrow
    /// </summary>
    /// <param name="power"></param>
    public void setPowerShoot(float power)
    {
        chargedArrow.setPower(power);
    }

    /// <summary>
    /// shoot arrow
    /// </summary>
    public void Shoot()
    {
        if (isCharged)
        {
            if (shootSound)
                SoundsManager.PlaySound(shootSound);
            else
                Debug.LogWarning("ArrowShooter:Shoot() shootSound is not assigned.");
            chargedArrow.Shoot();
            isCharged = false;
            Charge();
        }
    }
}
