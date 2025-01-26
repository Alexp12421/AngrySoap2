using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWater : MonoBehaviour
{
    [SerializeField]
    private float water = 100;

    [SerializeField]
    private bool hasWater = true;

    [SerializeField]
    GameObject moistureBar;

    [SerializeField] private float regenAmount = 0.5f;

    public void AddWater(float amount){
        if(water <= 0){
            hasWater = true;
        }
        water = Mathf.Clamp(water + amount, 0, 100);
        UpdateMoistureBar();
    }

    public bool ConsumeWater(int amount){
        if(water >= amount)
        {
            water -= amount;
            hasWater = water > 0;
            UpdateMoistureBar();
            return true;
        }
        else
        {
            hasWater = false;
            return false;
        }
    }

    private void UpdateMoistureBar(){
        float rotationZ = Mathf.Lerp(-90, -270, water / 100f);
        moistureBar.transform.rotation = Quaternion.Euler(0, 0, rotationZ);
    }

    public float GetWater(){
        return water;
    }

    public bool HasWater(){
        return hasWater;
    }

    private void Update()
    {
        AddWater(regenAmount * Time.deltaTime);
    }
}
