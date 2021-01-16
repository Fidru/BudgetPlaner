using IData.Interfaces;
using System;
using System.ComponentModel;

namespace UI.Wpf.ViewModel
{
    public class ElementViewModel<T> : IElementViewModel, INotifyPropertyChanged where T : IElement
    {
        public ElementViewModel(T element)
        {
            Element = element;
        }

        public T Element { get; set; }

        public Guid Id
        {
            get { return Element.Id; }
            set { Element.Id = value; }
        }

        public string Name
        {
            get { return Element.Name; }
            set
            {
                Element.Name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        protected void NotifyPropertyChanged(object sender, string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(sender, new PropertyChangedEventArgs(info));
            }
        }
    }
}