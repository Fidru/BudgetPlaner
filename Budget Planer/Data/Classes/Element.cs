using IData.Interfaces;
using System;

namespace Data.Classes
{
    public class Element : IElement
    {
        public Element()
        {
            Id = Guid.NewGuid();
            IsNew = true;
            Name = GetDefaultName;
            ChangedAt = DateTime.Now;
            CreatedAt = DateTime.Now;
        }

        public string Name { get; set; }
        public DateTime? ChangedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid Id { get; set; }
        public bool IsNew { get; set; }

        public int LoadingOrder
        {
            get
            {
                if (this is ICategory)
                {
                    return 10;
                }
                else if (this is IPaymentInterval)
                {
                    return 40;
                }
                else if (this is IPayPattern)
                {
                    return 50;
                }
                else if (this is IPayment)
                {
                    return 60;
                }
                else if (this is ITransaction)
                {
                    return 70;
                }
                else if (this is IMonth)
                {
                    return 80;
                }
                else if (this is IYear)
                {
                    return 90;
                }
                else if (this is IProject)
                {
                    return 100;
                }

                return 0;
            }
        }

        public bool IsDeleted { get; set; }

        internal string GetDefaultName
        {
            get
            {
                string name = "New ";

                if (this is ICategory)
                {
                    name += "Category";
                }
                if (this is IMonth)
                {
                    name += "Month";
                }
                if (this is IPayPattern)
                {
                    name += "Pay Pattern";
                }
                if (this is IProject)
                {
                    name += "Project";
                }
                if (this is IPayment)
                {
                    name += "Payment";
                }
                if (this is IYear)
                {
                    name += "Year";
                }
                if (this is ITransaction)
                {
                    name += "Transaction";
                }

                return name;
            }
        }

        public virtual void ConnectElements(IProject project)
        {
        }

        public override string ToString()
        {
            return Name;
        }

        public virtual void Delete()
        {
            IsDeleted = true;
        }
    }
}