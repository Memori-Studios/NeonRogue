using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberwareManager : MonoBehaviour
{
    public List<CyberwareUpgrade> cyberwareUpgrades = new List<CyberwareUpgrade>();
    Sandevistan sandevistan;
    AngelFire angelFire;
    public GameObject dronePrefab;
    private void Awake()
    {
        sandevistan = GetComponent<Sandevistan>();
        angelFire = GetComponent<AngelFire>();
    }
    public void LoadCyberware(CyberwareUpgrade cyberware)
    {
        cyberwareUpgrades.Add(cyberware);

        if(cyberware.cyberwareType == CyberwareType.Drone)
            Instantiate(dronePrefab, transform.position, Quaternion.identity, null);
    }
    private void Update()
    {
        FireAutoCyberware();
    }
    private void FireAutoCyberware()
    {
        if(PlayerManager.instance.upgradeManager.AngelFireOnline() && !angelFire.angelFireActive)
            StartCoroutine(angelFire.ActivateAngelFire());
    }
    public void HandleCyberware(int cyberwareIndex)
    {
        if(cyberwareIndex > cyberwareUpgrades.Count)
        {
            Debug.Log($"Cyberware index {cyberwareIndex} is out of range");
            return;
        }

        if(cyberwareUpgrades[cyberwareIndex-1].cyberwareType == CyberwareType.Sandevistan)
            HandleSandevistan();
    }
    private void HandleSandevistan()
    {
        if(PlayerManager.instance.upgradeManager.SandevistanOnline() && 
            !sandevistan.sandevistanActive && 
            !sandevistan.OnCoolDown)
            StartCoroutine(sandevistan.ActivateSandy());
    }
    public void OverrideSandy()
    {
        if(
            !sandevistan.sandevistanActive && 
            !sandevistan.OnCoolDown)
            StartCoroutine(sandevistan.ActivateSandy());
    }
}
