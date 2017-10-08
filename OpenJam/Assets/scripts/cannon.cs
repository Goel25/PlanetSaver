using UnityEngine;
using System.Collections;

public class cannon : MonoBehaviour {

    private Rigidbody2D rb;
    public Transform onPlanet;
    public Transform bulletPrefab;
    public gameManager gm;
    private AudioSource shootSound;

    public float minAngle = 0;
    public float maxAngle = 180;
    public float speed = 10f;

    public float chargeTime = 0f;
    public bool charging = false;
    public float maxChargeTime = 2f;
    public float minSpeed = 400f;
    public float maxSpeed = 1200f;

    public Transform spriteMask;
    public Vector3 SMNormalPos;

    [Range(-360, 360)]
    public float angle = 0f;

    void Awake() {
        onPlanet = GameObject.FindGameObjectWithTag("homePlanet").transform;
        gameManager.lastPlanetY = onPlanet.position.y;
        gameManager.bottomCamDist = onPlanet.position.y - Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;
    }

    // Use this for initialization
    void Start() {
        shootSound = GetComponent<AudioSource>();
        SMNormalPos = spriteMask.localPosition;
        gm = FindObjectOfType<gameManager>();
        rb = GetComponent<Rigidbody2D>();
        moveToPlanet(onPlanet.gameObject);
    }

    void Update() {
        if (charging) {
            float chargeAmount = Time.time - chargeTime;
            chargeAmount = Mathf.Clamp(chargeAmount, 0, maxChargeTime);
            float covered = 0.0296f;
            float shown = 1.3355f;
            float y = (chargeAmount - 0) * (shown - covered) / (maxChargeTime - 0) + covered;
            spriteMask.transform.localPosition = new Vector3(spriteMask.transform.localPosition.x, y, spriteMask.transform.localPosition.y);
        } else {
            spriteMask.transform.localPosition = SMNormalPos;
        }
        if (gm.resources > 0) {

            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                charging = true;
                chargeTime = Time.time;
            }

            if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) {
                chargeTime = Time.time - chargeTime; //How long you charged for
                charging = false;
                shoot();
            }
        }
    }

    public void shoot() {
        float power = (chargeTime - 0) * (maxSpeed - minSpeed) / (maxChargeTime - 0) + minSpeed;
        power = Mathf.Clamp(power, minSpeed, maxSpeed);

        Transform clone = Instantiate(bulletPrefab, transform.position, transform.rotation);
        clone.GetComponent<Rigidbody2D>().AddForce(clone.transform.up * power);
        shootSound.Play();

        gm.changeResources(-1);
    }

    //IEnumerator shoot() {
    //    if (!shooting) {
    //        shooting = true;
    //        float oldS = speed;
    //        speed = 0f;
    //        //TODO  Call recoil animation here!
    //        Transform clone = Instantiate(bulletPrefab, transform.position, transform.rotation);
    //        clone.GetComponent<Rigidbody2D>().AddForce(clone.transform.up * bulletSpeed);

    //        gm.changeResources(-1);
    //        yield return new WaitForSeconds(shootWait);

    //        speed = oldS;

    //        yield return new WaitForSeconds(shootFreq);
    //        shooting = false;
    //    }
    //}

    public void moveToPlanet(GameObject t) {
        float r = t.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        float h = GetComponent<SpriteRenderer>().bounds.size.y * 0.3f;
        transform.position = t.transform.position + new Vector3(0, r + h, 0);
    }

    // Update is called once per frame
    void FixedUpdate() {
        angle += speed;
        angle = Mathf.Clamp(angle, minAngle, maxAngle);

        if (angle >= maxAngle) {
            speed *= -1;
        } else if (angle <= minAngle) {
            speed *= -1;
        }

        rotateTo(angle);
    }

    public void rotateTo(float a) {
        transform.RotateAround(onPlanet.position, new Vector3(0, 0, 1), -transform.eulerAngles.z);
        transform.RotateAround(onPlanet.position, new Vector3(0, 0, 1), a);
    }
}
