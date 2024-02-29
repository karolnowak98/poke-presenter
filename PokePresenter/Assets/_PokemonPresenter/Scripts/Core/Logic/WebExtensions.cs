using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace GlassyCode.PokemonPresenter.Core.Logic
{
    public static class WebExtensions
    {
        public static IEnumerator GetJsonDataAsync(string url, Action<string> json)
        {
            using var webRequest = UnityWebRequest.Get(url);
            
            yield return webRequest.SendWebRequest();
        
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning("An error occurred while trying to communicate with the API: " + webRequest.error);
                //Additionally it can show some popup for user.
                json?.Invoke(null);
            }
            else
            {
                var jsonData = webRequest.downloadHandler.text;
                json?.Invoke(jsonData);
            }
        }
        
        public static IEnumerator GetHighQualityTexture2D(string url, Action<Texture2D> onSuccess)
        {
            var request = UnityWebRequestTexture.GetTexture(url);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                var trueColorTexture = new Texture2D(texture.width, texture.height, TextureFormat.ARGB32, true);
                trueColorTexture.SetPixels(texture.GetPixels());
                trueColorTexture.Apply();

                trueColorTexture.filterMode = FilterMode.Trilinear;
                trueColorTexture.anisoLevel = 32;
                trueColorTexture.wrapMode = TextureWrapMode.Clamp;
                trueColorTexture.Apply();

                onSuccess?.Invoke(trueColorTexture);
            }
            else
            {
                Debug.LogError($"Failed to load image. Error: {request.error}");
            }
        }
    }
}