using Data.Services;
using IData.Interfaces;
using System;

namespace Data.Classes
{
    public class Year : Element, IYear
    {
        public Year() : base()
        {
            Months = new ElementCollection<IMonth>();
            SortOrder = 0;
            Name = DateTime.Now.Year.ToString();
        }

        public Year(string name) : this()
        {
            Name = name;
        }

        public IElementCollection<IMonth> Months { get; set; }
        public int SortOrder { get; set; }

        public override void ConnectElements(IProject project)
        {
            base.ConnectElements(project);

            Months.ConnectIds(project.Months);

            new YearFactory().AlligneMonths(Months.Elements);
        }
    }
}