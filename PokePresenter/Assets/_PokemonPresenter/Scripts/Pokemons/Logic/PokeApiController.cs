using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ModestTree;
using GlassyCode.PokemonPresenter.Core.Logic;
using GlassyCode.PokemonPresenter.Pokemons.Data;
using GlassyCode.PokemonPresenter.Pokemons.Data.Models;

namespace GlassyCode.PokemonPresenter.Pokemons.Logic
{
    public class PokeApiController : MonoBehaviour, IPokeApiController
    {
        private List<PokemonModel> _pokemonModels = new();

        public event Action OnStartDownloadingAllPokemons;
        public event Action<PokemonModel[]> OnFinishDownloadingAllPokemons;
        public event Action<PokemonDetails> OnFinishDownloadingPokemonDetails;
        public event Action<float> OnDownloadingProgress;
        
        private void ProcessPokemonsData(string json)
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
                var nextPageCoroutine = WebExtensions.GetJsonDataAsync(pokemonArray.next, ProcessPokemonsData);
                StartCoroutine(nextPageCoroutine);
            }
            else
            {
                OnFinishDownloadingAllPokemons?.Invoke(_pokemonModels.ToArray());
                _pokemonModels = null;
                OnDownloadingProgress = null;
            }
        }
        
        private void ProcessPokemonDetailsData(string json)
        {
            if (json is null || json.IsEmpty()) return;
            
            var pokemonDetails = JsonUtility.FromJson<PokemonDetails>(json);
            
            StartCoroutine(DownloadSprites(pokemonDetails));
        }
        
        private IEnumerator DownloadSprites(PokemonDetails pokemonDetails)
        {
            var frontDownloaded = false;
            var backDownloaded = false;
            var frontUrl = pokemonDetails.sprites.front_default;
            var backUrl = pokemonDetails.sprites.back_default;

            if (!frontUrl.IsEmpty())
            {
                yield return StartCoroutine(WebExtensions.GetHighQualityTexture2D(pokemonDetails.sprites.front_default, texture =>
                {
                    pokemonDetails.FrontTexture2D = texture;
                    frontDownloaded = true;
                }));
            }
            else
            {
                frontDownloaded = true;
            }
            
            if (!backUrl.IsEmpty())
            {
                yield return StartCoroutine(WebExtensions.GetHighQualityTexture2D(pokemonDetails.sprites.back_default, texture =>
                {
                    pokemonDetails.BackTexture2D = texture;
                    backDownloaded = true;
                }));
            }
            else
            {
                backDownloaded = true;
            }

            yield return new WaitUntil(() => frontDownloaded && backDownloaded);
            OnFinishDownloadingPokemonDetails?.Invoke(pokemonDetails);
        }
        
        public void DownloadPokemons()
        {
            _pokemonModels.Clear();
            OnStartDownloadingAllPokemons?.Invoke();
            const string endpoint = PokeApiConfig.GetPokesEndpoint;
            var coroutine = WebExtensions.GetJsonDataAsync(endpoint, ProcessPokemonsData);
            StartCoroutine(coroutine);
        }
        
        public void DownloadPokemonDetails(string pokemonName)
        {
            var endpoint = PokeApiConfig.GetPokesEndpoint + $"/{pokemonName}";
            var coroutine = WebExtensions.GetJsonDataAsync(endpoint, ProcessPokemonDetailsData);
            StartCoroutine(coroutine);
        }

        public void FindPokemonByName(string pokemonName)
        {
            if (pokemonName.IsEmpty()) return;
            
            DownloadPokemonDetails(pokemonName);
        }
    }
}