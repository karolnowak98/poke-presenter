using System;

namespace GlassyCode.PokemonPresenter.Pokemons.Data.Models
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
        //Json
        public string name;
        public string url;
        
        public string NameToShow;
    }
}