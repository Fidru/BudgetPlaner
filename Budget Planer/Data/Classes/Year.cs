using IData.Interfaces;
using System;
using System.Linq;

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

        public Year(string name, int sortOrder) : this()
        {
            Name = name;
            SortOrder = sortOrder;
        }

        public IElementCollection<IMonth> Months { get; set; }
        public int SortOrder { get; set; }

        public override void ConnectElements(IProject project)
        {
            base.ConnectElements(project);

            Months.ConnectIds(project.Months);
        }

        public override void Delete()
        {
            base.Delete();

            Months.Elements.ToList().ForEach(m => m.Delete());
        }
    }
}