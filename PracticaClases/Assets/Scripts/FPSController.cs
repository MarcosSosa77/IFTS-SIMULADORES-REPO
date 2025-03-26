using UnityEngine;

public class FPSController : MonoBehaviour
{
    public float speed = 5f;
    public CharacterController controller;

    void Update()
    {
        MoverJugador();
    }

    void MoverJugador() 
    {
        float movX = Input.GetAxis("Horizontal");
        float movY = Input.GetAxis("Vertical");

        Vector3 movimiento = transform.right * movX + transform.forward * movY;

        controller.Move(movimiento * speed * Time.deltaTime);
       
    }


}
