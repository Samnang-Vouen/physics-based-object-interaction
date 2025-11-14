using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class ClothSystem : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;

    [Header("Visuals")]
    public Color collisionColor = Color.red;
    private Renderer _rend;
    private Color _originalColor;

    private Rigidbody _rb;
    private Vector3 _input;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        // init renderer and cache original color
        _rend = GetComponent<Renderer>();
        if (_rend == null) _rend = GetComponentInChildren<Renderer>();
        if (_rend != null) _originalColor = _rend.material.color;
    }

    void Update()
    {
        // Arrow keys or WASD map to these axes by default
        _input = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            0f,
            Input.GetAxisRaw("Vertical")
        );
        _input = Vector3.ClampMagnitude(_input, 1f);
    }

    void FixedUpdate()
    {
        if (_rb == null) return;
        Vector3 move = _input * moveSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + move);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (_rend == null) return;
        if (!collision.collider.CompareTag("Box")) return;
        _rend.material.color = collisionColor;
    }

    void OnCollisionExit(Collision collision)
    {
        if (_rend == null) return;
        if (!collision.collider.CompareTag("Box")) return;
        _rend.material.color = _originalColor;
    }
}
