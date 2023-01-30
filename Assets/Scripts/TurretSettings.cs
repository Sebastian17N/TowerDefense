using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Turret Shop Settings")]
public class TurretSettings : ScriptableObject
{
    public GameObject TurretPrefab;
    public int TurretShopCost;
    public Sprite TurretShopSprite;

}
