using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour {
    public Transform deadPlanet;

    public static float camLeft;
    public static float camRight;
    public static float lastPlanetY;
    public float planetFreq = 5;
    public static float moveUpAmount = 5;
    public static float bottomCamDist;


    public int resources = 3;
    public Text resourcesTxt;
    public int score = 0;
    public Text scoreTxt;

    // Use this for initialization
    void Awake() {
        camLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        camRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
    }

    void Start() {
        score = 0;
        resources = 3;
        changeScore(0);
        changeResources(0);
        for (int i = 0; i < 5; i++) {
            spawnPlanet();
        }
    }

    void Update() {
        if (resources <= 0 && GameObject.FindWithTag("bullet") == null) {
            Debug.Log("YOU LOSE!");
            if (PlayerPrefs.GetInt("highscore", 0) < score) {
                PlayerPrefs.SetInt("highscore", score);
            }
            UnityEngine.SceneManagement.SceneManager.LoadScene("menu");
        }
    }

    public void changeScore(int amount) {
        score += amount;
        scoreTxt.text = "" + score;
    }

    public void changeResources(int amount) {
        resources += amount;
        resourcesTxt.text = "" + resources;
    }

    public void spawnPlanet() {
        float camTop = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;

        float d = deadPlanet.GetComponent<SpriteRenderer>().bounds.size.x * 4.5f;
        float r = d * 0.5f;
        float y = lastPlanetY + planetFreq;

        Transform clone = Instantiate(deadPlanet, new Vector3(0, y, 0), deadPlanet.rotation);

        float max = r;
        float min = r * 0.2f;
        float newR = Mathf.Clamp((score - 0) * (r * min - r) / (25 - 0) + r, min, max);

        clone.localScale = new Vector2(newR, newR);

        lastPlanetY = y;
    }
}
