﻿using Data.Classes;
using IData.Constants;
using IData.Interfaces;
using IData.Services;
using System.Linq;

namespace Data.Services
{
    public class PaymentFactory : ElementFactory, IPaymentFactory
    {
        public override IElement CreateEmpty()
        {
            var factory = new PayPatternFactory() { Project = Project };

            var pattern = factory.CreateEmpty() as IPayPattern;
            var payment = new Payment(Project.CurrentProject.Categories.First(), pattern);

            Project.CurrentProject.Elements.AddElement(payment);
            return payment;
        }

        public IPayment Create(IMonth month)
        {
            var interval = Project.CurrentProject.Intervals.First();
            var factory = new PayPatternFactory() { Project = Project };

            var pattern = factory.Create(interval, month.MonthType);

            var payment = new Payment(Project.CurrentProject.Categories.First(), pattern);
            Project.CurrentProject.Elements.AddElement(payment);

            return payment;
        }

        public IPayment Create(string name, ICategory category, double amount, PaymentIntervalType intervalType, MonthEnum startingMonth, ICategory subCategory = null)
        {
            var interval = Project.CurrentProject.Intervals.Single(i => i.Type == intervalType);
            var factory = new PayPatternFactory() { Project = Project };
            var pattern = factory.Create(interval, startingMonth);

            var payment = new Payment(name, category, amount, pattern, subCategory);
            Project.CurrentProject.Elements.AddElement(payment);
            return payment;
        }

        public IPayment Copy(IPayment original)
        {
            var newPayment = CreateEmpty() as IPayment;

            newPayment.Name = original.Name;
            newPayment.PayPattern = original.PayPattern;
            newPayment.Category = original.Category;
            newPayment.SubCategory = original.SubCategory;

            newPayment.Amount = original.Amount;

            return newPayment;
        }
    }
}