using IData.Constants;
using IData.Interfaces;
using XmlSaver.Constants;

namespace XmlSaver.Data.CustomDataTypes
{
    public class CategoryXmlElement : NodeXmlElement<ICategory>
    {
        public CategoryXmlElement(string xmlTag, ICategory value)
            : base(xmlTag, value)
        {
        }

        public override void AddCustomAttributes()
        {
            Attributes.Add(XmlIds.Description, new SaveableAttribute(XmlIds.Description, Value.Description));
            Attributes.Add(XmlIds.Type, new SaveableAttribute(XmlIds.Type, (int)Value.CategoryType));
            Attributes.Add(XmlIds.SortOrder, new SaveableAttribute(XmlIds.SortOrder, Value.SortOrder));
            Attributes.Add(XmlIds.IsMain, new SaveableAttribute(XmlIds.IsMain, Value.IsMainCategory));
        }

        public override void ReadAttributes()
        {
            base.ReadAttributes();
            Value.Description = GetAttribute(XmlIds.Description).ToString();
            Value.CategoryType = (CategoryType)GetAttribute(XmlIds.Type);
            Value.SortOrder = (int)GetAttribute(XmlIds.SortOrder);
            Value.IsMainCategory = (bool)GetAttribute(XmlIds.IsMain);
        }
    }
}