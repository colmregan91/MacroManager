
using TMPro;

using UnityEngine;
using  UnityEngine.UI;
using Utils;

public class FoodSelectUI : MonoBehaviour
{

    [SerializeField] private RawImage image;

    [SerializeField] private TextMeshProUGUI text;

    private Food foodAtThisSelection;
    public void Init(Food food)
    {
        foodAtThisSelection = food;

        Texture2D texture2D = TextureUtils.GetTextureFromData(foodAtThisSelection.TextureData);
        image.texture =texture2D ;
        image.material.mainTexture = texture2D;
        text.text =  foodAtThisSelection.name;
    }

    public void HandleSelected()
    {
        AvailableFoodSelection.OnFoodSelected?.Invoke(foodAtThisSelection);
    }

}
