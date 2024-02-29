using System.Collections.Generic;
using GlassyCode.PokemonPresenter.Core.UI;
using GlassyCode.PokemonPresenter.Pokemons.Data.Models;
using GlassyCode.PokemonPresenter.Pokemons.Logic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GlassyCode.PokemonPresenter.Pokemons.AudioVisual
{
    public class PokemonGridPanel : Panel
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _contentTransform;
        [SerializeField] private GameObject _viewPrefab;
        [SerializeField] private int _visibleViewsNumber;

        private readonly List<GameObject> _views = new();
        private PokemonModel[] _models = {};
        private int _firstViewsIndex;
        private int _lastViewsIndex;
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
            UpdateViews();
        }

        private void InitModels(PokemonModel[] pokemonModels)
        {
            _models = pokemonModels;

            InitViews();
        }
        
        private void InitViews()
        {
            _firstViewsIndex = 0;
            _lastViewsIndex = _visibleViewsNumber;
            
            var viewsNumber = Mathf.Min(_visibleViewsNumber, _models.Length);
            
            for (var i = 0; i < viewsNumber; i++)
            {
                CreateItemView(_models[i]);
            }
        }

        private void UpdateViews()
        {
            DestroyViews();
            UpdateViewsRange();

            for (var i = _firstViewsIndex; i < _lastViewsIndex; i++)
            {
                CreateItemView(_models[i]);
            }
        }

        private void DestroyViews()
        {
            foreach (var viewGameObject in _views)
            {
                Destroy(viewGameObject);
            }
            
            _views.Clear();
        }
        
        private void UpdateViewsRange()
        {
            var scrollPos = _scrollRect.verticalNormalizedPosition;
            var visibleModelsNumber = CurrentModelsNumber - _visibleViewsNumber;
    
            _firstViewsIndex = Mathf.Clamp((int)((1 - scrollPos) * visibleModelsNumber), 0, Mathf.Max(0, visibleModelsNumber));
            _lastViewsIndex = Mathf.Min(_firstViewsIndex + _visibleViewsNumber, CurrentModelsNumber);
        }

        private void CreateItemView(PokemonModel model)
        {
            var newItem = Instantiate(_viewPrefab, _contentTransform);
            
            if (!newItem.TryGetComponent<PokemonView>(out var itemView))
            {
                Debug.LogError("Failed to get ItemView component on the instantiated prefab.");
                Destroy(newItem);
                return;
            }

            itemView.PokemonNameTmp.text = model.name;
            itemView.OpenDetailsWindowBtn.onClick.AddListener(() => OpenPokemonDetailsWindow(model.name));
            _views.Add(newItem);
        }

        private void OpenPokemonDetailsWindow(string pokemonName)
        {
            _pokeApiController.DownloadPokemonDetails(pokemonName);
        }
    }
}