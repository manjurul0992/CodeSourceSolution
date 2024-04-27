using CodeSourceSolution.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System;
using System.Threading.Tasks;
using CodeSourceSolution.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

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

        public IActionResult AddNewFormats(int? id)
        {
            ViewBag.format = new SelectList(_Context.Formats, "FormatId", "FormatName", id.ToString() ?? "");
            return PartialView("_addNewFormats");
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlayerVM playerVM, int[] FormatId)
        {
            Player player = new Player
            {
                PlayerName = playerVM.PlayerName,
                DateOfBirth = playerVM.DateOfBirth,
                Phone = playerVM.Phone,
                MaritalStatus = (bool)playerVM.MaritalStatus

            };
            string webroot = _Environment.WebRootPath;

            string pictureFileName = Path.GetFileName(playerVM.PicturePath.FileName);
            string fileToSave = Path.Combine(webroot, pictureFileName);

            using (var stream = new FileStream(fileToSave, FileMode.Create))
            {
                playerVM.PicturePath.CopyTo(stream);
                player.Picture = "/" + pictureFileName;
            }


            foreach (var item in FormatId)
            {
                SeriesEntry playerFormats = new SeriesEntry()
                {
                    Player = player,
                    PlayerId = player.PlayerId,
                    FormatId = item
                };
                _Context.SeriesEntries.Add(playerFormats);

            }
            await _Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }



        public async Task<IActionResult> Edit(int id)
        {
            var player = await _Context.Players.FirstOrDefaultAsync(x => x.PlayerId == id);
            PlayerVM playerVM = new PlayerVM()
            {
                PlayerId = player.PlayerId,
                PlayerName = player.PlayerName,
                DateOfBirth = player.DateOfBirth,
                Phone = player.Phone,
                Picture = player.Picture,
                MaritalStatus = (bool)player.MaritalStatus
            };
            var existFormat = _Context.SeriesEntries.Where(x => x.PlayerId == id).ToList();
            foreach (var item in existFormat)
            {
                playerVM.FormatList.Add(item.FormatId);
            }
            return View(playerVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PlayerVM playerVM, int[] FormatId)
        {

            Player player = new Player
            {
                PlayerId = playerVM.PlayerId,
                PlayerName = playerVM.PlayerName,
                DateOfBirth = playerVM.DateOfBirth,
                Phone = playerVM.Phone,
                MaritalStatus = playerVM.MaritalStatus,
                Picture = playerVM.Picture

            };
            var file = playerVM.PicturePath;


            string webroot = _Environment.WebRootPath;
            string pictureFileName = Path.GetFileName(playerVM.PicturePath.FileName);
            string fileToSave = Path.Combine(webroot, pictureFileName);

            using (var stream = new FileStream(fileToSave, FileMode.Create))
            {
                playerVM.PicturePath.CopyTo(stream);
                player.Picture = "/" + pictureFileName;
            }


            var existFormat = _Context.SeriesEntries.Where(x => x.PlayerId == player.PlayerId).ToList();
            foreach (var item in existFormat)
            {
                _Context.SeriesEntries.Remove(item);
            }
            foreach (var item in FormatId)
            {
                SeriesEntry seriesEntry = new SeriesEntry()
                {

                    PlayerId = player.PlayerId,
                    FormatId = item
                };
                _Context.SeriesEntries.Add(seriesEntry);

            }
            _Context.Update(player);
            await _Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

        

        public async Task<IActionResult> Delete(int id)
        {


            var player = await _Context.Players.FirstOrDefaultAsync(x => x.PlayerId == id);
            PlayerVM playerVM = new PlayerVM()
            {
                PlayerId = player.PlayerId,
                PlayerName = player.PlayerName,
                DateOfBirth = player.DateOfBirth,
                Phone = player.Phone,
                Picture = player.Picture,
                MaritalStatus = (bool)player.MaritalStatus
            };
            var existFormat = _Context.SeriesEntries.Where(x => x.PlayerId == id).ToList();
            foreach (var item in existFormat)
            {
                playerVM.FormatList.Add(item.FormatId);
            }
            return View(playerVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            var player = await _Context.Players.FirstOrDefaultAsync(x => x.PlayerId == id);
            var existFormat = _Context.SeriesEntries.Where(x => x.PlayerId == id).ToList();
            foreach (var item in existFormat)
            {
                _Context.SeriesEntries.Remove(item);
            }
            _Context.Remove(player);
            await _Context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


    }
}
