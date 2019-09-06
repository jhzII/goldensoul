﻿using System;
using UnityEngine;
using System.Collections;

public class comment1 : MonoBehaviour
{
    public bool Stay = false; //стоять во время диалога.
    public static bool IsLock = true;
    public string tagg = "Player";
    public int type; // ТИП Диалога
    public int[] mas = new int[10]; // нумерация строк из массива в диалоге
    public int[] masav = new int[10];
    public  bool checkcomm = true; // Проверка для одноразовости диалога
    public float t; // Время до удаления диалога
    private Transform playerPos;
    private Collider2D col;
    private int k1;

    void Awake() //Вход в скрипт
    {
        col = GetComponent<Collider2D>();
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (Stay)
        {
            moveScript.enable(false);
            StartCoroutine(wait(mas.Length, t));
        }
        if (!IsLock) return;
        switch (type)
        {
            case 1: //простой комментарий или неактиваруемый диалог
                if (col.CompareTag(tagg) && checkcomm == true)
                {
                    StartCoroutine(Dialog.Dialogue(Dialog.masDial[mas[0]], masav[0], 0.05f, t)); 
                    checkcomm = false;
                }
            break;
            case 2: //вечный комментарий активируемый
                if (col.CompareTag(tagg) && (Input.GetKeyDown(KeyCode.E) || moveScript.activate))
                {
                    StartCoroutine(Dialog.Dialogue(Dialog.masDial[mas[0]], masav[0], 0.05f, t)); 
                }
                break;
            case 3: //Диалог наступательный удаляется
                if (col.CompareTag(tagg) && checkcomm == true)
                {
                    StartCoroutine(Dialog.Dialogue3(Dialog.masDial,masav,mas,0.05f,t));
                    checkcomm = false;
                }
            break;

            case 4: //Титры
                if (col.CompareTag(tagg) && checkcomm == true)
                {
                    StartCoroutine(Dialog.Titres(Dialog.masDial[mas[0]], 0.05f, t));
                    checkcomm = false;
                }
            break;

            case 5: //Диалог активируемый удаляется
                if (col.CompareTag(tagg) && checkcomm == true )
                {
                    if (Input.GetKeyDown(KeyCode.E) || moveScript.activate)
                    {
                    StartCoroutine(Dialog.Dialogue3(Dialog.masDial, masav, mas, 0.05f, t));
                    checkcomm = false;
                    }
                }
            break;
            case 6: //Диалог активируемый НЕ удаляется
                if (col.CompareTag(tagg) && checkcomm == true)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        StartCoroutine(Dialog.Dialogue3(Dialog.masDial, masav, mas, 0.05f, t));
                    }
                }
                break;
        }
        moveScript.activate = false;
    }
    IEnumerator wait(float x, float y)
    {
        yield return new WaitForSeconds((x + 0.5f) * y);
        moveScript.enable(true);
    }
}
