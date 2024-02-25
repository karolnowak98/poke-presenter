using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GlassyCode.PokemonPresenter.Scripts.Core.Utils
{
    public class GlassyScrollView : MonoBehaviour
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private RectTransform _contentTransform;
        [SerializeField] private GameObject _viewPrefab;
        [SerializeField] private int _modelsNumber;
        [SerializeField] private int _visibleViewsNumber;
        
        private readonly List<ItemModel> _models = new();
        private readonly List<GameObject> _views = new();
        private int _firstViewsIndex;
        private int _lastViewsIndex;

        public int CurrentModelsNumber => _models.Count;
        
        private void Awake() => InitModels();
        private void OnEnable() =>_scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
        private void Start() => InitViews();
        private void OnDisable() => _scrollRect.onValueChanged.RemoveAllListeners();
        
        private void OnScrollValueChanged(Vector2 value) => UpdateViews();
        
        private void InitModels()
        {
            for (var i = 0; i < _modelsNumber; i++)
            {
                _models.Add(new ItemModel { Label = $"Item {i + 1}", IsChecked = false });
            }
        }
        
        private void InitViews()
        {
            _firstViewsIndex = 0;
            _lastViewsIndex = _visibleViewsNumber;
            
            var viewsNumber = Mathf.Min(_visibleViewsNumber, _modelsNumber);
            
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

        private void CreateItemView(ItemModel model, int index)
        {
            var newItem = Instantiate(_viewPrefab, _contentTransform);
            
            if (!newItem.TryGetComponent<PokemonView>(out var itemView))
            {
                Debug.LogError("Failed to get ItemView component on the instantiated prefab.");
                Destroy(newItem);
                return;
            }

            itemView.PokemonNameTmp.text = model.Label;
            _views.Add(newItem);
        }
        
        private void UpdateToggleState(int index, bool state)
        {
            _models[index] = new ItemModel{Label = _models[index].Label, IsChecked = state};
        }
    }
}