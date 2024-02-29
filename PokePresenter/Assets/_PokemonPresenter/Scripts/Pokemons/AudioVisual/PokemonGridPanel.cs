using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using GlassyCode.PokemonPresenter.Core.UI;
using GlassyCode.PokemonPresenter.Pokemons.Data.Models;
using GlassyCode.PokemonPresenter.Pokemons.Logic;

namespace GlassyCode.PokemonPresenter.Pokemons.AudioVisual
{
    public class PokemonGridPanel : Panel
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _contentTransform;
        [SerializeField] private GameObject _viewPrefab;
        [SerializeField, Min(0)] private int _visibleViewsNumber;

        private readonly List<GameObject> _views = new();
        private PokemonModel[] _models = {};
        private int _firstVisibleViewIndex;
        private int _lastVisibleViewsIndex;
        private IPokeApiController _pokeApiController;

        private int CurrentModelsNumber => _models.Length;

        [Inject]
        private void Construct(IPokeApiController pokeApiController)
        {
            _pokeApiController = pokeApiController;

            _pokeApiController.OnFinishDownloadingAllPokemons += InitModels;
        }
        
        private void OnDestroy()
        {
            _pokeApiController.OnFinishDownloadingAllPokemons -= InitModels;
        }

        private void OnEnable()
        {
            _scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
        }

        private void OnDisable()
        {
            _scrollRect.onValueChanged.RemoveAllListeners();
        }

        private void OnScrollValueChanged(Vector2 value)
        {
            UpdatePokemonViews();
        }

        private void InitModels(PokemonModel[] pokemonModels)
        {
            _models = pokemonModels;

            CreatePokemonViews();
        }
        
        private void CreatePokemonViews()
        {
            _firstVisibleViewIndex = 0;
            _lastVisibleViewsIndex = Mathf.Min(_visibleViewsNumber, CurrentModelsNumber);
            
            for (var i = _firstVisibleViewIndex; i < _lastVisibleViewsIndex; i++)
            {
                CreatePokemonView(_models[i]);
            }
        }

        private void UpdatePokemonViews()
        {
            UpdateViewsRange();

            var viewIndex = 0;

            for (var i = _firstVisibleViewIndex; i < _lastVisibleViewsIndex; i++)
            {
                if (!IsViewCreated(viewIndex))
                {
                    CreatePokemonView(_models[i]);
                }
                
                UpdateView(_views[viewIndex], _models[i]);
                viewIndex++;
            }
        }

        private void UpdateView(GameObject viewGameObject, PokemonModel model)
        {
            if (!viewGameObject.TryGetComponent<PokemonView>(out var pokemonView))
            {
                Debug.LogError("Failed to get PokemonView component on the instantiated prefab.");
                Destroy(viewGameObject);
                return;
            }

            pokemonView.OpenDetailsWindowBtn.onClick.RemoveAllListeners();
            model.NameToShow = model.name.FirstCharacterToUpper().Replace("-", " ");
            pokemonView.PokemonNameTmp.text = model.NameToShow;
            pokemonView.OpenDetailsWindowBtn.onClick.AddListener(() => OpenPokemonDetailsWindow(model.name));
        }

        private bool IsViewCreated(int viewIndex)
        {
            return _views.Count > viewIndex;
        }
        
        private void UpdateViewsRange()
        {
            var scrollPos = _scrollRect.verticalNormalizedPosition;
            var remainingModels = CurrentModelsNumber - _visibleViewsNumber;
    
            _firstVisibleViewIndex = Mathf.Clamp((int)((1 - scrollPos) * remainingModels), 0, Mathf.Max(0, remainingModels));
            _lastVisibleViewsIndex = Mathf.Min(_firstVisibleViewIndex + _visibleViewsNumber, CurrentModelsNumber);
        }

        private void CreatePokemonView(PokemonModel model)
        {
            var newPokemonView = Instantiate(_viewPrefab, _contentTransform);
            
            if (!newPokemonView.TryGetComponent<PokemonView>(out var pokemonView))
            {
                Debug.LogError("Failed to get PokemonView component on the instantiated prefab.");
                Destroy(newPokemonView);
                return;
            }

            model.NameToShow = model.name.FirstCharacterToUpper().Replace("-", " ");
            pokemonView.PokemonNameTmp.text = model.NameToShow;
            pokemonView.OpenDetailsWindowBtn.onClick.AddListener(() => OpenPokemonDetailsWindow(model.name));
            _views.Add(newPokemonView);
        }

        private void OpenPokemonDetailsWindow(string pokemonName)
        {
            _pokeApiController.DownloadPokemonDetails(pokemonName);
        }
    }
}