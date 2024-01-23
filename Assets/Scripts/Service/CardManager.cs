using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour, IService
{
    public Transform cardRoot;
    public GameObject player;
    public Action onDeactivate;

    private List<GameObject> _currentCards = new List<GameObject>();

    #region IService
    private void OnEnable()
    {
        ServiceLocator.RegisterService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.UnregisterService<CardManager>();
        StopAllCoroutines();
    }
    #endregion

    private void Awake()
    {
        if (!player)
            player = GameObject.FindGameObjectWithTag("Player");

        onDeactivate += SelectedCard;
    }

    private void Start()
    {
        ServiceLocator.GetService<ControlManager>().SetState("Stuck", false);
    }

    public void SetCards(List<GameObject> cards, bool show)
    {
        DeleteCards();

        foreach(var obj in cards)
        {
            var card = Instantiate(obj, cardRoot);
            _currentCards.Add(card);
            card.GetComponent<Card>()?.Init(this);
        }

        if(show)
            ShowCards();
    }

    public void ShowCards()
    {
        foreach (var card in _currentCards)
            card.SetActive(true);

        var service = ServiceLocator.GetService<ControlManager>();
        service.SetState("IsAttack", false);
        service.SetState("Stuck", true);
    }

    public void HideCards()
    {
        foreach (var card in _currentCards)
            card.SetActive(false);

        var service = ServiceLocator.GetService<ControlManager>();
        service.SetState("IsAttack", false);
        service.SetState("Stuck", false);
    }

    public void DeleteCards()
    {
        HideCards();
        foreach(var card in _currentCards)
            Destroy(card);

        _currentCards.Clear();
    }

    private void SelectedCard()
    {
        StartCoroutine(HideDelay(0.2f));
    }

    private IEnumerator HideDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        DeleteCards();
    }
}
