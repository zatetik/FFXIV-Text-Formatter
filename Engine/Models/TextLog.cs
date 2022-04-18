using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class TextLog : INotifyPropertyChanged
    {
        private string _filePath;
        private string _rawText;
        private string _filteredText;
        public string FilePath 
        { 
            get { return _filePath; }
            set 
            {
                _filePath = value;
                OnPropertyChanged(nameof(FilePath));
            } 
        }
        public string RawText 
        { 
            get { return _rawText; } 
            set
            {
                _rawText = value;
                OnPropertyChanged(nameof(RawText));
            }
        }
        public string FilteredText 
        { 
            get { return _filteredText; } 
            set 
            { 
                _filteredText = value;
                OnPropertyChanged(nameof(FilteredText));
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
