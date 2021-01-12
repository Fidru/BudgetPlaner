using IData.Interfaces;
using System.ComponentModel;
using System;

namespace UI.Wpf.ViewModel
{
    public class ElementViewModel : INotifyPropertyChanged
    {
        public IElement Element { get; set; }

        public Guid Id
        {
            get { return Element.Id; }
        }

        public string Name
        {
            get { return Element.Name; }
            set { Element.Name = value; }
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