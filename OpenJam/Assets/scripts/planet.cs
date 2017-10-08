using UnityEngine;

public class planet : MonoBehaviour {
    public Sprite greenPlanet;
    public gameManager gm;
    public AudioSource hitSound;
    private cannon c;

    void Start() {
        hitSound = GetComponent<AudioSource>();
        c = FindObjectOfType<cannon>();
        gm = FindObjectOfType<gameManager>();
        float d = GetComponent<SpriteRenderer>().bounds.size.x;
        float r = d * 0.5f;
        float margin = 0.2f;
        Debug.Log(gameManager.camLeft);
        float x = Random.Range(gameManager.camLeft + r + margin, gameManager.camRight - (r + margin));

        transform.position = new Vector3(x, transform.position.y);
    }

    void Update() {
        if (transform.position.y < Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y - 1f) {
            gm.spawnPlanet();
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.CompareTag("bullet")) {
            Destroy(GetComponent<CircleCollider2D>());
            GetComponent<SpriteRenderer>().sprite = greenPlanet;
            hitSound.Play();

            gm.changeResources(2);
            gm.changeScore(1);

            Destroy(coll.gameObject, 0.1f);

            c.rotateTo(0);
            c.onPlanet = transform;
            c.moveToPlanet(gameObject);

            Debug.Log(gameManager.bottomCamDist);
            Camera.main.GetComponent<cameraS>().desiredY = c.onPlanet.position.y - gameManager.bottomCamDist + (Camera.main.orthographicSize); //TODO FIX CAMERA MOVEING UP TOO MUCH!
        }
    }
}
