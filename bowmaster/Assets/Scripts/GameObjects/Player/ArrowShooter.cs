using UnityEngine;


/// <summary>
/// Controls arrow shooting
/// </summary>
public class ArrowShooter : MonoBehaviour
{
    public ChargeAnimation chargeAnimation;
    public AudioClip shootSound;
    private Object _arrowPrefab;
    public Transform bow;
    protected Bow bowScript;
    public Arrow chargedArrow;
    public Transform endArrow;
    public Transform handPointer;
    private bool _isCharged = false;

    public bool isCharged { get { return _isCharged; } set { _isCharged = value; } }

    public Object arrowPrefab { get { return _arrowPrefab; } }

    void Start()
    {
        DoInitialization();
    }

    /// <summary>
    /// Init script
    /// </summary>
    protected void DoInitialization()
    {
        _arrowPrefab = Resources.Load("Arrow/arrow");
        bow = Utility.FindInChildsByName("bow", transform);
        if (bow == null)
            Debug.LogError("ArrowShooter:DoInitialization() Couldn't find bow child");
        bowScript = bow.GetComponent<Bow>();
        if (bowScript == null)
            Debug.LogError("ArrowShooter:DoInitialization() Couldn't get bowScript component");
        Charge(0f);
    }

    /// <summary>
    /// Charge arrow
    /// </summary>
    void Charge(float time = 1f)
    {
        chargeAnimation.RunAnimation (time);
    }

    /// <summary>
    /// set power of charged arrow
    /// </summary>
    /// <param name="power"></param>
    public void SetPowerShoot(float power)
    {
        chargedArrow.setPower(power);
    }

    /// <summary>
    /// shoot arrow
    /// </summary>
    public void Shoot()
    {
        if (_isCharged)
        {
            if (shootSound)
                SoundsManager.PlaySound(shootSound);
            else
                Debug.LogWarning("ArrowShooter:Shoot() shootSound is not assigned.");
            chargedArrow.Shoot();
            _isCharged = false;
            Charge();
        }
    }
}
