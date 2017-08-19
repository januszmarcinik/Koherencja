using System.Web.Mvc;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Admin.Models
{
    public class DeleteConfirmViewModel
    {
        public int Id { get; set; }
        public string ConfirmationText { get; set; }
    }
}