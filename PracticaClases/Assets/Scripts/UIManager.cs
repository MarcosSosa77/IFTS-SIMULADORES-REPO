using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    [Header("UI")]
    [SerializeField] private Canvas interactionCanvas;
    [SerializeField] private TMP_Text objectNameText;


    [Header("Sliders Colores")]
    [SerializeField] private Slider rSlider, gSlider, bSlider;
    [SerializeField] private TMP_Text rValueText, gValueText, bValueText;


    private ObjetoInteractuable currentObject;
    public bool isUIOpen { get; private set; } = false;

    void Awake()
    {
        Singleton();

        rSlider.onValueChanged.AddListener(OnSliderValueChanged);
        gSlider.onValueChanged.AddListener(OnSliderValueChanged);
        bSlider.onValueChanged.AddListener(OnSliderValueChanged);

      //  interactionCanvas.gameObject.SetActive(false);


    }

    void Singleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }

    public void ShowInteractionCanvas(ObjetoInteractuable target)
    {
     //   interactionCanvas.transform.SetParent(target.gameObject.transform);
      //  interactionCanvas.transform.position = target.gameObject.transform.position + new Vector3(1f,2f,-1f);
        //  interactionCanvas.transform.rotation = Quaternion.LookRotation(Camera.main.transform.position + target.gameObject.transform.position);
        //interactionCanvas.transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.back, Camera.main.transform.rotation * Vector3.up);

        
        currentObject = target;

        interactionCanvas.gameObject.SetActive(true);
        isUIOpen = true;
        /*
        //MOSTRANDO INFO NOMBRE
        objectNameText.text = "Interactuando con: " + target.objectName;

        */

        //CAMBIANDO COLORES DEL MATERIAL
       if(currentObject.editableMaterials != null && currentObject.editableMaterials.Count > 0)
        {

            Color currentColor = currentObject.editableMaterials[0].color;

            rSlider.SetValueWithoutNotify(currentColor.r);
            gSlider.SetValueWithoutNotify(currentColor.g);
            bSlider.SetValueWithoutNotify(currentColor.b);

            //MOSTRAR LOS VALORES DEL RGB EN TEXTO EN PANTALLA (0-255)
            rValueText.text = Mathf.RoundToInt(currentColor.r * 255).ToString();
            gValueText.text = Mathf.RoundToInt(currentColor.g * 255).ToString();
            bValueText.text = Mathf.RoundToInt(currentColor.b * 255).ToString();
            
        }



    }


    public void HideInteractionCanvas() 
    {
        interactionCanvas.transform.SetParent(null);


        interactionCanvas.gameObject.SetActive(false);
        isUIOpen = false;

        objectNameText.text = "";
        currentObject = null;
    }


    public void OnSliderValueChanged(float value) 
    {
        if (currentObject == null || currentObject.currentMaterials == null) return;

        Color newColor = new Color(rSlider.value, gSlider.value, bSlider.value);

        foreach(Material mat in currentObject.editableMaterials) 
        {
            if (mat != null)
                mat.color = newColor; ;
        }
    }




  

    public void SiguienteObjeto() => currentObject?.CambiarObjeto(1);

    public void AnteriorObjeto() => currentObject?.CambiarObjeto(-1);
}
