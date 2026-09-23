using UnityEngine;
using UnityEngine.InputSystem;

    [RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        //bool a = true;
        //bool b = false;
        //gameobject.setactive(a && b);
        //Vector2 newPos = transform.position;
        //newPos.x = newPos.x + 5;
        //transform.position = newPos;        
        //transform.position = Vector3.one;

        //Vector2 newPos = transform.position;
        //newPos.x = newPos.x + 5;
        //transform.position = newPos;

        //Debug.Log(newPos.x);
        //Debug.Log(newPos.y);
    }

    void Update()
    {
        moveDirection = Vector2.zero;
        Keyboard keyboard = Keyboard.current;

        if (keyboard == null)
            return;

        if (keyboard.wKey.isPressed)
            moveDirection.y += 1f;
        if (keyboard.sKey.isPressed)
            moveDirection.y -= 1f;
        if (keyboard.aKey.isPressed)
            moveDirection.x -= 1f;
        if (keyboard.dKey.isPressed)
            moveDirection.x += 1f;

        // Keep diagonal movement at the same speed as straight movement.
        moveDirection = moveDirection.normalized;

        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    this.transform.Translate(0, 1, 0);
        //}

    }
    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }
}
