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
        selectedWeapon = 2;
        Sword.SetActive(false);
        Hammer.SetActive(false);
        Spear.SetActive(true);
    }

}
