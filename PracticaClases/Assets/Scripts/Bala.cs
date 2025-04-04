using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidadBala = 10f;
    public float vidaBala = 5f;
    public GameObject efectoExplosion;

   

    void OnEnable()
    {
        // Cancel the pending Desactivar call
        CancelInvoke();
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
        // Disable the bullet
        gameObject.SetActive(false);
       

        GameObject efecto = ObjectPooler.SharedInstance.GetPooledObject(ObjectType.efectoSangre);
        efecto.transform.position = collision.contacts[0].point;
        efecto.transform.rotation = Quaternion.LookRotation(collision.contacts[0].normal);
        efecto.SetActive(true);

        SoundManager.instance.PlaySound(SoundType.IMPACTO_1, 1f);

      
    }

}
