using JanuszMarcinik.Mvc.Domain.DataSource;
using JanuszMarcinik.Mvc.Domain.Identity.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Account.Models.Users
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            this.Roles = new List<UserRole>();
            this.AllRoles = new List<SelectListItem>();
            this.SelectedRoles = new List<int>();
        }

        [Grid(DataType = GridDataType.PrimaryKey)]
        public int Id { get; set; }

        [Grid(Order = 1)]
        [Display(Name = "Nazwa")]
        public string UserName { get; set; }

        [Grid(Order = 2)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public ICollection<UserRole> Roles { get; set; }

        public IEnumerable<int> SelectedRoles { get; set; }
        public List<SelectListItem> AllRoles { get; set; }
    }
}