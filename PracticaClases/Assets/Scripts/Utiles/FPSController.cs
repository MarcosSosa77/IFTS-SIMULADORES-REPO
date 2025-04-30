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


    //DETECTAR MOVIMIENTO DEL TOUCH
    private Vector2 lastTouchPosition;
    private bool isTouching = false;

    void Update()
    {
        MoverJugador();
        MoverCamaraMouse();

        if(Input.touchCount > 0)
        MoverCamaraTouch();
    }

    void MoverJugador() 
    {
        float movX = Input.GetAxis("Horizontal");
        float movY = Input.GetAxis("Vertical");

        Vector3 movimiento = transform.right * movX + transform.forward * movY;

        controller.Move(movimiento * speed * Time.deltaTime);       
    }

    void MoverCamaraMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY;

        AplicarRotation(mouseX,mouseY);
    }

    void MoverCamaraTouch() 
    {
        Touch touch = Input.GetTouch(0);


        //PREGUNTA SI ACABO DE TOCAR LA PANTALLA
        if (touch.phase == TouchPhase.Began)
        {
            lastTouchPosition = touch.position;
            isTouching = true;
            Debug.Log("TAP EN PANTALLA");
        }
        //PREGUNTA SI ESTOY MOVIENDO EL DEDO EN LA PANTALLA
        else if (touch.phase == TouchPhase.Moved && isTouching)
        {
            float posX = touch.deltaPosition.x * mouseSensitivityX * 0.1f;
            float posY = touch.deltaPosition.y * mouseSensitivityY * 0.1f;
           

            AplicarRotation(posX, posY);

        }
        //SI DEJASTE DE TOCAR LA PANTALLA ENTONCES..
        else if (touch.phase == TouchPhase.Ended) 
        {
            isTouching = false;
            Debug.Log("DEJASTE DE TOCAR LA PANTALLA");
        }

    }
    


    void AplicarRotation(float horizontal, float vertical) 
    {
        verticalRotation -= vertical;//INVERTIR ROTACION
        verticalRotation = Mathf.Clamp(verticalRotation,-90,90);
        //Rotacion Vertical
        camTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        //Rotacion Horizontal
        transform.Rotate(Vector3.up * horizontal);
    }

}
