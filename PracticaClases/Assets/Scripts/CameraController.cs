using UnityEngine;


public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 200f;
    public float minVerticalAngle = -80f;
    public float maxVerticalAngle = 80f;


    private Vector2 startTouchPosition;
    private bool isDragging = false;
    private float currentRotationX = 0f;


    void Update()
    {
        //MOBILE INPUT
        if(Input.touchCount > 0) 
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
                isDragging = false;
            } else if (touch.phase == TouchPhase.Moved) 
            {
                isDragging = true;
                RotarCamara(touch.deltaPosition);
            }
            
        }


        //DETECTAR EL INPUT DE PC
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
            isDragging = true;
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
           // isDragging = true;
            RotarCamara((Vector2)Input.mousePosition - startTouchPosition);
        }
        else if(Input.GetMouseButtonUp(0)) 
        {
            isDragging = false;
        }

    }


    void RotarCamara(Vector2 delta)
    {
        float rotX = delta.x * rotationSpeed * Time.deltaTime;
        float rotY = delta.y * rotationSpeed * Time.deltaTime;

        //ROTAR DE FORMA HORIZONTAL (EJE Y)
        transform.Rotate(Vector3.up, -rotX, Space.World);

        //CLAMPEAR LA ROTACION EN EL EJE X (VERTICAL)
        currentRotationX -= rotY;
        currentRotationX = Mathf.Clamp(currentRotationX, minVerticalAngle, maxVerticalAngle);

        transform.localRotation = Quaternion.Euler(-currentRotationX, transform.eulerAngles.y,0);
    
    }


}
