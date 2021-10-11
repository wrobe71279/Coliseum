using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{

    public int selectedWeapon = 0;
    public GameObject Sword;
    public GameObject Hammer;
    public GameObject Spear;

    /*
    public GameObject ESword;
    public GameObject EHammer;
    public GameObject ESpear;
    */

    /*
    private void Start()
    {
        SelectWeapon();
    }
    
    public void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if(i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
    */

    //Player Weapons
    public void SelectSword()
    {
        selectedWeapon = 0;
        Sword.SetActive(true);
        Hammer.SetActive(false);
        Spear.SetActive(false);

    }
    public void SelectHammer()
    {
        selectedWeapon = 1;
        Sword.SetActive(false);
        Hammer.SetActive(true);
        Spear.SetActive(false);
    }
    public void SelectSpear()
    {
        Sword.SetActive(false);
        Hammer.SetActive(false);
        Spear.SetActive(true);
    }

    public void HideWeapon()
    {
        selectedWeapon = 0;
        Sword.SetActive(false);
        Hammer.SetActive(false);
        Spear.SetActive(false);

    }

    /*
    //Enemy Weapons
    public void EnemySword()
    {
        ESword.SetActive(true);
        EHammer.SetActive(false);
        ESpear.SetActive(false);

    }
    public void EnemyHammer()
    {
        ESword.SetActive(false);
        EHammer.SetActive(true);
        ESpear.SetActive(false);
    }
    public void EnemySpear()
    {
        ESword.SetActive(false);
        EHammer.SetActive(false);
        ESpear.SetActive(true);
    }

    public void HideEnemyWeapon()
    {
        selectedWeapon = 0;
        Sword.SetActive(false);
        Hammer.SetActive(false);
        Spear.SetActive(false);

    }

    */
}
