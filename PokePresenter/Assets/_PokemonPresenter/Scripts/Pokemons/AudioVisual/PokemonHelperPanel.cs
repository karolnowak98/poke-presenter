using System;
using GlassyCode.PokemonPresenter.Core.UI;
using GlassyCode.PokemonPresenter.Pokemons.Data.Models;
using GlassyCode.PokemonPresenter.Pokemons.Logic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

namespace GlassyCode.PokemonPresenter.Pokemons.AudioVisual
{
    public class PokemonHelperPanel : Panel
    {
        [SerializeField] private TextMeshProUGUI _pokemonsNumberTmp;
        [SerializeField] private TMP_InputField _pokemonNameField;
        [SerializeField] private Button _downloadPokemonsBtn;
        [SerializeField] private Button _findPokemonBtn;

        private IPokeApiController _pokeApiController;
        
        [Inject]
        private void Construct(IPokeApiController pokeApiController)
        {
            _pokeApiController = pokeApiController;
            
            _pokeApiController.OnFinishDownloadingAllPokemons += UpdatePokemonsNumber;
        }

        private void OnDestroy()
        {
            _pokeApiController.OnFinishDownloadingAllPokemons -= UpdatePokemonsNumber;
        }

        private void OnEnable()
        {
            _downloadPokemonsBtn.onClick.AddListener(_pokeApiController.DownloadPokemons);
            _findPokemonBtn.onClick.AddListener(() => _pokeApiController.FindPokemonByName(_pokemonNameField.text));
        }

        private void UpdatePokemonsNumber(PokemonModel[] pokemonModels)
        {
            _pokemonsNumberTmp.text = $"Pokemons number: {pokemonModels.Length}";
            _pokemonsNumberTmp.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            _downloadPokemonsBtn.onClick.RemoveAllListeners();
            _findPokemonBtn.onClick.RemoveAllListeners();
        }
    }
}