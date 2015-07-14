﻿using UnityEngine;
using System.Collections;

class Egg
{
    public string Name;
    public int Number;
    public GameObject EggObject;
    public bool ToDestroy = false;
    public Egg(string name)
    {
        Name = name;
        Number = 1;
        EggObject = GameObject.Find(name + Number);
        EggObject.GetComponent<SpriteRenderer>().enabled = true;
    }
    public void Move()
    {
        EggObject.GetComponent<SpriteRenderer>().enabled = false;
        Number++;
        if (Number == 7)
        {
            EggObject = GameObject.Find(Name + "Broken");
            EggObject.GetComponent<SpriteRenderer>().enabled = true;
            return;
        }
        if (Number == 8 && (Name[0] == 'L' || Name[0] == 'R'))
        {
            if (Name[0] == 'L')
            {
                Number = 1;
                Name = "SmallChickenLeft";
                EggObject = GameObject.Find(Name + Number);
            }
            else if (Name[0] == 'R')
            {
                Number = 1;
                Name = "SmallChickenRight";
                EggObject = GameObject.Find(Name + Number);
            }
        }
        else if (Number >= 5 && (Name[0] == 'S'))
        {
            ToDestroy = true;
            return;
        }
        EggObject = GameObject.Find(Name + Number);
        EggObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}

public class EggsScript : MonoBehaviour
{
    public bool IsActive = false;
    System.Collections.Generic.List<Egg> Eggs;
    private System.Random Rand;
    void Start()
    {
        Eggs = new System.Collections.Generic.List<Egg>();
        for (int i = 1; i <= 6; i++)
        {
            GameObject.Find("LeftDownEgg" + i).GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("LeftUpEgg" + i).GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("RightDownEgg" + i).GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("RightUpEgg" + i).GetComponent<SpriteRenderer>().enabled = false;
            if (i <= 4)
            {
                GameObject.Find("SmallChickenLeft" + i).GetComponent<SpriteRenderer>().enabled = false;
                GameObject.Find("SmallChickenRight" + i).GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        GameObject.Find("LeftDownEggBroken").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("RightDownEggBroken").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("LeftUpEggBroken").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("RightUpEggBroken").GetComponent<SpriteRenderer>().enabled = false;
        Rand = new System.Random();
    }
    void MoveEgg()
    {
        if (Rand.Next(1,3) == 2)
        {
            var rand = Rand.Next(1, 5);
            if (rand == 1)
            {
                Eggs.Add(new Egg("LeftDownEgg"));
                return;
            }
            if (rand == 2)
            {
                Eggs.Add(new Egg("RightDownEgg"));
                return;
            }
            if (rand == 3)
            {
                Eggs.Add(new Egg("LeftUpEgg"));
                return;
            }
            if (rand == 4)
            {
                Eggs.Add(new Egg("RightUpEgg"));
                return;
            }
        }
        foreach (var item in Eggs)
        {
            if (item.ToDestroy)
            {
                Eggs.Remove(item);
            }
        }
        foreach (var item in Eggs)
        {
            item.Move();
        }
    }
    public float OldTime;
    public float Timer = 0;
    public float StartPlayingTime;
    private bool Inited = false;
    private void Init()
    {
        StartPlayingTime = Time.timeSinceLevelLoad;
        OldTime = StartPlayingTime;
    }
    void Update()
    {
        if (IsActive)
        {
            if (!Inited)
            {
                Init();
                Inited = true;
            }
       
            Timer = Time.timeSinceLevelLoad - StartPlayingTime;
            if (Timer - OldTime > 0.3)
            {
                MoveEgg();
                OldTime = Timer;
            }
        }
        else
        {
            Inited = false;
        }
    }
}