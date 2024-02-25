using System;

namespace GlassyCode.PokemonPresenter.Scripts.Core.Api.AudioVisual.Models
{
    [Serializable]
    public struct PokemonList
    {
        public int count;
        public string next;
        public string previous;
        public PokemonModel[] results;
    }
    
    [Serializable]
    public struct PokemonModel
    {
        public string name;
        public string url;
    }
}