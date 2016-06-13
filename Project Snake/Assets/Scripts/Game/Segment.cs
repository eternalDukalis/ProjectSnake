using UnityEngine;
using System.Collections;

public class Segment : MonoBehaviour {

    public float MoveTime = 0.3f; //Время передвижения
    public float Margin = 0.1f; //Отступ
    public Settings.Side Direction; //Направление движения
    public Vector2 Position; //Позиция сегмента
    Rigidbody rigid; //Компонент Rigidbody
    public bool NeedToGrow
    {
        get
        {
            bool res = _grow;
            _grow = false;
            return res;
        }
    } //Нужно ли вырасти
    bool _grow = false; //Нужно ли вырасти
	void Start ()
    {
        rigid = GetComponent<Rigidbody>(); //Находим компонент Rigidbody
	}
	
	void Update ()
    {
        
	}

    public void Place(Vector3 startPosition) //Метод установки сегмента
    {
        Position = new Vector2(startPosition.x, startPosition.z); //Записываем позицию
        Field.OccupyCell(Position); //Занимаем клетку
        transform.position = startPosition + new Vector3((float)Settings.CellSize / 2, 0, (float)Settings.CellSize / 2); //Установка стартовой позиции
        transform.localScale = new Vector3(Settings.CellSize - Margin, Settings.CellSize - Margin, Settings.CellSize - Margin); //Установка размера
    }

    public void Move() //Метод перемещения сегмента
    {
        Field.FreeCell(Position); //Освобождаем клетку
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
        Position += new Vector2(vector.x, vector.z); //Меняем позицию
        Field.OccupyCell(Position); //Занимаем клетку
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
            rigid.velocity = delta; //Устанавливаем скорость в Rigidbody
            yield return null; //Новый кадр
            if ((cur - vector).magnitude < delta.magnitude) //Если шаговое перемещение меньше, чем оставшееся перемещение
                break; //Выходим из цикла
        }
        rigid.velocity = new Vector3(); //Устанавливаем скорость в Rigidbody
        transform.position = oldPosition + vector; //Устанавливаем конечную позицию
    }

    void OnTriggerEnter(Collider collider) //При входе в триггер
    {
        Fruit fruit = collider.gameObject.GetComponent<Fruit>(); //Получаем компонент Fruit объекта
        if (fruit != null) //Если компонент Fruit существует
        {
            fruit.MakeEaten(); //Съедаем фрукт
            _grow = true; //Оставляем сигнал о том, что нужно вырасти
        }
    }
}
