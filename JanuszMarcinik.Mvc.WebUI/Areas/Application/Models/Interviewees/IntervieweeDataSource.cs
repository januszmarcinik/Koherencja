using JanuszMarcinik.Mvc.Domain.Application.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JanuszMarcinik.Mvc.WebUI.Areas.Application.Models.Interviewees
{
    public class IntervieweeDataSource : DataSource<IntervieweeViewModel>
    {
        public IntervieweeDataSource() : base(new IntervieweeViewModel()) { }

        public List<IntervieweeViewModel> Interviewees { get; set; }

        public override void SetActions()
        {
            base.PrepareData(this.Interviewees);

            foreach (var row in this.Data)
            {
                //row.ListText = "Pytania";
                //row.ListAction = MVC.Admin.Questions.List(row.PrimaryKeyId);
                //row.EditAction = MVC.Admin.Questionnaires.Edit(row.PrimaryKeyId);
                //row.DeleteAction = MVC.Admin.Questionnaires.Delete(row.PrimaryKeyId);
            }

            //this.AddAction = MVC.Admin.Questionnaires.Create();
            //this.BackAction = MVC.Admin.Configuration.Index();
            this.Title = "Ankieterzy";
        }
    }
}