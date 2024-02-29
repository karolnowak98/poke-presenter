using System;
using GlassyCode.PokemonPresenter.Pokemons.Data.Models;

namespace GlassyCode.PokemonPresenter.Pokemons.Logic
{
    public interface IPokeApiController
    {
        event Action OnStartDownloadingAllPokemons;
        event Action<PokemonModel[]> OnFinishDownloadingAllPokemons;
        event Action<PokemonDetails> OnFinishDownloadingPokemonDetails;
        event Action<float> OnDownloadingProgress;
        void DownloadPokemons();
        void DownloadPokemonDetails(string pokemonName);
        void FindPokemonByName(string pokemonName);
    }
}