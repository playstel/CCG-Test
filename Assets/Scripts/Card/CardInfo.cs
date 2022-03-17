using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(menuName = "Card/Info")]
public class CardInfo : ScriptableObject
{
    public string _cardName, _cardDescription;
    public int _cardManaValueMax, _cardHealthValueMax, _cardAttackValueMax;
    
    [HideInInspector]
    public Texture2D Texture;
    
    private const string artURL = "https://picsum.photos/200";

    public async UniTask GetArtTexture()
    {
        Texture = await DownloadImageAsync(artURL);
    }
    
    private async UniTask<Texture2D> DownloadImageAsync(string imageUrl)
    {
        using var request = UnityWebRequestTexture.GetTexture(imageUrl);

        await request.SendWebRequest();

        return request.result == UnityWebRequest.Result.Success
            ? DownloadHandlerTexture.GetContent(request)
            : null;
    }
}
