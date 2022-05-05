using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Positions : MonoBehaviour
{
    // Plano X: [-19, 79]
    // Plano Y: [-9, 9]
    // Es posible trabajar con arrays (1D) y matrices 2D y 3D
    public const int numRows=3, numColumns=15;
    public Vector3[,] firstPosition=new Vector3[numRows, numColumns];
    public Vector3[,] secondPosition=new Vector3[numRows, numColumns];
    public static int contRowFP=0, contColumnFP=0, contRowSP=0, contColumnSP=0;
    void Start()
    {
        /*
        Creamos posiciones en el mundo de Unity
            - Primera posicion
                Es la posicion donde va a parar por primera vez el objeto una vez que
                pasa la prueba de altura.
            - Segunda posicion
                Es la posicion objetivo donde tiene que parar dicho objeto, cuyo trabajo
                lo hace el servidor de accion, el cual se esta ejecutando en ROS.
        */
        Vector3 vecFP=new Vector3(34, 0, 9);
        Vector3 vecSP=new Vector3(75, 0, 9);
        float padding=1+0.2f;
        for(var i=0; i<numRows; i++){
            for(var j=0; j<numColumns; j++){
                firstPosition[i, j]=vecFP;
                vecFP.z-=padding;
                secondPosition[i, j]=vecSP;
                vecSP.z-=padding;
            }
            vecFP.x-=padding;
            vecFP.z=9.0f;
            vecSP.x-=padding;
            vecSP.z=9.0f;
        }
    }

    void Update()
    {
        
    }

    // Devolvemos la siguiente posicion (primera posicion)
    public static Vector2 getPosFP(){
        if(contRowFP==numRows-1 && contColumnFP==numColumns-1) {
            contRowFP=0; contColumnFP=0;
        }else if(contColumnFP==numColumns-1) {
            contRowFP++; contColumnFP=0;
        }
        Vector2 vec=new Vector2(contRowFP, contColumnFP);
        contColumnFP++;
        return vec;
    }

    // Devolvemos la siguiente posicion (segunda posicion)
    public static Vector2 getPosSP(){
        if(contRowSP==numRows-1 && contColumnSP==numColumns-1) {
            contRowSP=0; contColumnSP=0;
        }else if(contColumnSP==numColumns-1) {
            contRowSP++; contColumnSP=0;
        }
        Vector2 vec=new Vector2(contRowSP, contColumnSP);
        contColumnSP++;
        return vec;
    }
}
