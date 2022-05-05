using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    private float posX=-10, posZ=-2.35f;
    private GameObject conveyerBelt;
    public static List<GameObject> listGameObjects;
    private int count=0;
    void Start()
    {
        /*
        // Crear un objeto desde cero
        GameObject cube=GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.AddComponent<Rigidbody>();
        cube.transform.position=new Vector3(-5, posY, posZ);
        GameObject.Instantiate(cube);
        */
        listGameObjects=new List<GameObject>();
        Random.InitState(42);
        conveyerBelt=GameObject.Find("Conveyer Belt");
        StartCoroutine(InstantiateObject());
    }

    void Update()
    {
        
    }

    Vector3 RandomScale(){
        // Escala entre [1,4]
        return new Vector3(1, 1.0f+(Random.value*Random.Range(1,3)), 1);
    }

    GameObject CreateRandomGameObject(){
        // Creamos un objeto y agregamos/modificamos componentes
        GameObject obj=GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.name="Cube "+(listGameObjects.Count+1);
        obj.AddComponent<Rigidbody>();
        obj.GetComponent<Rigidbody>().useGravity=true;
        obj.AddComponent<Speed>();
        // Establecemos su tamano
        Vector3 scale=RandomScale();
        obj.transform.localScale=scale;
        // Establecemos su posicion
        Vector3 position=new Vector3(posX, conveyerBelt.transform.position.y+(scale.y/2), posZ);
        obj.transform.position=position;
        // Establecemos un color aleatorio
        obj.GetComponent<Renderer>().material.color=new Color32(((byte)Random.Range(0,255)),((byte)Random.Range(0,255)),((byte)Random.Range(0,255)), ((byte)0));
        return obj;
    }

    IEnumerator InstantiateObject(){
        // Solo se instanciaran 10 objetos
        while(count<10){
            count++;
            // Creamos el objeto
            GameObject obj=CreateRandomGameObject();
            // Lo agregamos a la lista de objetos (esto para llevar un control)
            listGameObjects.Add(obj);
            // Esperamos un cierto tiempo antes de instanciar el objeto
            int waitTime=Random.Range(2,3);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
