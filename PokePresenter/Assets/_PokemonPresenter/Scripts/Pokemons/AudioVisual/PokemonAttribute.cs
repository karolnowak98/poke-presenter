using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GlassyCode.PokemonPresenter.Pokemons.AudioVisual
{
    [Serializable]
    public struct PokemonAttributeUI
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _value;

        public Slider Slider => _slider;
        public TextMeshProUGUI Value => _value;
    }
}