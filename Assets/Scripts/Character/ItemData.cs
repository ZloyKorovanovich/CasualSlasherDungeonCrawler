using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "Create New ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string _itemName;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private ItemType _itemType;


    public string ItemName => _itemName;
    public GameObject Prefab => _prefab;
    public ItemType ItemType => _itemType;
}
