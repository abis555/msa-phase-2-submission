using msa_phase_2_api.Models;
using msa_phase_2_api.Services;
using NUnit.Framework;
using System.Collections.Generic;

namespace TeamTests
{
    public class TeamServiceTests
    {
        readonly List<Pokemon> pokemonList = new List<Pokemon> 
        { 
            new Pokemon { Id = 6, Name = "charizard" },
            new Pokemon { Id = 132, Name = "ditto" },
            new Pokemon { Id = 25, Name = "pikachu" }
        };

        static readonly Team t1 = new Team { Id = 1, Name = "t1", pokemons = null };
        static readonly Team t2 = new Team { Id = 2, Name = "t2", pokemons = null };
        static readonly Team t3 = new Team { Id = 3, Name = "t3", pokemons = null };

        [Test]
        public void TestGet_ReturnTeamName()
        {
            t1.pokemons = pokemonList;
            TeamService.Add(t1);

            var team = TeamService.Get(t1.Name);

            Assert.AreEqual("t1", team.Name);
        }

        [Test]
        public void TestGetAll_ReturnAllTeams()
        {
            TeamService.Add(t1);
            TeamService.Add(t2);
            TeamService.Add(t3);

            var team = TeamService.GetAll();

            var teamList = new List<Team>
            {
                t1, t2, t3
            };

            Assert.AreEqual(teamList, team);
        }

        [Test]
        public void TestGetPokemonIndex_ReturnPokemonIndex()
        {
            t1.pokemons = pokemonList;

            TeamService.Add(t1);
            TeamService.Add(t2);
            TeamService.Add(t3);

            var index = TeamService.GetPokemonIndex("pikachu", t1.Id);

            Assert.AreEqual(2, index);
        }

        [Test]
        public void TestDelete_ReturnDeleteTeam()
        {
            t1.pokemons = pokemonList;

            TeamService.Add(t1);
            TeamService.Add(t2);
            TeamService.Add(t3);

            TeamService.Delete(t1.Name);

            Assert.AreEqual(null, TeamService.Get(t1.Name));
        }
    }
}