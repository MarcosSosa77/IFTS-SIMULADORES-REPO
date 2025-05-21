/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ObjetoInteractuable : MonoBehaviour
{
    //public Transform spawnPoint;//punto donde aparecen los objetos

    [Header("SETEADO DE OBJETOS")]
    public ObjSO[] objetos; //lista de objetos scriptable

    public int currentIndex = 0;

    //MATERIALES
  //  [HideInInspector] public GameObject currentInstance;
    [HideInInspector] public Material[] currentMaterials;
    [HideInInspector] public List<Material> editableMaterials = new List<Material>();


  //  public Vector3 offset;

    private ObjSO currentObjectSelected;

    private int currentTextureIndex = 0;


    //public int currentRotationIndex = 0;

    void Start() 
    {
       // this.gameObject.GetComponent<Renderer>().enabled = false;

        SpawnCurrentObject();
    }


    /*
    public void CambiarObjeto(int direccion) 
    {
        if(objetos == null || objetos.Length == 0) return;

        currentIndex += direccion;
        if (currentIndex >= objetos.Length) currentIndex = 0;


        if (currentIndex < 0) currentIndex = objetos.Length -1;

        SpawnCurrentObject();

    }


    public void SpawnCurrentObject()
    {

        //Destruir instancia anterior

        /*if (currentInstance != null)
            Destroy(currentInstance);
      
        currentObjectSelected = objetos[currentIndex];

        Quaternion rotToUse = Quaternion.identity;

        if (currentObjectSelected.rotationVariants != null && currentObjectSelected.rotationVariants.Count > 0)
        {
            // Clamp rotation index
            if (currentRotationIndex >= currentObjectSelected.rotationVariants.Count)
                currentRotationIndex = 0;

            rotToUse = currentObjectSelected.rotationVariants[currentRotationIndex];
        }

        currentInstance = Instantiate(currentObjectSelected.prefab, transform.position, rotToUse);
        AutoAlignToPosition(currentInstance,transform.position,currentObjectSelected.customOffset);     

  



        // Renderer rend = currentInstance.GetComponent<Renderer>();
        Renderer rend = this.gameObject.GetComponent<Renderer>();
        if(rend != null) 
        {
            //currentObjectSelected = obj;
            currentObjectSelected = objetos[currentIndex];
            //CLONAR LOS MATERIALES DEL OBJETO INSTANCIADO
            Material[] originalMats = rend.sharedMaterials;
            currentMaterials = new Material[originalMats.Length];

            for(int i=0; i< originalMats.Length; i++) 
            {
                currentMaterials[i] = originalMats[i] != null ? new Material(originalMats[i]) : null;
               
            }


            //guardar referencias a los materiales originales

            rend.materials = currentMaterials;
            //DETERMINAR QUE MATERIALES QUIERO EXPONER
            editableMaterials.Clear();

            if (currentObjectSelected.customMaterialsToEdit != null && currentObjectSelected.customMaterialsToEdit.Count > 0)
            {
                foreach (Material matToEdit in currentObjectSelected.customMaterialsToEdit)
                {
                    foreach (Material clonado in currentMaterials)
                    {
                        if (clonado.name.StartsWith(matToEdit.name))
                        {
                            editableMaterials.Add(clonado);
                        }
                    }
                }
            }
            else 
            {
                editableMaterials.AddRange(currentMaterials);
            }


            //SISTEMA DE TEXTURAS
            if(currentObjectSelected.isTextured  && currentObjectSelected.texturedMaterial != null && currentObjectSelected.availableTextures.Count > 0) 
            {
                foreach(Material mat in currentMaterials) 
                {
                    if(mat != null && mat.name.StartsWith(currentObjectSelected.texturedMaterial.name)) 
                    {
                        mat.mainTexture = currentObjectSelected.availableTextures[0];
                        break;
                    }
                }
            }

        }
    
    
    }


    /*
    public static void AutoAlignToPosition(GameObject obj, Vector3 targetPos, Vector3 manualOffset)
    {
        Renderer rend = obj.GetComponentInChildren<Renderer>();
        if (rend != null)
        {
            Vector3 offset = obj.transform.position - rend.bounds.center + manualOffset;
            obj.transform.position = targetPos + offset;
        }
    }
    

    public void Interactuar() 
    {
        UIManager.instance.ShowInteractionCanvas(this);
    }

    public void SetTexture(int textureIndex)
    {
        if (!currentObjectSelected.isTextured ||
            currentObjectSelected.availableTextures == null ||
            textureIndex < 0 ||
            textureIndex >= currentObjectSelected.availableTextures.Count) 
        {
            return;
        }

        currentTextureIndex = textureIndex;
        ApplyTextureToTexturedMaterial();
     }

    public void NextTexture() 
    {
        if (!currentObjectSelected.isTextured ||
            currentObjectSelected.availableTextures == null ||
            currentObjectSelected.availableTextures.Count == 0)
            return;

        currentTextureIndex++;
        if(currentTextureIndex >= currentObjectSelected.availableTextures.Count) 
        {
            currentTextureIndex = 0;
        }

        ApplyTextureToTexturedMaterial();
    }

    public void PreviousTexture()
    {
        if (!currentObjectSelected.isTextured ||
            currentObjectSelected.availableTextures == null ||
            currentObjectSelected.availableTextures.Count == 0)
            return;

        currentTextureIndex--;
        if (currentTextureIndex < 0)
        {
            currentTextureIndex = currentObjectSelected.availableTextures.Count - 1;
        }

        ApplyTextureToTexturedMaterial();
    }

    void ApplyTextureToTexturedMaterial() 
    {
        if (!currentObjectSelected.isTextured ||
            currentObjectSelected.availableTextures == null ||
            currentObjectSelected.availableTextures.Count == 0 ||
            currentObjectSelected.texturedMaterial == null)
        {
            return;
        }

        foreach(Material mat in currentMaterials) 
        {
            if(mat != null && mat.name.StartsWith(currentObjectSelected.texturedMaterial.name)) 
            {
                mat.mainTexture = currentObjectSelected.availableTextures[currentTextureIndex];
                break;
            }
        }

    }


}
*/


using UnityEngine;
using System.Collections.Generic;

public class ObjetoInteractuable : MonoBehaviour
{
    [Header("Scriptable Object")]
    public ObjSO currentObjectSO;

    [Header("Runtime")]
    [HideInInspector] public Material[] currentMaterials;
    [HideInInspector] public List<Material> editableMaterials = new List<Material>();
    private int currentTextureIndex = 0;

    void Start()
    {
        SetupMaterials();
    }

    public void Interactuar()
    {
        UIManager.instance.ShowInteractionCanvas(this);
    }

    void SetupMaterials()
    {
        Renderer rend = GetComponent<Renderer>();
        if (!rend || currentObjectSO == null) return;

        // Clone materials to avoid shared asset modification
        Material[] originalMats = rend.sharedMaterials;
        currentMaterials = new Material[originalMats.Length];

        for (int i = 0; i < originalMats.Length; i++)
        {
            currentMaterials[i] = originalMats[i] ? new Material(originalMats[i]) : null;
        }

        rend.materials = currentMaterials;

        // Select editable materials
        editableMaterials.Clear();

        if (currentObjectSO.customMaterialsToEdit != null && currentObjectSO.customMaterialsToEdit.Count > 0)
        {
            foreach (var matToEdit in currentObjectSO.customMaterialsToEdit)
            {
                foreach (var mat in currentMaterials)
                {
                    if (mat != null && mat.name.StartsWith(matToEdit.name))
                    {
                        editableMaterials.Add(mat);
                    }
                }
            }
        }
        else
        {
            editableMaterials.AddRange(currentMaterials); // Edit all if none specified
        }
              

        if (currentObjectSO.isTextured && currentObjectSO.texturedMaterial && currentObjectSO.texturedElements.Count > 0)
        {
            ApplyTexture(currentObjectSO.texturedElements[0]);
        }
    }

    public void SetColor(Color newColor)
    {
        foreach (var mat in editableMaterials)
        {
            if (mat != null)
            {
                mat.color = newColor;
            }
        }
    }

    public void SetTexture(int index)
    {
        if (!IsTextureValid(index)) return;
        currentTextureIndex = index;
        ApplyTexture(currentObjectSO.texturedElements[currentTextureIndex]);
    }

    public void NextTexture()
    {
        if (!IsTextureValid()) return;

        currentTextureIndex++;
        if (currentTextureIndex >= currentObjectSO.texturedElements.Count)
            currentTextureIndex = 0;

        ApplyTexture(currentObjectSO.texturedElements[currentTextureIndex]);

    }

    public void PreviousTexture()
    {
        if (!IsTextureValid()) return;

        currentTextureIndex--;
        if (currentTextureIndex < 0)
            currentTextureIndex = currentObjectSO.texturedElements.Count - 1;

        ApplyTexture(currentObjectSO.texturedElements[currentTextureIndex]);

    }

    private void ApplyTexture(TexturedElement element)
    {
        if (!currentObjectSO.texturedMaterial || element == null) return;

        foreach (var mat in currentMaterials)
        {
            if (mat != null && mat.name.StartsWith(currentObjectSO.texturedMaterial.name))
            {
                mat.mainTexture = element.mainTexture;

                if (element.normalMap != null)
                {
                    mat.EnableKeyword("_NORMALMAP");
                    mat.SetTexture("_BumpMap", element.normalMap);
                }
                break;
            }
        }
    }


    private bool IsTextureValid(int index = -1)
    {
        return currentObjectSO.isTextured &&
               currentObjectSO.texturedMaterial != null &&
               currentObjectSO.texturedElements != null &&
               currentObjectSO.texturedElements.Count > 0 &&
               (index == -1 || (index >= 0 && index < currentObjectSO.texturedElements.Count));
    }

    public void SetTiling(Vector2 newTiling)
    {
        foreach (Material mat in editableMaterials)
        {
            if (mat != null)
                mat.mainTextureScale = newTiling;
        }
    }


    public void SetTextureByIndex(int index)
    {
        if (currentObjectSO == null || currentObjectSO.texturedElements == null || index < 0 || index >= currentObjectSO.texturedElements.Count)
            return;

        TexturedElement selectedElement = currentObjectSO.texturedElements[index];

        foreach (Material mat in editableMaterials)
        {
            if (mat != null)
            {
                mat.mainTexture = selectedElement.mainTexture;

                if (selectedElement.normalMap != null)
                {
                    mat.EnableKeyword("_NORMALMAP");
                    mat.SetTexture("_BumpMap", selectedElement.normalMap);
                }
                else
                {
                    mat.DisableKeyword("_NORMALMAP");
                    mat.SetTexture("_BumpMap", null); // Clear previous bump
                }
            }
        }

        currentTextureIndex = index;
    }


}
