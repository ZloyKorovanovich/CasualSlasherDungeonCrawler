using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnGround : MonoBehaviour
{
    [SerializeField] private ItemData _itemData;

    public ItemData PickUpItem()
    {
        return _itemData;
    }
}
