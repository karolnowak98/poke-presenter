using System.Collections.Generic;
using GlassyCode.PokemonPresenter.Scripts.Core.Api.AudioVisual.Models;
using GlassyCode.PokemonPresenter.Scripts.Core.Api.Logic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Utils_Canvas = GlassyCode.PokemonPresenter.Scripts.Core.Utils.Canvas;

namespace GlassyCode.PokemonPresenter.Scripts.Core.Api.AudioVisual
{
    public class PokemonCanvas : Utils_Canvas
    {
        [SerializeField] private PokemonDetailsWindow _pokemonDetailsWindow;
        [SerializeField] private PokemonGridPanel _pokemonGridPanel;
        [SerializeField] private Slider _downloadingBar;
        
        private ApiController _apiController;

        [Inject]
        private void Construct(ApiController apiController)
        {
            _apiController = apiController;

            _apiController.OnStartDownloading += ShowDownloadingBar;
            _apiController.OnFinishDownloading += HideDownloadingBar;
            _apiController.OnDownloadingProgress += UpdateDownloadingBar;
        }

        private void OnDestroy()
        {
            _apiController.OnStartDownloading -= ShowDownloadingBar;
            _apiController.OnFinishDownloading -= HideDownloadingBar;
            _apiController.OnDownloadingProgress -= UpdateDownloadingBar;
        }
        
        private void ShowDownloadingBar()
        {
            _downloadingBar.gameObject.SetActive(true);
        }

        private void HideDownloadingBar(List<PokemonModel> pokemonData)
        {
            _downloadingBar.gameObject.SetActive(false);
        }
        
        private void UpdateDownloadingBar(float value)
        {
            _downloadingBar.value = value;
        }
    }
}