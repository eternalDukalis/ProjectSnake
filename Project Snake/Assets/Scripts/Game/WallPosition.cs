using UnityEngine;
using System.Collections;

public class WallPosition : MonoBehaviour {

    public Settings.Side WallsSide; //Сторона стены
	void Start ()
    {
        SetPosition(); //Устанавливаем позицию
	}
	
	void Update ()
    {
	
	}

    void SetPosition() //Метод установки позиции
    {
        switch (WallsSide) //В зависимости от стороны стены
        {
            case Settings.Side.Bottom:
                transform.position = new Vector3((float)Field.FieldWidth / 2, transform.position.y, -(float)Settings.CellSize / 2);
                transform.localScale = new Vector3(Field.FieldWidth, transform.localScale.y, Settings.CellSize);
                break;
            case Settings.Side.Top:
                transform.position = new Vector3((float)Field.FieldWidth / 2, transform.position.y, Field.FieldHeight + (float)Settings.CellSize / 2);
                transform.localScale = new Vector3(Field.FieldWidth, transform.localScale.y, Settings.CellSize);
                break;
            case Settings.Side.Left:
                transform.position = new Vector3(-(float)Settings.CellSize / 2, transform.position.y, (float)Field.FieldHeight / 2);
                transform.localScale = new Vector3(Settings.CellSize, transform.localScale.y, Field.FieldHeight);
                break;
            case Settings.Side.Right:
                transform.position = new Vector3(Field.FieldWidth + (float)Settings.CellSize / 2, transform.position.y, (float)Field.FieldHeight / 2);
                transform.localScale = new Vector3(Settings.CellSize, transform.localScale.y, Field.FieldHeight);
                break;
        } //Меняем позицию и масштабируем объект
    }
}
