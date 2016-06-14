using UnityEngine;
using System.Collections;

public class Fruit : MonoBehaviour {

    public float Margin = 0.15f; //Отступ
	void Start ()
    {
        Place(Field.RandomPosition(false));
	}
	
	void Update ()
    {
	
	}

    public void Place(Vector3 position) //Функция установки фрукта
    {
        transform.position = position + new Vector3((float)Settings.CellSize / 2, 0, (float)Settings.CellSize / 2); //Установка стартовой позиции
        transform.localScale = new Vector3(Settings.CellSize - Margin, Settings.CellSize - Margin, Settings.CellSize - Margin); //Установка размера
    }

    public void MakeEaten() //Съедение фрукта
    {
        Destroy(this.gameObject); //Разрушаем объект
        Vector3 newPos = new Vector3(); //Позиция нового фрукта
        do
        {
            newPos = Field.RandomPosition(false); //Рассчитываем новую позицию
        } while (!Field.CellIsFree(new Vector2(newPos.x, newPos.y))); //Пока она не станет незанятой змеёй
        GameObject obj = Instantiate(this.gameObject); //Создаём новый объект
        obj.name = "Fruit"; //Переименовываем его
        obj.GetComponent<Fruit>().Place(newPos); //Устанавливаем объект
    }
}
