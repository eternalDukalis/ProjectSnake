using UnityEngine;
using System.Collections.Generic;

public class Field : MonoBehaviour {

    public const int FieldWidth = 9; //Длина поля
    public const int FieldHeight = 9; //Ширина поля
    const int RandomXMargin = 2; //Отступ по оси Х при рандомизации позиции
    const int RandomYMargin = 1; //Отступ по оси Y при рандомизации позиции
    static List<Vector2> OccupiedCells; //Список занятых клеток
	void Start ()
    {
        OccupiedCells = new List<Vector2>(); //Инициализируем список занятых клеток
	}
	
	void Update ()
    {
	
	}

    static public void OccupyCell(Vector2 position) //Метод занятия клетки поля
    {
        if (OccupiedCells == null) //Если список не инициализирован
        {
            Debug.LogError("Попытка обратиться к неинициализированному спискку занятых клеток"); //Выводим ошибку
            return; //Прерываем метод
        }
        if (OccupiedCells.Contains(position)) //Если клетка уже в списке занятых
        {
            Debug.LogError("Попытка занять уже занятую клетку"); //Выводим ошибку
            return; //Прерываем метод
        }
        OccupiedCells.Add(position); //Добавляем клетку в список занятых
    }

    static public void FreeCell(Vector2 position) //Метод освобождения клетки поля
    {
        if (OccupiedCells == null) //Если список не инициализирован
        {
            Debug.LogError("Попытка обратиться к неинициализированному спискку занятых клеток"); //Выводим ошибку
            return; //Прерываем метод
        }
        if (!OccupiedCells.Contains(position)) //Если клетки нет в списке занятых
        {
            Debug.LogError("Попытка освободить незанятую клетку"); //Выводим ошибку
            return; //Прерываем метод
        }
    }

    static public bool CellIsFree(Vector2 position) //Функция, возвращающая, свободна ли клетка
    {
        return !OccupiedCells.Contains(position); //Возвращаем, нет ли клетки в списке занятых
    }

    static public Vector2 RandomPosition(bool withMargin) //Функция расчёт случайной позиции на поле
    {
        int x = Random.Range(RandomXMargin * withMargin.GetHashCode(), FieldWidth - RandomXMargin * withMargin.GetHashCode() - 1); //Рассчитываем координату Х
        int y = Random.Range(RandomYMargin * withMargin.GetHashCode(), FieldHeight - RandomYMargin * withMargin.GetHashCode() - 1); //Рассчитываем координату Y
        return new Vector2(x, y); //Возвращаем результат
    }
}
