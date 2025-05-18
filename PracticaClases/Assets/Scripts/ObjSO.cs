using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(fileName ="NewObjSO", menuName ="Interactable/Object")]
public class ObjSO : ScriptableObject
{
    public string objectName;
    public GameObject prefab;

    public List<Material> customMaterialsToEdit;//SI ESTA LISTA TIENE ALGUN MATERIAL 
                                                // EL SISTEMA ME VA A EDITAR ESE MATERIAL EXCLUSIVO
                                                //SI LA LISTA DE MATERIALES ESTA VACIA (ME VA A EDITAR TODOS LOS MATERIALES QUE ENCUENTRE)



    public bool isTextured = false; //determinar si el objeto utilza al menos una textura
    public Material texturedMaterial; // material que va a recibir la texture
    public List<Texture> availableTextures; //listado de texturas disponibles
    public Quaternion customRot;
    public Vector3 customScale = new Vector3(1,1,1);

}
