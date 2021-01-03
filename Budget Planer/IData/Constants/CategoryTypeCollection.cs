using System.Collections.Generic;
using System.Linq;

namespace IData.Constants
{
    public class CategoryTypeCollection
    {
        public CategoryTypeCollection()
        {
        }

        public IEnumerable<CategoryType> All
        {
            get
            {
                var elements = new List<CategoryType>();

                elements.AddRange(MainCateogires);
                elements.AddRange(SubCategories);

                return elements;
            }
        }

        public IEnumerable<CategoryType> MonthlyBills
        {
            get
            {
                return new[]
                {
                CategoryType.Income,
                CategoryType.RentAndHousing,
                CategoryType.InternetAndMobile,
                CategoryType.Insurance,
                CategoryType.Bankbalance
            };
            }
        }

        public IEnumerable<CategoryType> FoodCategories
        {
            get
            {
                return new[]
                {
                    CategoryType.FoodAndHousehold,
                    CategoryType.Food,
                    CategoryType.DogFood,
                    CategoryType.Fuel,
                };
            }
        }

        public IEnumerable<CategoryType> MainCateogires
        {
            get
            {
                return new[]
                {
                    CategoryType.Income,
                    CategoryType.RentAndHousing,
                    CategoryType.InternetAndMobile,
                    CategoryType.FoodAndHousehold,
                    CategoryType.Insurance,
                    CategoryType.CreditCard,
                    CategoryType.ExtraExpenses,
                    CategoryType.UnexpectedExpenses,
                    CategoryType.Bankbalance
                };
            }
        }

        public IEnumerable<CategoryType> SubCategories
        {
            get
            {
                return new[]
              {
                    CategoryType.Sallary,
                    CategoryType.Rent,
                    CategoryType.OperatingCosts,
                    CategoryType.WaterAndHeating,
                    CategoryType.Electricity,
                    CategoryType.Tv,
                    CategoryType.Gis,
                    CategoryType.Phone,
                    CategoryType.Streaming,
                    CategoryType.Food,
                    CategoryType.DogFood,
                    CategoryType.Fuel,
                    CategoryType.CarInsurance,
                    CategoryType.Lease,
                    CategoryType.CreditAppartment,
                };
            }
        }

        public IEnumerable<CategoryType> CreditCardCategories
        {
            get
            {
                var elements = new List<CategoryType>();

                elements.Add(CategoryType.CreditCard);
                elements.AddRange(SubCategories);

                return elements;
            }
        }

        public IEnumerable<CategoryType> ExpectedUnexpectedCategories
        {
            get
            {
                var elements = new List<CategoryType>();

                elements.Add(CategoryType.ExtraExpenses);
                elements.Add(CategoryType.UnexpectedExpenses);
                elements.AddRange(SubCategories);

                return elements;
            }
        }
    }
}