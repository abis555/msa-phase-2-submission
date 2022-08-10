using Microsoft.AspNetCore.Mvc;
using msa_phase_2_api.Models;
using msa_phase_2_api.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace msa_phase_2_api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly HttpClient _client;

        /// <summary />
        public TeamController(IHttpClientFactory clientFactory)
        {
            if (clientFactory is null)
            {
                throw new ArgumentNullException(nameof(clientFactory));
            }

            _client = clientFactory.CreateClient("pokemon");
        }

        public static async Task<Pokemon> getPokemon (string value, HttpClient _client)
        {
            var res = await _client.GetAsync(value.ToString());

            if (res.StatusCode == System.Net.HttpStatusCode.NotFound) return null;

            Pokemon pokemon = await res.Content.ReadFromJsonAsync<Pokemon>();
            return pokemon;
        }

        /// <summary>
        /// Get Pokemon Team
        /// </summary>
        /// <returns>Team with given name</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<Team> Get(string teamName)
        {
            var team = TeamService.Get(teamName);

            if (team == null) return BadRequest("Team not found. Team is either deleted or not made yet.");

            return team;
        }

        /// <summary>
        /// Get Pokemon Team
        /// </summary>
        /// <returns>All teams</returns>
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(200)]
        public ActionResult<List<Team>> Get()
        {
            var team = TeamService.GetAll();

            return team;
        }

        /// <summary>
        /// Create Pokemon Team
        /// </summary>
        /// <returns>Creates Team with given team name</returns>
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> CreateAsync(string teamName, DateTime DoB)
        {
            Team team = new Team { Name = teamName };
            var pokemonList = new List<Pokemon>();
            var pokemonIndex = new List<string>();
            Random rnd = new Random();

            if (DoB.ToString("yy") == "00")
            {
                pokemonIndex.Add(rnd.Next(1, 905).ToString());
            } else
            {
                pokemonIndex.Add(DoB.ToString("yy"));
            }

            pokemonIndex.Add(DoB.Month.ToString());
            pokemonIndex.Add(DoB.Day.ToString());

            for (int i = 0; i < 3; i++)
            {
                pokemonList.Add(await getPokemon(pokemonIndex[i], _client));
            }

            team.pokemons = pokemonList;

            TeamService.Add(team);

            return Ok(CreatedAtAction(nameof(CreateAsync), new { id = team.Id }, team));
        }

        /// <summary>
        /// Update Pokemon Team
        /// </summary>
        /// <returns>Updates the team with given new pokemon with given old pokemon</returns>
        [HttpPut]
        [ProducesResponseType(201)]
        public async Task<IActionResult> UpdateAsync(string oldPoke, string newPoke, string teamName)
        {
            var team = TeamService.Get(teamName);

            if (team is null) return BadRequest("Team not found. Team is either deleted or not made yet.");

            var pokemonIndex = TeamService.GetPokemonIndex(oldPoke, team.Id);

            if (pokemonIndex == -1) return BadRequest("Old Pokemon not found. Please enter the correct name.");

            Pokemon newPokemon = await getPokemon(newPoke, _client);

            if (newPokemon is null) return BadRequest("New Pokemon not found. Please enter the correct name.");

            team.pokemons[pokemonIndex] = newPokemon;

            return NoContent();
        }

        /// <summary>
        /// Update Team Name
        /// </summary>
        /// <returns>Updates the team's name</returns>
        [HttpPut]
        [Route("name")]
        [ProducesResponseType(201)]
        public IActionResult UpdateTeamName(string oldName, string newName)
        {
            var team = TeamService.Get(oldName);

            if (team is null) return BadRequest("Team not found. Team is either deleted or not made yet.");

            team.Name = newName;

            return NoContent();
        }

        /// <summary>
        /// Delete Pokemon Team
        /// </summary>
        /// <returns>Deletes the team of given id</returns>
        [HttpDelete]
        [ProducesResponseType(204)]
        public IActionResult Delete(string teamName)
        {
            var team = TeamService.Get(teamName);

            if (team is null) return BadRequest("Team not found. Team is either deleted or not made yet.");

            TeamService.Delete(teamName);

            return NoContent();
        }
    }
}