using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using poke.Models;
using Newtonsoft.Json;

namespace poke.Controllers
{
    public class pokeController : Controller
    {
        [HttpGet]
        [Route("{pokeid}")]
        public IActionResult pokemon(int pokeid)
        {
            var PokeInfo = new Dictionary<string, object>();
            WebRequest.GetPokemonDataAsync(pokeid, ApiResponse =>
                {
                    PokeInfo = ApiResponse;
                }
            ).Wait();
            var name = PokeInfo["name"];
            List<object> types = JsonConvert.DeserializeObject<List<object>>(PokeInfo["types"].ToString());
            Dictionary<string, object> primary = JsonConvert.DeserializeObject<Dictionary<string, object>>(types[0].ToString());
            Dictionary<string, object> primary2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(primary["type"].ToString());
            ViewBag.name = PokeInfo["name"];
            ViewBag.primary = primary2["name"];
            ViewBag.weight = PokeInfo["weight"];
            ViewBag.height = PokeInfo["height"];
            return View();
        }
    }
}