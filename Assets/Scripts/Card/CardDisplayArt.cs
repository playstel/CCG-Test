using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class CardDisplayArt : MonoBehaviour
{
    public void GetArtSprite(CardInfo info)
    {
        if(info.Texture == null) return;
        GetComponent<RawImage>().texture = info.Texture;
    }
}
