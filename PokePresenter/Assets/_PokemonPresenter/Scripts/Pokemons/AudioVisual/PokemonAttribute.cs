using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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