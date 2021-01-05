using IData.Interfaces;
using IData.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using XmlSaver.Constants;
using XmlSaver.Data;

namespace XmlSaver.Save
{
    public class MyXmlSaver
    {
        public void Save(IProject project)
        {
            var fs = File.Open(SavePaths.XmlFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);

            XmlTextWriter writer = new XmlTextWriter(fs, Encoding.UTF8) { Formatting = Formatting.Indented };

            writer.WriteStartDocument();
            writer.WriteStartElement(XmlIds.Finance);

            SaveElements(new[] { project }, XmlIds.Projects, XmlIds.Project, writer);

            SaveElements(project.Intervals, XmlIds.Intervals, XmlIds.Interval, writer);
            SaveElements(project.PayPatterns, XmlIds.PayPatterns, XmlIds.PayPattern, writer);
            SaveElements(project.Categories.Concat(project.SubCategories), XmlIds.Categories, XmlIds.Category, writer);

            SaveElements(project.Payments, XmlIds.Payments, XmlIds.Payment, writer);
            SaveElements(project.Months, XmlIds.Months, XmlIds.Month, writer);
            SaveElements(project.Transactions, XmlIds.Transactions, XmlIds.Transaction, writer);
            SaveElements(project.Years, XmlIds.Years, XmlIds.Year, writer);

            writer.WriteEndElement();

            writer.Close();
        }

        private void SaveElements(IEnumerable<IElement> elements, string groupTag, string itemTag, XmlTextWriter writer)
        {
            writer.WriteStartElement(groupTag);

            foreach (var element in elements)
            {
                CreateSaveableXmlElement(itemTag, element).WriteAttribute(writer);
            }

            writer.WriteEndElement();
        }

        private SaveableXmlElement CreateSaveableXmlElement(string itemTag, IElement element)
        {
            return new SaveableXmlElement(itemTag, new SaveableAttribute(itemTag, element));
        }

        public IProject Read(IEnumerable<IService> services)
        {
            var reader = XmlReader.Create(SavePaths.XmlFile);

            reader.Read();
            SkipWrongTags(XmlIds.Finance, reader);

            return ReadProject(services, reader);
        }

        private IProject ReadProject(IEnumerable<IService> services, XmlReader reader)
        {
            var elements = new List<IElement>();
            ICurentProjectService currentProject = services.GetService<ICurentProjectService>();

            var projects = ReadElements<IProjectFactory>(services, XmlIds.Projects, XmlIds.Project, reader);
            var firstProject = projects.First() as IProject;
            currentProject.CurrentProject = firstProject;

            elements.AddRange(ReadElements<IPaymentIntervalFactory>(services, XmlIds.Intervals, XmlIds.Interval, reader));
            elements.AddRange(ReadElements<IPayPatternFactory>(services, XmlIds.PayPatterns, XmlIds.PayPattern, reader));
            elements.AddRange(ReadElements<ICategoryFactory>(services, XmlIds.Categories, XmlIds.Category, reader));
            elements.AddRange(ReadElements<IPaymentFactory>(services, XmlIds.Payments, XmlIds.Payment, reader));

            elements.AddRange(ReadElements<IMonthFactory>(services, XmlIds.Months, XmlIds.Month, reader));
            elements.AddRange(ReadElements<ITransactionFactory>(services, XmlIds.Transactions, XmlIds.Transaction, reader));
            elements.AddRange(ReadElements<IYearFactoy>(services, XmlIds.Years, XmlIds.Year, reader));

            reader.Close();

            ConnectIdsToElements(firstProject);

            return firstProject;
        }

        private void ConnectIdsToElements(IProject project)
        {
            project.Elements.Elements.OrderBy(e => e.LoadingOrder).ToList().ForEach(e => e.ConnectElements(project));
        }

        private IEnumerable<IElement> ReadElements<T>(IEnumerable<IService> services, string groupTag, string itemTag, XmlReader reader)
        {
            var elements = new List<IElement>();

            SkipWrongTags(groupTag, reader);
            var factory = services.GetService<T>() as IElementFactory;

            reader.Read();
            reader.Read();

            while (reader.Name == itemTag)
            {
                var element = factory.GetCreateEmpty();

                var xmlReader = CreateSaveableXmlElement(itemTag, element);
                xmlReader.ReadAttribute(reader);

                elements.Add((IElement)xmlReader.Value.Data);

                reader.Read();
                reader.Read();
            }

            return elements;
        }

        public static void SkipWrongTags(string tag, XmlReader reader)
        {
            while (reader.Name != tag)
            {
                reader.Read();
                CheckFormat(reader);
            }
        }

        private static void CheckFormat(XmlReader reader)
        {
            if (reader.EOF)
            {
                throw new Exception("End of File");
            }
        }
    }
}