using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance { get; private set; }

    [Header("UI")]
    [SerializeField] private Canvas interactionCanvas;
    [SerializeField] private TMP_Text objectNameText;

    [Header("UI Panels")]
    [SerializeField] private GameObject textureNavigationPanel;



    [Header("Sliders Colores")]
    [SerializeField] private Slider rSlider, gSlider, bSlider;
    [SerializeField] private TMP_Text rValueText, gValueText, bValueText;


    [Header("Tiling Sliders")]
    [SerializeField] private Slider tilingXSlider, tilingYSlider;
    [SerializeField] private TMP_Text tilingXValueText, tilingYValueText;


    [Header("Texture Selection Grid")]
    [SerializeField] private GameObject textureButtonPrefab; // A prefab with Image + Button
    [SerializeField] private Transform textureGridParent;


    private ObjetoInteractuable currentObject;
    public bool isUIOpen { get; private set; } = false;

    void Awake()
    {
        Singleton();

        rSlider.onValueChanged.AddListener(OnSliderValueChanged);
        gSlider.onValueChanged.AddListener(OnSliderValueChanged);
        bSlider.onValueChanged.AddListener(OnSliderValueChanged);

        tilingXSlider.onValueChanged.AddListener(OnTilingSliderChanged);
        tilingYSlider.onValueChanged.AddListener(OnTilingSliderChanged);


          interactionCanvas.gameObject.SetActive(false);


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
        currentObject = target;

        interactionCanvas.gameObject.SetActive(true);
        isUIOpen = true;

        objectNameText.text = currentObject.currentObjectSO.objectName;


        // Show texture panel if the object supports textures
        if (currentObject.currentObjectSO.isTextured && currentObject.currentObjectSO.texturedElements.Count > 0)
        {
            textureNavigationPanel.SetActive(true);
        }
        else
        {
            textureNavigationPanel.SetActive(false);
        }


        // Set tiling values if materials exist
        if (currentObject.editableMaterials != null && currentObject.editableMaterials.Count > 0)
        {
            Vector2 currentTiling = currentObject.editableMaterials[0].mainTextureScale;

            tilingXSlider.SetValueWithoutNotify(currentTiling.x);
            tilingYSlider.SetValueWithoutNotify(currentTiling.y);

            tilingXValueText.text = currentTiling.x.ToString("0.00");
            tilingYValueText.text = currentTiling.y.ToString("0.00");
        }

        PopulateTextureGrid();



        //CAMBIANDO COLORES DEL MATERIAL
        if (currentObject.editableMaterials != null && currentObject.editableMaterials.Count > 0)
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

    public void OnTilingSliderChanged(float value)
    {
        if (currentObject == null) return;

        Vector2 newTiling = new Vector2(tilingXSlider.value, tilingYSlider.value);
        currentObject.SetTiling(newTiling);

        tilingXValueText.text = newTiling.x.ToString("0.00");
        tilingYValueText.text = newTiling.y.ToString("0.00");
    }



    public void HideInteractionCanvas()
    {
        interactionCanvas.transform.SetParent(null);
        interactionCanvas.gameObject.SetActive(false);
        isUIOpen = false;
        objectNameText.text = "";
        currentObject = null;
        textureNavigationPanel.SetActive(false);
    }


    private void PopulateTextureGrid()
    {
        // Clear old buttons
        foreach (Transform child in textureGridParent)
        {
            Destroy(child.gameObject);
        }

        if (currentObject == null || currentObject.currentObjectSO == null || currentObject.currentObjectSO.texturedElements == null)
            return;

        for (int i = 0; i < currentObject.currentObjectSO.texturedElements.Count; i++)
        {
            int index = i; // Needed to avoid closure issue in lambda

            GameObject buttonGO = Instantiate(textureButtonPrefab, textureGridParent);
            RawImage image = buttonGO.GetComponentInChildren<RawImage>();
            image.texture = currentObject.currentObjectSO.texturedElements[i].mainTexture;

            buttonGO.GetComponent<Button>().onClick.AddListener(() =>
            {
                currentObject.SetTextureByIndex(index);
                SoundManager.instance.PlaySound(SoundType.BUTTON_CLICK, 2.0f);
            });
        }
    }



    public void OnSliderValueChanged(float value)
    {
        SoundManager.instance.PlaySound(SoundType.BUTTON_HOVER);

        if (currentObject == null || currentObject.currentMaterials == null) return;

        Color newColor = new Color(rSlider.value, gSlider.value, bSlider.value);

        currentObject.SetColor(newColor); // Let the object apply the color

        // Update RGB text values (0-255)
        rValueText.text = Mathf.RoundToInt(newColor.r * 255).ToString();
        gValueText.text = Mathf.RoundToInt(newColor.g * 255).ToString();
        bValueText.text = Mathf.RoundToInt(newColor.b * 255).ToString();
    }




    public void OnNextTextureClicked()
    {
        if (currentObject != null)
        {
            currentObject.NextTexture();
            SoundManager.instance.PlaySound(SoundType.BUTTON_CLICK, 2.0f);
        }
    }


    public void OnPreviousTextureClicked()
    {
        if (currentObject != null)
        {
            currentObject.PreviousTexture();
            SoundManager.instance.PlaySound(SoundType.BUTTON_CLICK, 2.0f);
        }
    }
}

  
