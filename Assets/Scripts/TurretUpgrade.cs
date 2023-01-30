using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgrade : MonoBehaviour
{
    [SerializeField] private int upgradeInictialCost;
    [SerializeField] private int upgradeCostIncremental;
    [SerializeField] private float damageIncremental;
    [SerializeField] private float delayReduse;

    [Header("Sell")]
    [Range(0, 1)]
    [SerializeField] private float sellPert;
    public float SellPert { get; set; }

    public int UpgradeCost { get; set; }
    public int Level { get; set; }

    private TurretsProjectile _turretsProjectile;
    void Start()
    {
        _turretsProjectile = GetComponent<TurretsProjectile>();
        UpgradeCost = upgradeInictialCost;
        SellPert = sellPert;
        Level = 1;
    }
    public void UpgradeTurret()
    {
        if (CurrencySystem.Instance.TotalCoins >= UpgradeCost)
        {
            _turretsProjectile.Damage += damageIncremental;
            _turretsProjectile.DelayPerShot -= delayReduse;
            UpdateUpgrade();
        }
    }
    public int GetSellValue()
    {
        int sellValue = Mathf.RoundToInt(UpgradeCost * SellPert);
        return sellValue;
    }
    private void UpdateUpgrade()
    {
        CurrencySystem.Instance.RemoveCoins(UpgradeCost);
        UpgradeCost += upgradeCostIncremental;
        Level++;
    }
}
