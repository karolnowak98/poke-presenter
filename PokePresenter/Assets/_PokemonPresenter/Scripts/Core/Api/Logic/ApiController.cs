using System;
using GlassyCode.PokemonPresenter.Scripts.Core.Api.Data;
using GlassyCode.PokemonPresenter.Scripts.Core.Utils;
using ModestTree;
using UnityEngine;
using Zenject;

namespace GlassyCode.PokemonPresenter.Scripts.Core.Api.Logic
{
    public class ApiController : MonoBehaviour
    {
        private ApiConfig _apiConfig;

        public event Action<PokemonData[]> OnPokemonDataProcessed;
        
        [Inject]
        private void Construct(ApiConfig apiConfig)
        {
            _apiConfig = apiConfig;
        }

        [ContextMenu("Download Pokemons")]
        public void DownloadPokemonData()
        {
            const string endpoint = ApiConfig.GetPokesEndpoint;
            var coroutine = WebExtensions.GetJsonDataAsync(endpoint, ProcessPokemonData);
            StartCoroutine(coroutine);
        }
        
        private void ProcessPokemonData(string json)
        {
            if (json == null) return;

            var pokemonData = ParsePokemonJson(json);

            if (pokemonData.IsEmpty())
            {
                Debug.LogWarning("Pokemon array is empty for some reason!");
                return;
            }
            
            OnPokemonDataProcessed?.Invoke(pokemonData);
        }
        
        private static PokemonData[] ParsePokemonJson(string json)
        {
            var pokemonArray = JsonUtility.FromJson<PokemonData[]>(json);
            
            return pokemonArray ?? Array.Empty<PokemonData>();
        }
    }
}