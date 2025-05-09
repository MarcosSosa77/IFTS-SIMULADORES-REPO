using UnityEngine;
using UnityEngine.UI;

public class SelectorObjetos : MonoBehaviour
{
    [Header("Tiempo requerido para detectar INTERACCION")]
    public float holdTimeThreshold = 0.1f;//TIEMPO REQUERIDO PARA DETECTAR EL CLICK MANTENIDO

    [Header("MOVIMIENTO MAXIMO para detectar INTERACCION")]
    public float maxHoldMovement = 15f;

    [Header("CAPAS CON LAS QUE PODEMOS INTERACTUAR")]
    public LayerMask interactableLayer;//IDENTIFICAR LAS LAYER O CAPAS QUE SON INT


    private Vector2 startTouchPosition;
    private float holdTimer = 0f; // temporizador para mantener apretando algo
    private bool isHolding = false;

    //PARA LA UI (VER DESPUES)
    //  public Text objectName;



    void Update()
    {
        if (UIManager.instance.isUIOpen)
            return;
       
        //MOBILE INPUT
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                StartHoldDetection(touch.position);
            } else if (touch.phase == TouchPhase.Moved)
            {
                CheckHoldMovement(touch.position);
            } else if (touch.phase == TouchPhase.Stationary)
            {
                UpdateHoldTimer(touch.position);
            } else if (touch.phase == TouchPhase.Ended)
            {
                isHolding = false;
            }
        }

        //PC INPUT
        if (Input.GetMouseButtonDown(0)) //HAGO CLICK
        {
            StartHoldDetection(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))  // MANTENGO EL CLICK APRETADO
        {
            CheckHoldMovement(Input.mousePosition);
            UpdateHoldTimer(Input.mousePosition);
        } 
        else if (Input.GetMouseButtonUp(0)) //levanto el click
        {
            isHolding = false;
        }
    }




    void StartHoldDetection(Vector2 pos) 
    {
        startTouchPosition = pos;
        holdTimer = 0f;
        isHolding = true;
    }

    void CheckHoldMovement(Vector2 pos) 
    {
        if (Vector2.Distance(startTouchPosition, pos) > maxHoldMovement) 
        {
            isHolding = false;//CANCELA EL HOLD SI EL DEDO SE MUEVE DEMASIADO LEJOS
        }
        
    }

    //ACTUALIZA EL TEMPORIZADOR SI MANTENEMOS APRETANDO EL DEDO SOBRE UN OBJETO
    void UpdateHoldTimer(Vector2 pos) 
    {
        if (!isHolding) return;

        holdTimer += Time.deltaTime;
        if(holdTimer > holdTimeThreshold) 
        {
            DetectarObjecto(pos);
            isHolding = false;
        }
    }


    void DetectarObjecto(Vector2 screenPos) 
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, interactableLayer)) 
        {
            ObjetoInteractuable objeto = hit.collider.GetComponent<ObjetoInteractuable>();

            if(objeto != null)
            {
                objeto.Interactuar();
            }

        } 
    }

}
