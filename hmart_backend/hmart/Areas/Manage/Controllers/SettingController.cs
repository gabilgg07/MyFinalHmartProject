using hmart.DAL;
using hmart.Helpers;
using hmart.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin,Admin,EditorAdmin")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SettingController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            Setting setting = _context.Settings.FirstOrDefault();

            return View(setting);
        }

        public IActionResult Update()
        {
            Setting setting = _context.Settings.FirstOrDefault();
            return View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Setting set)
        {
            if (!ModelState.IsValid) return View(set);

            Setting setting = _context.Settings.FirstOrDefault();

            if (set.TitleIcon != null)
            {
                if (set.TitleIcon.ContentType != "image/jpeg" && set.TitleIcon.ContentType != "image/png" && set.TitleIcon.ContentType != "image/vnd.microsoft.icon")
                {
                    ModelState.AddModelError("TitleIcon", "You can choose file only .jpg, .jpeg, .ico or .png format!");
                    return View(set);
                }

                if (set.TitleIcon.Length > 1048576)
                {
                    ModelState.AddModelError("TitleIcon", "You can choose file only maximum 1Mb !");
                    return View(set);
                }

                string newFileName = FileManager.Save(_env.WebRootPath, "uploads/setting", set.TitleIcon);

                if (!string.IsNullOrWhiteSpace(setting.TitleIconSrc))
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/setting", setting.TitleIconSrc);
                }

                setting.TitleIconSrc = newFileName;
            }

            if (set.HeaderLogo != null)
            {
                if (set.HeaderLogo.ContentType != "image/jpeg" && set.HeaderLogo.ContentType != "image/png")
                {
                    ModelState.AddModelError("HeaderLogo", "You can choose file only .jpg, .jpeg or .png format!");
                    return View(set);
                }

                if (set.HeaderLogo.Length > 2097152)
                {
                    ModelState.AddModelError("HeaderLogo", "You can choose file only maximum 2Mb !");
                    return View(set);
                }

                string newFileName = FileManager.Save(_env.WebRootPath, "uploads/setting", set.HeaderLogo);

                if (!string.IsNullOrWhiteSpace(setting.HeaderLogoSrc))
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/setting", setting.HeaderLogoSrc);
                }

                setting.HeaderLogoSrc = newFileName;
            }

            if (set.FooterLogo != null)
            {
                if (set.FooterLogo.ContentType != "image/jpeg" && set.FooterLogo.ContentType != "image/png")
                {
                    ModelState.AddModelError("FooterLogo", "You can choose file only .jpg, .jpeg or .png format!");
                    return View(set);
                }

                if (set.FooterLogo.Length > 2097152)
                {
                    ModelState.AddModelError("FooterLogo", "You can choose file only maximum 2Mb !");
                    return View(set);
                }

                string newFileName = FileManager.Save(_env.WebRootPath, "uploads/setting", set.FooterLogo);

                if (!string.IsNullOrWhiteSpace(setting.FooterLogoSrc))
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/setting", setting.FooterLogoSrc);
                }

                setting.FooterLogoSrc = newFileName;
            }

            if (set.SliderBgImg != null)
            {
                if (set.SliderBgImg.ContentType != "image/jpeg" && set.SliderBgImg.ContentType != "image/png")
                {
                    ModelState.AddModelError("SliderBgImg", "You can choose file only .jpg, .jpeg or .png format!");
                    return View(set);
                }

                if (set.SliderBgImg.Length > 5242880)
                {
                    ModelState.AddModelError("SliderBgImg", "You can choose file only maximum 5Mb !");
                    return View(set);
                }

                string newFileName = FileManager.Save(_env.WebRootPath, "uploads/setting", set.SliderBgImg);

                if (!string.IsNullOrWhiteSpace(setting.SliderBgImgSrc))
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/setting", setting.SliderBgImgSrc);
                }

                setting.SliderBgImgSrc = newFileName;
            }

            if (set.FashionBgImg != null)
            {
                if (set.FashionBgImg.ContentType != "image/jpeg" && set.FashionBgImg.ContentType != "image/png")
                {
                    ModelState.AddModelError("FashionBgImg", "You can choose file only .jpg, .jpeg or .png format!");
                    return View(set);
                }

                if (set.FashionBgImg.Length > 5242880)
                {
                    ModelState.AddModelError("FashionBgImg", "You can choose file only maximum 5Mb !");
                    return View(set);
                }

                string newFileName = FileManager.Save(_env.WebRootPath, "uploads/setting", set.FashionBgImg);

                if (!string.IsNullOrWhiteSpace(setting.FashionBgImgSrc))
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/setting", setting.FashionBgImgSrc);
                }

                setting.FashionBgImgSrc = newFileName;
            }

            if (set.MainBgImg != null)
            {
                if (set.MainBgImg.ContentType != "image/jpeg" && set.MainBgImg.ContentType != "image/png")
                {
                    ModelState.AddModelError("MainBgImg", "You can choose file only .jpg, .jpeg or .png format!");
                    return View(set);
                }

                if (set.MainBgImg.Length > 5242880)
                {
                    ModelState.AddModelError("MainBgImg", "You can choose file only maximum 5Mb !");
                    return View(set);
                }

                string newFileName = FileManager.Save(_env.WebRootPath, "uploads/setting", set.MainBgImg);

                if (!string.IsNullOrWhiteSpace(setting.MainBgImgSrc))
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/setting", setting.MainBgImgSrc);
                }

                setting.MainBgImgSrc = newFileName;
            }

            if (set.PromoImg != null)
            {
                if (set.PromoImg.ContentType != "image/jpeg" && set.PromoImg.ContentType != "image/png")
                {
                    ModelState.AddModelError("PromoImg", "You can choose file only .jpg, .jpeg or .png format!");
                    return View(set);
                }

                if (set.PromoImg.Length > 5242880)
                {
                    ModelState.AddModelError("PromoImg", "You can choose file only maximum 5Mb !");
                    return View(set);
                }

                string newFileName = FileManager.Save(_env.WebRootPath, "uploads/setting", set.PromoImg);

                if (!string.IsNullOrWhiteSpace(setting.PromoImgSrc))
                {
                    FileManager.Delete(_env.WebRootPath, "uploads/setting", setting.PromoImgSrc);
                }

                setting.PromoImgSrc = newFileName;
            }

            setting.WellComeText = set.WellComeText;
            setting.FeaturedTitle = set.FeaturedTitle;
            setting.FeaturedDesc = set.FeaturedDesc;
            setting.TestimonialsTitle = set.TestimonialsTitle;
            setting.TestimonialsDesc = set.TestimonialsDesc;
            setting.BlogsTitle = set.BlogsTitle;
            setting.BlogsDesc = set.BlogsDesc;
            setting.RelatedTitle = set.RelatedTitle;
            setting.RelatedDesc = set.RelatedDesc;
            setting.FashionTitle = set.FashionTitle;
            setting.FashionSubTitle = set.FashionSubTitle;
            setting.FashionBtnText = set.FashionBtnText;
            setting.AboutText = set.AboutText;
            setting.Address = set.Address;
            setting.Phone1 = set.Phone1;
            setting.Phone2 = set.Phone2;
            setting.Email = set.Email;
            setting.Site = set.Site;

            setting.AboutTitle = set.AboutTitle;
            setting.AboutSubtitle = set.AboutSubtitle;
            setting.AboutDesc = set.AboutDesc;
            setting.PromoVideoLink = set.PromoVideoLink;
            setting.AboutTeamTitle = set.AboutTeamTitle;
            setting.AboutTeamDesc = set.AboutTeamDesc;

            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
