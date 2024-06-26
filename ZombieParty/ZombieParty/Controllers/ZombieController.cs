﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZombieParty.Models;
using ZombieParty.Models.Data;
using ZombieParty.Services;
using ZombieParty.Utility;
using ZombieParty.ViewModels;

namespace ZombieParty.Controllers
{
    public class ZombieController : Controller
    {
        private ZombiePartyDbContext _baseDonnees { get; set; }
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IZombieService _zombieService;
        private IZombieTypeService _zombieTypeService;

        public ZombieController(ZombiePartyDbContext baseDonnees, IWebHostEnvironment webHostEnvironment, IZombieService zombieService, IZombieTypeService zombieTypeService)
        {
            _baseDonnees = baseDonnees;
            _webHostEnvironment = webHostEnvironment;
            _zombieService = zombieService;
            _zombieTypeService = zombieTypeService;
        }

        public async Task<IActionResult> Index()
        {
            IReadOnlyList<Zombie> zombiesList = await _zombieService.GetAllIndexAsync();

            return View(zombiesList);
        }


        /*public async Task<IActionResult> StrongestZombies()
        {
            List<Zombie> zombiesList = await _baseDonnees.Zombies.Where(z => z.Point >= 8).OrderBy(z => z.Name).Include(z => z.ZombieType).ToListAsync();

            return View(zombiesList);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Zombie? zombie = await _baseDonnees.Zombies.Include(z => z.ZombieType).FirstOrDefaultAsync(z => z.Id == id);
            if (zombie == null)
            {
                return NotFound();
            }

            return View(zombie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            Zombie? zombie = await _baseDonnees.Zombies.FindAsync(id);
            if (zombie == null)
            {
                return NotFound();
            }

            _baseDonnees.Zombies.Remove(zombie);
            await _baseDonnees.SaveChangesAsync();
            TempData[AppConstants.Success] = $"Zombie {zombie.Name} terminated";
            return RedirectToAction("Index");
        }*/

        public async Task<IActionResult> Edit(int id)
        {
            ZombieVM zombieVM = new ZombieVM();
            zombieVM.Zombie = await _zombieService.GetByIdAsync(id);
            zombieVM.ZombieTypeSelectList = _zombieTypeService.ListZombieTypeDisponible();

            return View(zombieVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ZombieVM zombieVM)
        {
            if (_zombieService.ZombieNameExist(zombieVM.Zombie.Name))
            {
                TempData[AppConstants.Error] = $"Zombie {zombieVM.Zombie.Name} is already exist.";
                return View(zombieVM);
            }

            //Si le modèle est valide le zombie est modifié et nous sommes redirigé vers index.
            if (ModelState.IsValid)
            {
                string webRootPath = _webHostEnvironment.WebRootPath; //Chemin des images de zombies
                var files = HttpContext.Request.Form.Files; //nouvelle image récupérée

                if (files.Count > 0)
                {
                    // Nom fichier généré, unique        
                    string fileName = Guid.NewGuid().ToString();
                    // chemin pour les images du zombie
                    var uploads = Path.Combine(webRootPath, AppConstants.ImagePathZombies);
                    // extraire l'extention du fichier
                    var extenstion = Path.GetExtension(files[0].FileName);

                    // Create un cannal pour transférer le fichier 
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }

                    // Composer le nom du fichier avec son extension qui sera enregister dans la BD
                    // avec le path relatif à partir du Root
                    // sans le path relatif (le path devra être ajouté dans la View)
                    zombieVM.Zombie.Image = fileName + extenstion;
                }
                else
                {
                    zombieVM.Zombie.Image = zombieVM.OldImage;
                }

                await _zombieService.EditAsync(zombieVM.Zombie);
                TempData[AppConstants.Success] = $"Zombie {zombieVM.Zombie.Name} has been modified";
                return this.RedirectToAction("Index");
            }
            zombieVM.ZombieTypeSelectList = _zombieTypeService.ListZombieTypeDisponible();

            return View(zombieVM);
        }

        public IActionResult Create()
        {
            ZombieVM zombieVM = new ZombieVM();
            zombieVM.ZombieTypeSelectList = _zombieTypeService.ListZombieTypeDisponible();

            return View(zombieVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ZombieVM zombieVM)
        {
            if (_zombieService.ZombieNameExist(zombieVM.Zombie.Name))
            {
                TempData[AppConstants.Error] = $"Zombie {zombieVM.Zombie.Name} is already exist.";
                return View(zombieVM);
            }

            //Si le modèle est valide le zombie est ajouté et nous sommes redirigé vers index.
            if (ModelState.IsValid)
            {
                string webRootPath = _webHostEnvironment.WebRootPath; //Chemin des images de zombies
                var files = HttpContext.Request.Form.Files; //nouvelle image récupérée

                if (files.Count > 0)
                {
                    // Nom fichier généré, unique        
                    string fileName = Guid.NewGuid().ToString();
                    // chemin pour les images du zombie
                    var uploads = Path.Combine(webRootPath, AppConstants.ImagePathZombies);
                    // extraire l'extention du fichier
                    var extenstion = Path.GetExtension(files[0].FileName);

                    // Create un cannal pour transférer le fichier 
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }

                    // Composer le nom du fichier avec son extension qui sera enregister dans la BD
                    // avec le path relatif à partir du Root
                    // sans le path relatif (le path devra être ajouté dans la View)
                    zombieVM.Zombie.Image = fileName + extenstion;
                }

                await _zombieService.CreateAsync(zombieVM.Zombie);
                TempData[AppConstants.Success] = $"Zombie {zombieVM.Zombie.Name} added";
                return this.RedirectToAction("Index");
            }
            zombieVM.ZombieTypeSelectList = _zombieTypeService.ListZombieTypeDisponible();

            return View(zombieVM);
        }
    }
}
