using UnityEngine;

public class FlyingStar : MonoBehaviour
{

    /// <summary>
    /// script which set target position
    /// </summary>
    public StarsContainer target;
    /// <summary>
    /// moving speed
    /// </summary>
    public float speedMove = 1F;
    public float startScale = 0.2f;
    public float targetScale = 0.6f;
    /// <summary>
    /// speed of animation scale
    /// </summary>
    public float speedScale = 1F;
    /// <summary>
    /// no of which star it is
    /// </summary>
    public int starNo;

    private Transform particle;
    private Transform accelerometer;
    private bool moveOn = true;
    private bool scaleOn = true;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("StarsContainer").GetComponent<StarsContainer>();
        accelerometer = GameObject.FindGameObjectWithTag("Accelerometer").transform;
        targetScale = target.getRealStarSize();
        starNo = target.addStar();
        particle = Utility.FindChildByName("StarParticle", transform);
        particle.rotation = Quaternion.FromToRotation(transform.position, target.getWorldPosStar(starNo));
        transform.localScale = new Vector3(startScale, startScale, 1);
    }

    void Update()
    {
        if (GameController.isGameDone)
            FinnishVisualisation();
        float step = speedMove * Time.deltaTime;
        Vector3 targetV = target.getWorldPosStar(starNo);
        transform.position = Vector3.MoveTowards(transform.position, targetV, step);
        if (moveOn)
        {
            if (transform.position.x == targetV.x && transform.position.y == targetV.y)
            {
                moveOn = false;
                FinnishVisualisation();
            }
        }
        if (scaleOn)
        {
            float scale = transform.localScale.x + speedScale * Time.deltaTime;
            if (scale >= targetScale)
            {
                scale = targetScale;
                scaleOn = false;
            }
            transform.localScale = new Vector3(scale, scale, 1);
        }
        transform.rotation = accelerometer.rotation;
    }

    /// <summary>
    /// after star is on right position it finnish moving it by creating same star but in UI system and destroy this one
    /// </summary>
    void FinnishVisualisation()
    {
        target.createUiStar(starNo);
        ParticleSystem particS = Utility.FindChildByName("StarParticle", particle).GetComponent<ParticleSystem>();
        transform.GetComponent<SpriteRenderer>().enabled = false;
        particS.startSize = 0;
        Object.Destroy(gameObject, 1.0f);
    }

}
