using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace hmart.Models
{
    public class Setting
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 200)]
        public string TitleIconSrc { get; set; }

        [NotMapped]
        public IFormFile TitleIcon { get; set; }

        [StringLength(maximumLength: 200)]
        public string WellComeText { get; set; }

        [Required]
        [StringLength(maximumLength: 30)]
        public string Phone1 { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength: 200)]
        public string HeaderLogoSrc { get; set; }

        [NotMapped]
        public IFormFile HeaderLogo { get; set; }

        [StringLength(maximumLength: 200)]
        public string FooterLogoSrc { get; set; }

        [NotMapped]
        public IFormFile FooterLogo { get; set; }

        [Required]
        [StringLength(maximumLength: 200)]
        public string MainBgImgSrc { get; set; }

        [NotMapped]
        public IFormFile MainBgImg { get; set; }

        [Required]
        [StringLength(maximumLength: 200)]
        public string SliderBgImgSrc { get; set; }

        [NotMapped]
        public IFormFile SliderBgImg { get; set; }

        [Required]
        [StringLength(maximumLength: 200)]
        public string FashionBgImgSrc { get; set; }

        [NotMapped]
        public IFormFile FashionBgImg { get; set; }

        [Required]
        [StringLength(maximumLength: 80)]
        public string FashionTitle { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string FashionSubTitle { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string FashionBtnText { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string FeaturedTitle { get; set; }

        [StringLength(maximumLength: 300)]
        public string FeaturedDesc { get; set; }

        [Required] 
        [StringLength(maximumLength: 100)]
        public string TestimonialsTitle { get; set; }

        [StringLength(maximumLength: 300)]
        public string TestimonialsDesc { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string BlogsTitle { get; set; }

        [StringLength(maximumLength: 300)]
        public string BlogsDesc { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string RelatedTitle { get; set; }

        [StringLength(maximumLength: 300)]
        public string RelatedDesc { get; set; }

        [StringLength(maximumLength: 400)]
        public string AboutText { get; set; }

        [StringLength(maximumLength: 150)]
        public string Address { get; set; }

        [StringLength(maximumLength: 30)]
        public string Phone2 { get; set; }

        [StringLength(maximumLength: 100)]
        public string Site { get; set; }


        // About Setting

        [Required]
        [StringLength(maximumLength: 100)]
        public string AboutTitle { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string AboutSubtitle { get; set; }

        [StringLength(maximumLength: 700)]
        public string AboutDesc { get; set; }

        [Required]
        [StringLength(maximumLength: 200)]
        public string PromoImgSrc { get; set; }

        [NotMapped]
        public IFormFile PromoImg { get; set; }

        [StringLength(maximumLength: 200)]
        public string PromoVideoLink { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        public string AboutTeamTitle { get; set; }

        [StringLength(maximumLength: 300)]
        public string AboutTeamDesc { get; set; }

    }
}
