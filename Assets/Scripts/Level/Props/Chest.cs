using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public List<GameObject> cards;
    public float enemyRadius = 7f;
    public LayerMask characterLayer;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
            TriggeredPlayer();
    }

    private void TriggeredPlayer()
    {
        if (checkEnemies())
            return;

        _animator.SetTrigger("open");
        setCards();
        destroyChest();

        bool checkEnemies()
        {
            var characters = Physics.OverlapSphere(transform.position, enemyRadius, characterLayer);
            var found = false;
            foreach(var character in characters)
            {
                if(character.gameObject.GetComponent<BotInput>())
                    found = true;
            }

            return found;
        }

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
