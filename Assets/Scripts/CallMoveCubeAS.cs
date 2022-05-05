using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Robotics.ROSTCPConnector; // Del script "ROSConnection"
using RosMessageTypes.ROSMessages; // De los scripts ubicados en "Assets/ROSMessages"
using RosMessageTypes.Geometry;

public class CallMoveCubeAS : MonoBehaviour
{
    private string serviceNameToCall="/call_move_cube_as";
    private string topicNameFeedback="/move_cube/feedback";
    private string topicNameResult="/move_cube/result";
    private ROSConnection ros;
    private Positions positions;
    private Text actionInformation;
    public bool result=false;
    void Start()
    {
        // Creamos una instancia de conexion con ROS, ya que es lo que devuelve esta propiedad.
        ros = ROSConnection.GetOrCreateInstance();
        /*
        Registramos un servicio llamado "call_move_cube_as" el cual se encuentra ejecutandose en ROS y
        estara capturando las solicitudes en su funcion de devolucion de llamada y devolvera 
        una respuesta.
        */
        ros.RegisterRosService<CallMoveCubeASRequest, CallMoveCubeASResponse>(serviceNameToCall);
        // Obtenemos el componente "Positions"
        positions=GameObject.Find("Positions").GetComponent<Positions>();
        actionInformation=GameObject.Find("Action Information").GetComponent<Text>();
        /*
        Llamamos al servicio para que llame al servidor de acciones en ROS.
            - Solicitud: Le pasamos la  posicion actual y la posicion objetivo (a la que se tiene que dirigir el cubo).
            - Respuesta: Nos devuelve un booleano y un mensaje de confirmacion de que se ha llamado la accion.
        */
        SendServiceMessage();
    }

    void Update()
    {
        
    }

    void SendServiceMessage(){
        Vector2 vec=Positions.getPosSP();
        Vector3 pos=positions.secondPosition[((int)vec.x), ((int)vec.y)];
        CallMoveCubeASRequest callMoveCubeASRequest=new CallMoveCubeASRequest(
            new PoseOriginTargetMsg(
                pose_origin: new PoseMsg(
                    position: new PointMsg(
                        x: this.gameObject.transform.position.x,
                        y: this.gameObject.transform.position.y,
                        z: this.gameObject.transform.position.z
                    ),
                    orientation: new QuaternionMsg()
                ),
                pose_target: new PoseMsg(
                    position: new PointMsg(
                        x: pos.x,
                        y: pos.y,
                        z: pos.z
                    ),
                    orientation: new QuaternionMsg()
                )
            )
        );
        ros.SendServiceMessage<CallMoveCubeASResponse>(serviceNameToCall, callMoveCubeASRequest, CallbackCallMoveCubeAS);
    }

    private void CallbackCallMoveCubeAS(CallMoveCubeASResponse callMoveCubeASResponse){
        // Si se llamo correctamenta al servidor de accion entonces...
        if(callMoveCubeASResponse.success){
            Debug.Log(callMoveCubeASResponse.message);
            // Nos suscribimos al tema del "/move_cube/feedback" donde publica la accion una vez obtenida una respuesta.
            ros.Subscribe<MoveCubeActionFeedbackMsg>(topicNameFeedback, CallbackInfoFeedbackMoveCubeAS);
            // Nos suscribimos al tema del "/move_cube/result" donde publica la accion una vez finalizada la accion.
            ros.Subscribe<MoveCubeActionResultMsg>(topicNameResult, CallbackInfoResultMoveCubeAS);
        }
    }
    
    private void CallbackInfoFeedbackMoveCubeAS(MoveCubeActionFeedbackMsg moveCubeActionFeedbackMsg){
        if(!result){
            Vector3 posObj=new Vector3(
                (float)moveCubeActionFeedbackMsg.feedback.current_pose.position.x,
                (float)moveCubeActionFeedbackMsg.feedback.current_pose.position.y,
                (float)moveCubeActionFeedbackMsg.feedback.current_pose.position.z
            );
            actionInformation.text="RETROALIMENTACION\n";
            actionInformation.text+="Objeto: "+this.gameObject.name+"\n";
            actionInformation.text+="Coordenadas:";
            actionInformation.text+="\n   x: "+posObj.x;
            actionInformation.text+="\n   y: "+posObj.y;
            actionInformation.text+="\n   z: "+posObj.z;
            this.gameObject.transform.position=new Vector3(posObj.x,posObj.y,posObj.z);
        }
    }

    private void CallbackInfoResultMoveCubeAS(MoveCubeActionResultMsg moveCubeActionResultMsg){
        if(!result){
            Debug.Log(moveCubeActionResultMsg.result.result_message);
            actionInformation.text="";
            result=true;
            WaitForAction.actionServerIsBusy=false;
        }
    }
}
