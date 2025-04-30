using UnityEngine;

public class ManagerJuego : MonoBehaviour
{
    public static ManagerJuego instancia;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(this);
        }
    }


    public void HolaMundo() 
    {
        Debug.Log("HOLA MUNDO");
    }

    public void ChauMundo()
    {
        Debug.Log("CHAU CHAU CHAU MUNDO");
    }



}
