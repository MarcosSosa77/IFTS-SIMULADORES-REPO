using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidadBala = 10f;
    public float vidaBala = 5f;
    public GameObject efectoExplosion;

   

    void OnEnable()
    {
        Invoke("Desactivar", vidaBala);
    }

    void Desactivar() 
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        transform.position += transform.forward * velocidadBala * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
      //  Instantiate(efectoExplosion,collision.transform.position,Quaternion.identity);
    }

}
