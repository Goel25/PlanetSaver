using UnityEngine;

public class repeat : MonoBehaviour {
    public Transform neighbor;
    private float height;
    public float yVarient = 2f;
    public float widthDivisor = 2.5f;

    void Start() {
        height = GetComponent<SpriteRenderer>().sprite.bounds.size.x * (GetComponent<SpriteRenderer>().size.y / widthDivisor);
    }

    // Update is called once per frame
    void Update() {
        //float camBottom = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;
        //float top = GetComponent<SpriteRenderer>().sprite.bounds.max.y;
        //if (top < camBottom) transform.position = new Vector2(transform.position.x, transform.position.y + 50);


        float top = transform.position.x + (height / 2);
        float camBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;

        if (top < camBottom - 0.1f) {
            float y = transform.position.y + (25.43f * 2);   //neighbor.transform.position.y + (height / 2);
            float x = (yVarient > 0f) ? Random.Range(-yVarient, yVarient) : transform.position.y;

            transform.position = new Vector2(x, y);
            //}
        }
    }
}