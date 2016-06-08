using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour {

	void Start ()
    {
        transform.position = new Vector3((float)Field.FieldWidth / 2, transform.position.y, (float)Field.FieldHeight / 2); //Устанавливаем камеру сверху над серединой поля	
	}
	
	void Update ()
    {
	
	}
}
