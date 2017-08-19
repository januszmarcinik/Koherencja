using System.Text;
using System.Web;
using System.Web.Mvc;
using TwitterBootstrap3;
using TwitterBootstrapMVC.BootstrapMethods;
using TwitterBootstrapMVC.Controls;

namespace JanuszMarcinik.Mvc.Extensions.Helpers
{
    public static class ButtonsHelper
    {
        public static BootstrapActionLinkButton AddButton<TModel>(this BootstrapBase<TModel> bootstrap, ActionResult result) where TModel : class
        {
            return bootstrap.ActionLinkButton("Dodaj", result).Style(ButtonStyle.Success).PrependIcon(FontAwesome.plus);
        }

        public static BootstrapActionLinkButton ListButton<TModel>(this BootstrapBase<TModel> bootstrap, string linkText, ActionResult result) where TModel : class
        {
            return bootstrap.ActionLinkButton(linkText, result).Title(linkText).PrependIcon(FontAwesome.list).Style(ButtonStyle.Warning);
        }

        public static BootstrapActionLinkButton EditButton<TModel>(this BootstrapBase<TModel> bootstrap, ActionResult result) where TModel : class
        {
            return bootstrap.ActionLinkButton("", result).Title("Edytuj").Style(ButtonStyle.Warning).PrependIcon(FontAwesome.pencil);
        }

        public static BootstrapActionLinkButton DeleteButton<TModel>(this BootstrapBase<TModel> bootstrap, ActionResult result) where TModel : class
        {
            return bootstrap.ActionLinkButton("", result).Title("Usuń").Style(ButtonStyle.Danger).PrependIcon(FontAwesome.remove);
        }

        public static BootstrapActionLinkButton BackButton<TModel>(this BootstrapBase<TModel> bootstrap, ActionResult result) where TModel : class
        {
            return bootstrap.ActionLinkButton("Powrót", result).PrependIcon(FontAwesome.arrow_left);
        }

        public static BootstrapButton<TModel> SubmitSaveButton<TModel>(this BootstrapBase<TModel> bootstrap) where TModel : class
        {
            return bootstrap.SubmitButton().Text("Zapisz").PrependIcon(FontAwesome.save).Style(ButtonStyle.Primary);
        }

        public static BootstrapButton<TModel> SubmitDeleteButton<TModel>(this BootstrapBase<TModel> bootstrap) where TModel : class
        {
            return bootstrap.SubmitButton().Text("Usuń").PrependIcon(FontAwesome.remove).Style(ButtonStyle.Danger);
        }

        #region ButtonsFooter()
        public static MvcHtmlString ButtonsFooter(this HtmlHelper htmlHelper, params IHtmlString[] controls)
        {
            var footer = new TagBuilder("div");
            footer.AddCssClass("navbar-fixed-bottom");
            footer.AddCssClass("navbar-inverse");
            footer.AddCssClass("navbar");

            var container = new TagBuilder("div");
            container.AddCssClass("container-fluid");

            var buttons = new TagBuilder("p");
            buttons.AddCssClass("navbar-text");

            var builder = new StringBuilder();
            foreach (var control in controls)
            {
                builder.AppendLine(control.ToString());
            }

            buttons.InnerHtml = builder.ToString();
            container.InnerHtml = buttons.ToString();
            footer.InnerHtml = container.ToString();

            return MvcHtmlString.Create(footer.ToString());
        }
        #endregion

        #region ConfirmDeleteButton()
        public static BootstrapButton<TModel> ConfirmDeleteButton<TModel>(this BootstrapBase<TModel> bootstrap) where TModel : class
        {
            return bootstrap.Button().Style(ButtonStyle.Danger).PrependIcon(FontAwesome.remove).Text("Usuń").TriggerModal("deleteConfirm");
        }
        #endregion
    }
}