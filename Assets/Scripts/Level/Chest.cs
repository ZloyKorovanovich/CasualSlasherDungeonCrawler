using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public List<GameObject> cards;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            TriggeredPlayer();
    }

    private void TriggeredPlayer()
    {
        _animator.SetTrigger("open");
        setCards();
        destroyChest();

        void destroyChest()
        {
            Destroy(_animator, 5f);
            Destroy(this);
        }

        void setCards()
        {
            ServiceLocator.GetService<CardManager>().SetCards(cards, true);
        }
    }
}
