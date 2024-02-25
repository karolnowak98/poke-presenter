using GlassyCode.PokemonPresenter.Scripts.Core.Api.Data;
using GlassyCode.PokemonPresenter.Scripts.Core.Api.Logic;
using UnityEngine;
using Zenject;
using Utils_Canvas = GlassyCode.PokemonPresenter.Scripts.Core.Utils.Canvas;

namespace GlassyCode.PokemonPresenter.Scripts.Core.Api.AudioVisual
{
    public class PokemonCanvas : Utils_Canvas
    {
        [SerializeField] private PokemonDetailsWindow _pokemonDetailsWindow;
        [SerializeField] private PokemonGridPanel _pokemonGridPanel;
        
        private ApiController _apiController;

        [Inject]
        private void Construct(ApiController apiController)
        {
            _apiController = apiController;

            _apiController.OnPokemonDataProcessed += UpdatePokemonGrid;
        }

        private void OnDestroy()
        {
            _apiController.OnPokemonDataProcessed -= UpdatePokemonGrid;
        }

        private void UpdatePokemonGrid(PokemonData[] pokemonData)
        {
            
        }

        private void ResetPokemonGrid()
        {
            
        }
    }
}