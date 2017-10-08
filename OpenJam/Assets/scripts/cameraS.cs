using UnityEngine;

public class cameraS : MonoBehaviour {
    [Range(0.001f, 1)]
    public float moveSpeed = 0.5f;
    public float desiredY;

    void Start() {
        desiredY = transform.position.y;
    }

    void Update() {
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, desiredY, moveSpeed), transform.position.z);
    }
}
