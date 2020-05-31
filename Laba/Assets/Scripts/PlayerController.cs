using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Camera mainCamera;
    public Guy guy;
    public Slider healthBar;
    public Image healthBarFill;
    public Color healedColor;
    public Color deadColor;
    public float cameraDistance = 8f;
    public float mouseSensetivity = 25f;
    public float movementSpeed = 3;
    public float jumpForce = 5;

    private float lookAngle = 0;
    //private Vector3 lastMousePos = Vector3.zero;
    private Vector2 mouseDelta = Vector2.zero;


    // Start is called before the first frame update
    void Start()
    {
        this.guy = this.GetComponent<Guy>();
        //lastMousePos = Input.mousePosition;
    }

    // Update is called once per frame
    void Update()
    {
        bool shot = false;
        bool jump = false;
        Vector2 inputMovement = Vector2.zero;

        if (Input.GetMouseButtonDown(0))
        {
            shot = true;
        }

        if (Input.GetKey(KeyCode.W))
        {
            inputMovement.y += movementSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMovement.x -= movementSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMovement.y -= movementSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMovement.x += movementSpeed;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        //mouseDelta = Input.mousePosition - lastMousePos;
        //lastMousePos = Input.mousePosition;
        mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        this.guy.transform.rotation *= Quaternion.Euler(new Vector3(0, mouseDelta.x * mouseSensetivity * Time.deltaTime, 0));
        this.lookAngle += mouseDelta.y * Time.deltaTime * mouseSensetivity;
        this.lookAngle = Mathf.Clamp(this.lookAngle, -80, 80);
        this.mainCamera.transform.parent.transform.localRotation = Quaternion.AngleAxis(-lookAngle, Vector3.right);

        this.guy.weapon.transform.localRotation = Quaternion.AngleAxis(lookAngle, Vector3.forward);

        this.mainCamera.transform.LookAt(this.mainCamera.transform.parent.transform.position);

        this.guy.transform.position += this.guy.transform.right * inputMovement.x * Time.deltaTime;
        this.guy.transform.position += this.guy.transform.forward * inputMovement.y * Time.deltaTime;
        

        if (jump)
        {
            this.guy.rb.AddForce(this.guy.transform.up * jumpForce);
        }

        if (shot)
        {
            this.guy.weapon.Shot();
        }

        healthBar.value = Mathf.InverseLerp(0, guy.maxHealth, guy.currentHealth);
        healthBarFill.color = Color.Lerp(deadColor, healedColor, healthBar.value);
    }
}
