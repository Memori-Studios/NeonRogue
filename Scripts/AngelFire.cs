using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelFire : MonoBehaviour
{
    [SerializeField] float angelFireDamage = 1f;
    [SerializeField] GameObject angelFirePrefab;
    public bool angelFireActive = false;
    public IEnumerator ActivateAngelFire()
    {
        angelFireActive = true;
        PlayerManager.instance.uIManager.RechargeCyberUpgradeUI(CyberwareType.AngelFire);

        Enemy highestHealthEnemy = GetEnemyWithHighestHealth();
        if(highestHealthEnemy==null)
            yield break;

        Debug.Log($"Highest health enemy is {highestHealthEnemy.name}");
        AngelFireObject angelFireObject = Instantiate(angelFirePrefab, highestHealthEnemy.transform.position, Quaternion.identity, highestHealthEnemy.transform).GetComponent<AngelFireObject>();
        GameObject angelFireLoaded = angelFireObject.SetUpAngelFire((int)angelFireDamage, 2f);
        while(angelFireLoaded!=null)
            yield return null;

        angelFireActive = false;
    }
    public Enemy GetEnemyWithHighestHealth()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Enemy highestHealthEnemy = null;
        for (int i = 0; i < enemies.Length; i++)
        {
            if(highestHealthEnemy==null || enemies[i].Health > highestHealthEnemy.Health)
                highestHealthEnemy = enemies[i];
        }
        return highestHealthEnemy;
    }
}
