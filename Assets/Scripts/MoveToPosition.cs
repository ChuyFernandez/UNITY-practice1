using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector; // Del script "ROSConnection"
using RosMessageTypes.ROSMessages; // De los scripts ubicados en "Assets/ROSMessages"
using RosMessageTypes.Geometry;

public class MoveToPosition : MonoBehaviour
{
    private bool onCollisionEnter=false;
    private ROSConnection ros;
    private Positions positions;
    void Start()
    {
        // Obtenemos el componente "Positions"
        positions=GameObject.Find("Positions").GetComponent<Positions>();
    }

    void Update()
    {

    }

    // Detectamos la colision del objeto con el suelo
    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.name=="Plane" && !onCollisionEnter){
            onCollisionEnter=true;
            // Destruimos el componente de velocidad (para que no se siga moviendo)
            Destroy(this.gameObject.GetComponent<Speed>());
            Vector2 vec=Positions.getPosFP();
            Vector3 pos=positions.firstPosition[((int)vec.x), ((int)vec.y)];
            // Establecemos una posicion a nivel del suelo para el cubo
            pos.y=this.gameObject.transform.localScale.y/2;
            // Movemos el cubo a su posicion de partida (cada cubo tiene una posicion)
            this.gameObject.transform.position=pos;
            // Establecemos una rotacion al cubo
            this.gameObject.transform.rotation=Quaternion.Euler(0,0,0);
            // Agregamos un componente que llama a un servicio que a su vez llama al servidor de accion
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait(){
        string nameObject=this.gameObject.name;
        // Esperamos hasta que el servidor de accion se haya desocupado y sea el turno del objeto
        while(WaitForAction.actionServerIsBusy || nameObject!=WaitForAction.namesObjects[WaitForAction.turn]) {
            yield return null;
        }
        WaitForAction.actionServerIsBusy=true;
        WaitForAction.turn++;
        this.gameObject.AddComponent<CallMoveCubeAS>();
    }
}
