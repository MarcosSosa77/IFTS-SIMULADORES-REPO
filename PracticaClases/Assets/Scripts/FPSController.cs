using UnityEngine;

public class FPSController : MonoBehaviour
{
    [Header("Movimiento Personaje")]
    public float speed = 5f;
    public CharacterController controller;


    [Header("Movimiento de Camara")]
    public Transform camTransform;
    public float mouseSensitivityX = 2f;
    public float mouseSensitivityY = 2f;
    private float verticalRotation = 0f;

    void Update()
    {
        MoverJugador();
        MoverCamara();
    }

    void MoverJugador() 
    {
        float movX = Input.GetAxis("Horizontal");
        float movY = Input.GetAxis("Vertical");

        Vector3 movimiento = transform.right * movX + transform.forward * movY;

        controller.Move(movimiento * speed * Time.deltaTime);       
    }

    void MoverCamara()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        camTransform.localRotation = Quaternion.Euler(verticalRotation,0,0f);
        transform.Rotate(Vector3.up * mouseX);      
         
    }


}
