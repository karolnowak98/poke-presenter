namespace GlassyCode.PokemonPresenter.Pokemons.Data
{
    public static class PokeApiConfig 
    {
        //With odin I would make a scriptable object with serialized dictionary <key, url>
        //That's more testable way and scalable way
        //For my case I just created static class and const
        
        public const string GetPokesEndpoint = "https://pokeapi.co/api/v2/pokemon";
    }
}