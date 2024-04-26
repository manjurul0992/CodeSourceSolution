using CodeSourceSolution.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CodeSourceSolution.Controllers
{
    public class PlayersController : Controller
    {
        private readonly PlayerDbContext? _Context;
        private  IWebHostEnvironment? _Environment;

        public PlayersController(PlayerDbContext context, IWebHostEnvironment environment)
        {
            this._Context = context;
            this._Environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _Context.Players.Include(x => x.SeriesEntries).ThenInclude(y => y.Format).ToListAsync());
        }
        
    }
}
