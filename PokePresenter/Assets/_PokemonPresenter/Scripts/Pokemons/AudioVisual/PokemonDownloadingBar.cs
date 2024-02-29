using GlassyCode.PokemonPresenter.Pokemons.Data.Models;
using GlassyCode.PokemonPresenter.Pokemons.Logic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GlassyCode.PokemonPresenter.Pokemons.AudioVisual
{
    public class PokemonDownloadingBar : MonoBehaviour
    {
        [SerializeField] private Slider _downloadingBar;
        
        private IPokeApiController _pokeApiController;

        [Inject]
        private void Construct(IPokeApiController pokeApiController)
        {
            _pokeApiController = pokeApiController;

            _pokeApiController.OnStartDownloadingAllPokemons += ShowDownloadingAllPokemonsBar;
            _pokeApiController.OnFinishDownloadingAllPokemons += HideDownloadingAllPokemonsBar;
            _pokeApiController.OnDownloadingProgress += UpdateDownloadingBar;
        }

        private void OnDestroy()
        {
            _pokeApiController.OnStartDownloadingAllPokemons -= ShowDownloadingAllPokemonsBar;
            _pokeApiController.OnFinishDownloadingAllPokemons -= HideDownloadingAllPokemonsBar;
            _pokeApiController.OnDownloadingProgress -= UpdateDownloadingBar;
        }
        
        private void ShowDownloadingAllPokemonsBar()
        {
            _downloadingBar.gameObject.SetActive(true);
        }

        private void HideDownloadingAllPokemonsBar(PokemonModel[] pokemonData)
        {
            _downloadingBar.gameObject.SetActive(false);
        }
        
        private void UpdateDownloadingBar(float value)
        {
            _downloadingBar.value = value;
        }
    }
}