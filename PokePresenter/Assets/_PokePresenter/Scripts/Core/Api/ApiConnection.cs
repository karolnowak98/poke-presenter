using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

namespace GlassyCode.PokePresenter.Core.Api
{
    public class ApiConnection : MonoBehaviour
    {
        private ApiConfig _apiConfig;
        
        [Inject]
        private void Construct(ApiConfig apiConfig)
        {
            _apiConfig = apiConfig;
        }

        public void Start()
        {
            Debug.Log("la");
            StartCoroutine(GetPokemonDataAsync(HandleJsonData));
        }
        
        private static void HandleJsonData(string json)
        {
            if (json == null) return;
        
            Debug.Log(json);
        }
        
        private static IEnumerator GetPokemonDataAsync(Action<string> callback)
        {
            using var webRequest = UnityWebRequest.Get(ApiConfig.GetPokesEndpoint);
            
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