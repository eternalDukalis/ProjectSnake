﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Snake : MonoBehaviour {

    public GameObject SegmentObject; //Префаб сегмента
    public float MovingInterval = 1; //Интервал между перемещениями
    public Material HeadMaterial; //Материал головы
    public GameObject RestartScreen; //Экран игры заново
    List<Segment> Segments; //Список сегментов
    Segment Head
    {
        get
        {
            if ((Segments == null) || (Segments.Count == 0))
                return null;
            return Segments.ToArray()[0];
        }
    } //Голова
    Settings.Side Direction = Settings.Side.Top; //Направление следующего перемещения
    Vector2 LastPosition; //Предыдущая позиция последнего сегмента
    Settings.Side LastDirection; //Предыдущее направление последнего сегмента
    IEnumerator mainCor; //Корутина
    void Start ()
    {
        Segments = new List<Segment>(); //Инициализируем список сегментов
        Vector3 startPosition = Field.RandomPosition(true); //Получаем стартовую позицию
        Segments.Add(InstSegment(startPosition)); //Помещаем голову
        Segments.ToArray()[0].SetMaterial(HeadMaterial); //Устанавливаем материал голове
        Segments.Add(InstSegment(startPosition - new Vector3(0, 0, 1))); //Помещаем ещё один сегмент
        StartCoroutine(mainCor = movement()); //Начинаем движение
	}

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.RightArrow)) && (Head.Direction != Settings.Side.Left)) //Если нажата стрелка вправо и движение не влево
            Direction = Settings.Side.Right; //Следующее перемещение - вправо
        if ((Input.GetKeyDown(KeyCode.LeftArrow)) && (Head.Direction != Settings.Side.Right)) //Если нажата стрелка влево и движение не вправо
            Direction = Settings.Side.Left; //Следующее перемещение - влево
        if ((Input.GetKeyDown(KeyCode.UpArrow)) && (Head.Direction != Settings.Side.Bottom)) //Если нажата стрелка вверж и движение не вниз
            Direction = Settings.Side.Top; //Следующее перемещение - вверх
        if ((Input.GetKeyDown(KeyCode.DownArrow)) && (Head.Direction != Settings.Side.Top)) //Если нажата стрелка вниз и движение не вверх
            Direction = Settings.Side.Bottom; //Следующее перемещение - вниз
        if (Head.NeedToGrow) //Если нужно вырасти
        {
            Segment seg = InstSegment(new Vector3(LastPosition.x, 0, LastPosition.y)); //Создаём новый сегмент
            seg.Direction = LastDirection; //Устанавливаем ему направление
            Segments.Add(seg); //Добавляем в список
        }
        if (Head.End) //Если конец
        {
            StopCoroutine(mainCor); //Останавливаем корутину
            RestartScreen.SetActive(true); //Показываем экран игры заново
        }
    }

    IEnumerator movement() //Корутина перемещения
    {
        float tm = 0; //Счётчик времени
        while (true) //В цикле
        {
            tm += Time.deltaTime; //Увеличиваем счётчик времени
            if (tm >= MovingInterval) //Если настало время перемещения
            {
                tm -= MovingInterval; //Уменьшаем счётчик времени
                Head.Direction = Direction; //Устанавливаем направление движения головы
                Settings.Side prevDir = Head.Direction; //Направление предыдущего сегмента
                foreach (Segment x in Segments) //Для всех сегментов в списке
                {
                    LastPosition = x.Position; //Записываем позицию
                    LastDirection = x.Direction; //Записываем направление
                    x.Move(); //Перемещаем сегмент
                    Settings.Side cur = x.Direction; //Получаем направление сегмента
                    if (cur != prevDir) //Если направление не совпадает с направлением предыдущего сегмента
                        x.Direction = prevDir; //Устанавливаем сегменту направление предыдущего сегмента
                    prevDir = cur; //Записываем направление текущего сегмента
                }
            }
            yield return null; //Новый кадр
        }
    }

    Segment InstSegment(Vector3 startPosition) //Функция установки сегмента
    {
        GameObject obj = Instantiate(SegmentObject); //Инстанциируем объект
        obj.transform.SetParent(transform, false); //Устанавливаем родительский объект
        obj.GetComponent<Segment>().Place(startPosition, Segments.Count); //Устанавливаем сегмент
        return obj.GetComponent<Segment>(); //Возвращаем компонент Segment
    }
}
