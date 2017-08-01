using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {
    [Range(0,100)]
    public float Health = 100;
    [Range(0, 100)]
    public float Energy = 100;
    [Range(0, 100)]
    public float Mass = 100;
    // Gotta maintain that public image    
    public Image HealthBar;
    public Image EnergyBar;
    public Image MassBar;

    public AudioClip hitSound;

    private void Start()
    {
        HealthBar.fillAmount = Health / 100;
        EnergyBar.fillAmount = Energy / 100;
        MassBar.fillAmount = Mass / 100;
    }


    private void Update()
    {
        if(Health <= 0){
            GetComponent<RespawnManager>().Respawn();
        }
    }
    public bool ModifyHealth(float amount)
    {

        if(Health + amount < 0)
        {
            Health = 0;
            return true;
        }

        if (Health + amount > 100)
        {
            Health = 0;
            return true;
        }

        if (Health + amount <= 100 && Health + amount >= 0)
        {
            if(amount < 0)
            {
                GetComponent<AudioSource>().PlayOneShot(hitSound);
            }
            Health += amount;
            HealthBar.fillAmount = Health / 100;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetHealth(float value)
    {
        Health = value;
        HealthBar.fillAmount = Health / 100;
    }

    public bool ModifyEnergy(float amount)
    {

        if (Energy + amount <= 100 && Energy + amount >= 0)
        {
            Energy += amount;
            EnergyBar.fillAmount = Energy / 100;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool ModifyMass(float amount)
    {


        if (Mass + amount <= 100 && Mass + amount >= 0)
        {
            Mass += amount;
            MassBar.fillAmount = Mass / 100;
            return true;
        }
        else
        {
            return false;
        }
    }


}
