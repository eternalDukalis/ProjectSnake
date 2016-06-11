using UnityEngine;
using System.Collections;

public class Segment : MonoBehaviour {

    public float MoveTime = 0.3f; //Время передвижения
    public float Margin = 0.1f; //Отступ
    public Settings.Side Direction; //Направление движения
	void Start ()
    {
        
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Move();
	}

    public void Place(Vector3 startPosition) //Метод установки сегмента
    {
        transform.position = startPosition + new Vector3((float)Settings.CellSize / 2, 0, (float)Settings.CellSize / 2); //Установка стартовой позиции
        transform.localScale = new Vector3(Settings.CellSize - Margin, Settings.CellSize - Margin, Settings.CellSize - Margin); //Установка размера
    }

    public void Move() //Метод перемещения сегмента
    {
        Vector3 vector = new Vector3(); //Вектор перемещения
        switch (Direction) //В зависимости от направления
        {
            case Settings.Side.Left:
                vector = new Vector3(-Settings.CellSize, 0, 0);
                break;
            case Settings.Side.Right:
                vector = new Vector3(Settings.CellSize, 0, 0);
                break;
            case Settings.Side.Bottom:
                vector = new Vector3(0, 0, -Settings.CellSize);
                break;
            case Settings.Side.Top:
                vector = new Vector3(0, 0, Settings.CellSize);
                break;
        } //Определяем вектор перемещения
        StartCoroutine(moving(vector)); //Стартуем корутину
    }

    IEnumerator moving(Vector3 vector) //Корутина перемещения сегмента
    {
        Vector3 cur = new Vector3(); //Текущее перемещение
        Vector3 oldPosition = transform.position; //Старая позиция
        while (true) //В цикле
        {
            Vector3 delta = vector * Time.deltaTime / MoveTime; //Определяем перемещение для текущего шага
            cur += delta; //Прибавляем к текущему перемещению
            transform.position = oldPosition + cur * cur.magnitude / vector.magnitude; //Вычисляем новую позицию объекта
            yield return null; //Новый кадр
            if ((cur - vector).magnitude < delta.magnitude) //Если шаговое перемещение меньше, чем оставшееся перемещение
                break; //Выходим из цикла
        }
        transform.position = oldPosition + vector; //Устанавливаем конечную позицию
    }
}
