using System;
using System.Collections.Generic;
using GlassyCode.PokemonPresenter.Core.Api.AudioVisual.Models;
using GlassyCode.PokemonPresenter.Core.Api.Data;
using GlassyCode.PokemonPresenter.Core.Utils;
using ModestTree;
using UnityEngine;
using Zenject;

namespace GlassyCode.PokemonPresenter.Core.Api.Logic
{
    public class ApiController : MonoBehaviour
    {
        private ApiConfig _apiConfig;
        private List<PokemonModel> _pokemonModels = new();

        public event Action OnStartDownloading;
        public event Action<PokemonModel[]> OnFinishDownloading;
        public event Action<float> OnDownloadingProgress;
        
        [Inject]
        private void Construct(ApiConfig apiConfig)
        {
            _apiConfig = apiConfig;
        }

        [ContextMenu("Download Pokemons")]
        public void DownloadPokemonData()
        {
            OnStartDownloading?.Invoke();
            const string endpoint = ApiConfig.GetPokesEndpoint;
            var coroutine = WebExtensions.GetJsonDataAsync(endpoint, ProcessPokemonData);
            StartCoroutine(coroutine);
        }
        
        private void ProcessPokemonData(string json)
        {
            if (json is null || json.IsEmpty()) return;

            var pokemonArray = JsonUtility.FromJson<PokemonList>(json);

            if (pokemonArray.results == null)
            {
                Debug.LogWarning("Pokemon list is empty for some reason!");
                return;
            }
            
            _pokemonModels.AddRange(pokemonArray.results);
            var progress = (float) _pokemonModels.Count / pokemonArray.count;
            OnDownloadingProgress?.Invoke(progress);

            if (!string.IsNullOrEmpty(pokemonArray.next))
            {
                var nextPageCoroutine = WebExtensions.GetJsonDataAsync(pokemonArray.next, ProcessPokemonData);
                StartCoroutine(nextPageCoroutine);
            }
            else
            {
                OnFinishDownloading?.Invoke(_pokemonModels.ToArray());
                _pokemonModels = null;
                OnDownloadingProgress = null;
            }
        }
    }
}