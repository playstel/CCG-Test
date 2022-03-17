using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerCardsSpawner : MonoBehaviour
{
    [Header("Setup")]
    public GameObject cardPrefab;
    public Transform cardHolder;
    
    [Header("Count")]
    public int maxCards = 4;
    public int minCards = 6;

    [Header("Loading")] 
    public bool downloadArt;
    public Slider loadingSlider;
    public GameObject randomizeButton;
    
    private async void Start()
    {
        if(downloadArt) await DownloadCardArt();
        await CreatePlayerCards();
        randomizeButton.SetActive(true);
    }

    private async UniTask DownloadCardArt()
    {
        var availableCards = GetCardsInfo();
        
        loadingSlider.gameObject.SetActive(true);
        loadingSlider.maxValue = availableCards.Length;

        for (var i = 0; i < availableCards.Length; i++)
        {
            await availableCards[i].GetArtTexture();
            loadingSlider.value += 1;
        }
        
        loadingSlider.gameObject.SetActive(false);
    }

    private async UniTask CreatePlayerCards()
    {
        PlayerCards.RemoveExistingCards();

        var randomCards = GetRandomCardsInfo();

        for (var i = 0; i < randomCards.Count; i++)
        {
            var card = GetNewCard().GetComponent<Card>();

            if (!card)
            {
                Debug.LogError("Failed to find component Card in cardObject");
                Destroy(card.gameObject);
                return;
            }

            card.InitializeCard(randomCards[i]);
            await card.StartingPunch();
        }
    }

    private List<CardInfo> GetRandomCardsInfo()
    {
        var availableCards = GetCardsInfo();
        var pickedCards = new List<CardInfo>();
        var cardsInHand = Random.Range(minCards, maxCards);
        
        for (var i = 0; i < cardsInHand; i++)
        {
            var randomCard = UnityEngine.Random.Range(0, availableCards.Length);
            pickedCards.Add(availableCards[randomCard]);
        }

        return pickedCards;
    }

    private const string infoResourcesPath = "Info";
    private CardInfo[] GetCardsInfo()
    {
        return Resources.LoadAll<CardInfo>(infoResourcesPath);
        
        //I usually use the PlayFab server instead of "Resources.LoadAll"
        //to send JSON data here to deserialize it.
    }

    private GameObject GetNewCard()
    {
        return Instantiate(cardPrefab, cardHolder);
        
        //I usually use Addressables & UniTask instead of all "Instantiate" methods.
        //Example: Addressables.InstantiateAsync(asset, parent); when asset is IResourceLocation
    }
}
