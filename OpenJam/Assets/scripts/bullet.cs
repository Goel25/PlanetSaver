using UnityEngine;

public class bullet : MonoBehaviour {
    private Rigidbody2D rb;


    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Vector2 moveDirection = rb.velocity;
        if (moveDirection != Vector2.zero) {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            angle += 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (transform.position.y < Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y - 1f) {
            Destroy(this.gameObject);

        }
    }
}
