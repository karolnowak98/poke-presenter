using System;
using UnityEngine;

namespace GlassyCode.PokemonPresenter.Pokemons.Data.Models
{
    [Serializable]
    public struct PokemonDetails
    {
        //Json models
        public Ability[] abilities;
        public int height;
        public string name;
        public Sprites sprites;
        public Stat[] stats;
        public int weight;
        
        //Downloaded
        public Texture2D FrontTexture2D;
        public Texture2D BackTexture2D;
        public string NameToShow;
    }

    [Serializable]
    public struct Ability
    {
        public AbilityDetails ability;
    }

    [Serializable]
    public struct AbilityDetails
    {
        public string name;
    }

    [Serializable]
    public struct Sprites
    {
        public string back_default;
        public string front_default;
    }

    [Serializable]
    public struct Stat
    {
        public int base_stat;
    }
}