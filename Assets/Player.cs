using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpSpeedMult = 5f;
    public float limitSpeed = 3.75f;
    public float mouseSensivity = 3f;
    public bool invertMouse;
    public string collidingTagName = "block";

    Camera mainCamera;
    Rigidbody rigidBody;
    SphereCollider sphereCollider;
    ColliderSetup collSetup;
    RandomAction randomAction;
    Renderer rendererBall;
    Vector2 movement, mouseLook;
    Vector3 initialPosition;

    float distance = 5f;

    //NEVER USE CONSTANTS! USE READONLY INSTEAD
    readonly float cameraAngleLimitYMin;
    readonly float cameraAngleLimitYMax = 75;

    void ColorChange() {
        rendererBall.material.color = UnityEngine.Random.ColorHSV();
    }

    void Start()
    {
        mainCamera = Camera.main;
        rigidBody = gameObject.GetComponent<Rigidbody>();
        rendererBall = gameObject.GetComponent<Renderer>();

        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.material.bounciness = 0.3f;
        sphereCollider.material.bounceCombine = PhysicMaterialCombine.Maximum;
        collSetup = sphereCollider.gameObject.AddComponent<ColliderSetup>();

        initialPosition = transform.position;

        randomAction = RandomAction.CreateInstance(gameObject, 120, 300);
        randomAction.ActionAdd(ColorChange);
    }

    void FixedUpdate()
    {
        ClampVelocity();
        Move();
        CallJump();
        CallResetPosition();
    }

    void Update() { }

    void LateUpdate()
    {
        CameraWork();
        MenuCall();
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider coll = collision.contacts[0].otherCollider;
        if (coll.gameObject.tag == collidingTagName)
        {
            var renderer = coll.gameObject.GetComponent<Renderer>();
            renderer.material.color = rendererBall.material.color;
        }
    }

    void MenuCall()
    {
        if (Input.GetKeyDown(KeyCode.Escape) | Input.GetKeyDown(KeyCode.JoystickButton7))
            MenuCanvas.Getinstance().ChangeState();
    }

    void Move()
    {
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Vector3 front = new Vector3(mainCamera.transform.forward.x, 0, mainCamera.transform.forward.z);
        Vector3 side = new Vector3(mainCamera.transform.right.x, 0, mainCamera.transform.right.z);
        //Vector3 direction = (front + side).normalized;

        //rigidBody.AddForce((front * movement.y) + (side * movement.x) * moveSpeed);
        //rigidBody.AddForce((mainCamera.transform.forward * movement.y) + (mainCamera.transform.right * movement.x) * moveSpeed);

        //Vector3 direction = (movement.y * front.normalized/*mainCamera.transform.forward*/) + (movement.x * side.normalized/*mainCamera.transform.right*/);
        Vector3 direction = ((movement.y * front) + (movement.x * side)).normalized;
        if (CheckVelocity() && collSetup.IsColliding)
            rigidBody.AddForce(direction * moveSpeed * movement.magnitude);
    }

    void CameraWork()
    {
        if (Input.GetMouseButton(0) | Input.GetAxis("ThumbRX") != 0 | Input.GetAxis("ThumbRY") != 0)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            mouseLook += new Vector2(Input.GetAxis("Mouse X") * mouseSensivity, (invertMouse ? Input.GetAxis("Mouse Y") : Input.GetAxis("Mouse Y") * -1) * mouseSensivity);
            mouseLook.y = Mathf.Clamp(mouseLook.y, cameraAngleLimitYMin, cameraAngleLimitYMax);
            //TODO: Make collision detection instead of clamping
        }

        if (Input.GetMouseButton(1))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            distance += Input.GetAxis("Mouse X") * Time.deltaTime * 10f;
            distance = Mathf.Clamp(distance, 2f, 5f);
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Vector3 direction = new Vector3(0f, 0f, -distance);
        Quaternion rotation = Quaternion.Euler(mouseLook.y, mouseLook.x, 0f);
        mainCamera.transform.position = transform.position + rotation * direction;
        mainCamera.transform.LookAt(transform.position);
    }

    void CallResetPosition()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) | Input.GetKeyDown(KeyCode.JoystickButton5))//using literal keycode rn bcuz fuck input managers and stuff
            ResetPosition();
    }

    void ResetPosition()
    {
        rigidBody.Sleep();
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        transform.position = initialPosition;
        rigidBody.WakeUp();
    }

    void CallJump()
    {
        if ((Input.GetKeyDown(KeyCode.Space) | Input.GetKeyDown(KeyCode.JoystickButton0)) && collSetup.IsColliding)
            Jump();
    }

    void Jump()
    {
        if (Math.Abs(rigidBody.velocity.y) < 1f)
            rigidBody.AddForce(new Vector3(0f, 50f * jumpSpeedMult/* * moveSpeed*/, 0f)/* * moveSpeed*/);
    }

    bool CheckVelocity() {
        return rigidBody.velocity.x < limitSpeed | rigidBody.velocity.z < limitSpeed;
    }

    void ClampVelocity()
    {
        var x = Mathf.Clamp(rigidBody.velocity.x, limitSpeed * -1, limitSpeed);
        var y = Mathf.Clamp(rigidBody.velocity.y, limitSpeed * -1, limitSpeed);
        var z = Mathf.Clamp(rigidBody.velocity.z, limitSpeed * -1, limitSpeed);
        rigidBody.velocity = new Vector3(x, y, z);
    }
}
