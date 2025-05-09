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
}
