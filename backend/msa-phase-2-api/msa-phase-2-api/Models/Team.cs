using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace msa_phase_2_api.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Pokemon> pokemons { get; set; }
    }
}
