namespace XmlSaver.Data
{
    public class SaveableAttribute
    {
        public SaveableAttribute(string attributeTag)
        {
            AttributeTag = attributeTag;
        }

        public SaveableAttribute(string attributeTag, object data)
        {
            Data = data;
            AttributeTag = attributeTag;
        }

        public object Data { get; set; }
        public string AttributeTag { get; set; }
    }
}