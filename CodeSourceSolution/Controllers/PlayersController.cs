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
        private readonly PlayerDbContext? _context;
        private  IWebHostEnvironment? _environment;

        public PlayersController(PlayerDbContext context, IWebHostEnvironment environment)
        {
            this._context = context;
            this._environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Players.Include(x => x.SeriesEntries).ThenInclude(y => y.Format).ToListAsync());
        }


        public IActionResult AddNewFormats(int? id)
        {
            ViewBag.format = new SelectList(_context.Formats, "FormatId", "FormatName", id.ToString() ?? "");
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
            string webroot = _environment.WebRootPath;


            string imagesFolder = Path.Combine(webroot, "images");
            // Create the images folder if it doesn't exist
            if (!Directory.Exists(imagesFolder))
            {
                Directory.CreateDirectory(imagesFolder);
            }


            string pictureFileName = Path.GetFileName(playerVM.PicturePath.FileName);
            string fileToSave = Path.Combine(imagesFolder, pictureFileName);

            using (var stream = new FileStream(fileToSave, FileMode.Create))
            {
               await playerVM.PicturePath.CopyToAsync(stream);
                player.Picture = "/images/" + pictureFileName;
            }
            _context.Players.Add(player);

            foreach (var item in FormatId)
            {
                SeriesEntry playerFormats = new SeriesEntry()
                {
                    Player = player,
                    PlayerId = player.PlayerId,
                    FormatId = item
                };
                _context.SeriesEntries.Add(playerFormats);

            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }









        public async Task<IActionResult> Edit(int id)
        {
            var player = await _context.Players.FirstOrDefaultAsync(x => x.PlayerId == id);
            PlayerVM playerVM = new PlayerVM()
            {
                PlayerId = player.PlayerId,
                PlayerName = player.PlayerName,
                DateOfBirth = player.DateOfBirth,
                Phone = player.Phone,
                Picture = player.Picture,
                MaritalStatus = (bool)player.MaritalStatus
            };
            var existFormat = _context.SeriesEntries.Where(x => x.PlayerId == id).ToList();
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


            string webroot = _environment.WebRootPath;
            string pictureFileName = Path.GetFileName(playerVM.PicturePath.FileName);
            string fileToSave = Path.Combine(webroot, pictureFileName);

            using (var stream = new FileStream(fileToSave, FileMode.Create))
            {
                playerVM.PicturePath.CopyTo(stream);
                player.Picture = "/" + pictureFileName;
            }


            var existFormat = _context.SeriesEntries.Where(x => x.PlayerId == player.PlayerId).ToList();
            foreach (var item in existFormat)
            {
                _context.SeriesEntries.Remove(item);
            }
            foreach (var item in FormatId)
            {
                SeriesEntry seriesEntry = new SeriesEntry()
                {

                    PlayerId = player.PlayerId,
                    FormatId = item
                };
                _context.SeriesEntries.Add(seriesEntry);

            }
            _context.Update(player);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }

        

        public async Task<IActionResult> Delete(int id)
        {


            var player = await _context.Players.FirstOrDefaultAsync(x => x.PlayerId == id);
            PlayerVM playerVM = new PlayerVM()
            {
                PlayerId = player.PlayerId,
                PlayerName = player.PlayerName,
                DateOfBirth = player.DateOfBirth,
                Phone = player.Phone,
                Picture = player.Picture,
                MaritalStatus = (bool)player.MaritalStatus
            };
            var existFormat = _context.SeriesEntries.Where(x => x.PlayerId == id).ToList();
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
            var player = await _context.Players.FirstOrDefaultAsync(x => x.PlayerId == id);
            var existFormat = _context.SeriesEntries.Where(x => x.PlayerId == id).ToList();
            foreach (var item in existFormat)
            {
                _context.SeriesEntries.Remove(item);
            }
            _context.Remove(player);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


    }

  
}
