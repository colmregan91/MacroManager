using System;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using Utils;

namespace Singletons
{
    public class WebRequestManager : MonoSingleton<WebRequestManager>
    {
        private const string ApiUrl = "https://world.openfoodfacts.net//api/v2/product/";
        private const string ImagesEndPoint = "https://images.openfoodfacts.org/images/products/";
    
        private const string UserAgent = "/api/v2/product/"; // Dedicated contact email for the app
        private const int MaxRequestsPerMinute = 100;
        private int requestCount = 0;
        private float requestTimer = 0f;

        void Update()
        {
            // Reset the request count every minute
            requestTimer += Time.deltaTime;
            if (requestTimer >= 60f)
            {
                requestCount = 0;
                requestTimer = 0f;
            }
        }


        public void MakeApiRequest(string serialNumber, System.Action<Food> onSuccess, System.Action<string> onError)
        {
            Debug.Log("req");
            if (requestCount < MaxRequestsPerMinute)
            {
                StartCoroutine(SendRequest(serialNumber, onSuccess, onError));
            }
            else
            {
                onError?.Invoke("Rate limit exceeded. Try again later.");
            }
        }
    
        private void OnDataRecieved(string data, Action<Food> Callback)
        {
            ProductData productData = JsonConvert.DeserializeObject<ProductData>(data);

            StartCoroutine(DownloadImage(productData.product.image_front_small_url, (tex) =>
            {
                
    
                
                Food newFood = new Food()
                {
                    TextureData = TextureUtils.GetDataFromTexture(tex),
                    calories = productData.product.energy_kcal_100g,
                    name = productData.product.product_name,
                    normalPortionSize = 100,
                    carbs = productData.product.carbohydrates_100g,
                    protein = productData.product.proteins_100g,
                    fat = productData.product.fat_100g
                };
                Callback?.Invoke(newFood);
            }));
   
        }

        private IEnumerator SendRequest(string serialNumber, Action<Food> onSuccess, Action<string> onError)
        {
            string url = ApiUrl + serialNumber + "/?fields=product_name,carbohydrates_100g,proteins_100g,fat_100g,energy-kcal_100g,image_front_small_url";
            UnityWebRequest request = UnityWebRequest.Get(url);
            request.SetRequestHeader("User-Agent", UserAgent + "colmregan91@gmail.com");
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                onError?.Invoke(request.error);
            }
            else
            {

     
                Debug.Log(request.downloadHandler.text);
            
                OnDataRecieved(request.downloadHandler.text, food => onSuccess?.Invoke(food));
                requestCount++;
            }
        }


        IEnumerator DownloadImage(string url, Action<Texture2D> callback)
        {
            Debug.Log(url);
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error downloading image: " + www.downloadHandler.error);
            }
            else
            {
                // Get the downloaded texture
                var texture = DownloadHandlerTexture.GetContent(www);

                // Create a sprite from the texture
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

                var tex = sprite.texture;
                callback?.Invoke(tex);
            }
        }
    }
}

