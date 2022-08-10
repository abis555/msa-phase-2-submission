using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using msa_phase_2_api.Models;

namespace msa_phase_2_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly HttpClient _client;


        /// <summary />
        public PokemonController(IHttpClientFactory clientFactory)
        {
            if (clientFactory is null)
            {
                throw new ArgumentNullException(nameof(clientFactory));
            }

            _client = clientFactory.CreateClient("pokemon");
        }

        /// <summary>
        /// Gets the raw JSON consist of the searched pokemon 
        /// </summary>
        /// <returns>A JSON object representing all the details on that pokemon available in PokeApi</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetPokemon(string id)
        {
            var res = await _client.GetAsync(id);
            Pokemon pokemon = await res.Content.ReadFromJsonAsync<Pokemon>();

            return Ok(pokemon);
        }
    }
}
