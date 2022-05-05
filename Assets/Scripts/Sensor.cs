using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Robotics.ROSTCPConnector; // Del script "ROSConnection"
using RosMessageTypes.ROSMessages; // De los scripts ubicados en "Assets/ROSMessages"

public class Sensor : MonoBehaviour
{
    private string serviceName="/height_test";
    private ROSConnection ros;
    private GameObject conveyerBelt, sensor;
    private Text sensorReading;
    private int cont=0;

    void Start()
    {
        // Creamos una instancia de conexion con ROS, ya que es lo que devuelve esta propiedad.
        ros = ROSConnection.GetOrCreateInstance();
        /*
        Registramos un servicio llamado "height_test" el cual se encuentra ejecutandose en ROS y
        estara capturando las solicitudes en su funcion de devolucion de llamada y devolvera 
        una respuesta.
        */
        ros.RegisterRosService<HeightTestRequest, HeightTestResponse>(serviceName);
        conveyerBelt=GameObject.Find("Conveyer Belt");
        sensor=GameObject.Find("Sensor");
        sensorReading=GameObject.Find("Sensor Reading").GetComponent<Text>();
    }

    void Update()
    {
        if(SpawnObjects.listGameObjects.Count>cont){
            StartCoroutine(HeightCube(cont++));
        }
    }

    IEnumerator HeightCube(int i){
        GameObject obj=SpawnObjects.listGameObjects[i];
        while(obj.transform.position.x<0) {
            yield return null;
        }
        float posYConveyerBelt=conveyerBelt.transform.localPosition.y;
        float posYObject=obj.transform.localPosition.y;
        float scaleYObject=obj.transform.localScale.y;
        float heightCube=(posYObject-posYConveyerBelt)+(scaleYObject/2);
        float heightSensor=sensor.gameObject.transform.localPosition.y;
        float distance=heightSensor-(heightCube+posYConveyerBelt);
        //Debug.Log("Distance between the sensor and the cube: "+distance.ToString());
        //Debug.Log("Height cube: "+heightCube.ToString());

        HeightTestRequest heightTestRequest=new HeightTestRequest(
            height_cube_mts: heightCube
        );

        ros.SendServiceMessage<HeightTestResponse>(serviceName, heightTestRequest, (HeightTestResponse heightTestResponse) => {
            StartCoroutine(ShowSensorReading(obj.name, heightCube, heightTestResponse.result));
            if(heightTestResponse.result==true){
                // Si pasa la prueba de altura entonces continua
                obj.AddComponent<MoveToPosition>();
                WaitForAction.namesObjects.Add(obj.name);
            }else{
                // Si no pasa la prueba de altura entonces se elimina el cubo
                obj.AddComponent<DeleteOutOfRange>();
            }
        });
    }

    IEnumerator ShowSensorReading(string nameObject, float heightCube, bool result){
        sensorReading.text="LECTURA SENSOR\n";
        sensorReading.text+="Altura del cubo (mts): "+heightCube.ToString()+"\n";
        sensorReading.text+=nameObject+": "+result;
        yield return new WaitForSeconds(1);
        sensorReading.text="";
    }
}
