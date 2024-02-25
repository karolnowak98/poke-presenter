using System.Collections.Generic;
using GlassyCode.PokemonPresenter.Scripts.Core.Api.AudioVisual.Models;
using GlassyCode.PokemonPresenter.Scripts.Core.Api.Logic;
using GlassyCode.PokemonPresenter.Scripts.Core.Utils;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GlassyCode.PokemonPresenter.Scripts.Core.Api.AudioVisual
{
    public class PokemonGridPanel : Panel
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _contentTransform;
        [SerializeField] private GameObject _viewPrefab;
        [SerializeField] private int _visibleViewsNumber;
        
        private List<PokemonModel> _models = new();
        private readonly List<GameObject> _views = new();
        private int _firstViewsIndex;
        private int _lastViewsIndex;
        private ApiController _apiController;

        public int CurrentModelsNumber => _models.Count;

        [Inject]
        private void Construct(ApiController apiController)
        {
            _apiController = apiController;

            _apiController.OnFinishDownloading += InitModels;
        }

        private void OnDestroy()
        {
            _apiController.OnFinishDownloading -= InitModels;
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

        private void InitModels(List<PokemonModel> pokemonModels)
        {
            _models = pokemonModels;

            InitViews();
        }
        
        private void InitViews()
        {
            _firstViewsIndex = 0;
            _lastViewsIndex = _visibleViewsNumber;
            
            var viewsNumber = Mathf.Min(_visibleViewsNumber, _models.Count);
            
            for (var i = 0; i < viewsNumber; i++)
            {
                CreateItemView(_models[i], i);
            }
        }

        private void UpdateViews()
        {
            DestroyViews();
            UpdateViewsRange();

            for (var i = _firstViewsIndex; i < _lastViewsIndex; i++)
            {
                CreateItemView(_models[i], i);
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

        private void CreateItemView(PokemonModel model, int index)
        {
            var newItem = Instantiate(_viewPrefab, _contentTransform);
            
            if (!newItem.TryGetComponent<PokemonView>(out var itemView))
            {
                Debug.LogError("Failed to get ItemView component on the instantiated prefab.");
                Destroy(newItem);
                return;
            }

            itemView.PokemonNameTmp.text = model.name;
            itemView.OpenDetailsWindowBtn.onClick.AddListener(() => OpenPokemonDetailsWindow(index));
            _views.Add(newItem);
        }

        private void OpenPokemonDetailsWindow(int index)
        {
            Debug.Log($"Open window for {index}");
        }
    }
}