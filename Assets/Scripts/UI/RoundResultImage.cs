using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundResultImage : MonoBehaviour
{
    [SerializeField] Sprite futureRoundImage;
    [SerializeField] Sprite currentRoundImage;
    [SerializeField] Sprite lostRoundImage;
    [SerializeField] Sprite wonRoundImage;

    Image _image;

    Sprite[] _roundResultImages;
    
    readonly Dictionary<string, int> _imageDict = new()
    {
        {"future", 0},
        {"current", 1},
        {"lost", 2},
        {"won", 3}
    };

    void Awake()
    {
        _roundResultImages = new[] {futureRoundImage, currentRoundImage, lostRoundImage, wonRoundImage};
        
        _image = GetComponent<Image>();
    }

    public void SetRoundResultImage(string imageType)
    {
        _image.sprite = _roundResultImages[_imageDict[imageType]];
    }
}
