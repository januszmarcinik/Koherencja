using JanuszMarcinik.Mvc.Domain.DataSource;
using System.ComponentModel.DataAnnotations;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Account.Models.Roles
{
    public class RoleViewModel
    {
        public int Id { get; set; }

        [Grid(Order = 1)]
        [Display(Name = "Nazwa roli")]
        [StringLength(256, ErrorMessage = "Nazwa roli może zawierać do 256 znaków.")]
        public string Name { get; set; }
    }
}