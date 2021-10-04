using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtons : MonoBehaviour
{

    public GameObject ActionPanel;
    public GameObject WeaponPanel;
    
    public void AttackButton()
    {
        //insert code here
        Debug.Log("Attack chosen");
    }

    public void DefendButton()
    {
        //insert code here
        Debug.Log("Defend chosen");
    }

    public void SwitchButton()
    {
        ActionPanel.SetActive(false);
        WeaponPanel.SetActive(true);
    }

    public void Return()
    {
        ActionPanel.SetActive(true);
        WeaponPanel.SetActive(false);
    }

    public void HammerButton()
    {
        //Debug.Log("Hammer chosen");
        Return();
    }

    public void SwordButton()
    {
        //Debug.Log("Sword chosen");
        Return();
    }

    public void SpearButton()
    {
        //Debug.Log("Spear chosen");
        Return();
    }
}
