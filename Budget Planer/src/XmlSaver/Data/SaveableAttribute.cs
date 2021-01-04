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
            AttributeTag = attributeTag;
            Data = data;
        }

        public string AttributeTag { get; set; }
        public object Data { get; set; }
    }
}