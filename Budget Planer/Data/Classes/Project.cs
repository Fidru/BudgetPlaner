using IData.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Data.Classes
{
    public class Project : Element, IProject
    {
        public Project() : base()
        {
            Elements = new ElementCollection<IElement>();
        }

        public Project(string name) : this()
        {
            Name = name;
        }

        public IEnumerable<IYear> Years { get { return Elements.Elements.OfType<IYear>(); } }

        public IEnumerable<IPayment> Payments { get { return Elements.Elements.OfType<IPayment>(); } }

        public IEnumerable<IPayPattern> PayPatterns { get { return Elements.Elements.OfType<IPayPattern>(); } }
        public IEnumerable<IPaymentInterval> Intervals { get { return Elements.Elements.OfType<IPaymentInterval>().OrderBy(x => (int)x.Type); } }
        public IEnumerable<ICategory> Categories { get { return Elements.Elements.OfType<ICategory>().Where(c => c.IsMainCategory).OrderBy(x => x.SortOrder); } }
        public IEnumerable<ICategory> SubCategories { get { return Elements.Elements.OfType<ICategory>().Where(c => !c.IsMainCategory).OrderBy(x => x.SortOrder); } }
        public IEnumerable<IMonth> Months { get { return Elements.Elements.OfType<IMonth>(); } }
        public IEnumerable<ITransaction> Transactions { get { return Elements.Elements.OfType<ITransaction>(); } }
        public IElementCollection<IElement> Elements { get; set; }

        public override void Delete()
        {
            base.Delete();
            Elements.Elements.ToList().ForEach(e => e.Delete());
        }
    }
}