using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace GlassyCode.PokemonPresenter.Core.Utils
{
    public static class WebExtensions
    {
        public static IEnumerator GetJsonDataAsync(string endpoint, Action<string> callback)
        {
            using var webRequest = UnityWebRequest.Get(endpoint);
            
            yield return webRequest.SendWebRequest();
        
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("An error occurred while trying to communicate with the API: " + webRequest.error);
                callback?.Invoke(null);
            }
            else
            {
                var jsonData = webRequest.downloadHandler.text;
                callback?.Invoke(jsonData);
            }
        }
    }
}