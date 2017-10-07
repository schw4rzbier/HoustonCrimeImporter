using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HoustonCrimeImporter.Models;

namespace HoustonCrimeImporter
{
    public class HoustonCrimeFlatFileWalker
    {
        private const int LinesPerSave = 200;
        private readonly HoustonCrimeContext _context;
        private string _parseLine;
        private StreamReader _reader;
        private string _dataSetIdentifier;
        private int _myCounter;
        private List<string> _stringList;
        private int _listIteratorCount;

        public HoustonCrimeFlatFileWalker(string fileName, string dataSetIdentifier)
        {
            _context = new HoustonCrimeContext();
            _dataSetIdentifier = dataSetIdentifier;
            var hpdFileStream = File.OpenRead(fileName);
            _myCounter = 0;
            _listIteratorCount = 0;
            _reader = new StreamReader(hpdFileStream);
            _stringList = new List<string>();
            _reader.ReadLine(); //throw away the column definition row
            while (!_reader.EndOfStream)
            {
                _stringList.Add(_reader.ReadLine());
            }
        }

        public int CurrentLine => _myCounter;
        public int FileLineCount => _stringList.Count;
        public float Progress => 100 * _listIteratorCount / FileLineCount;

        public async Task ProcessFileAsync()
        {
            _myCounter = 1;

            foreach (var inputLine in _stringList)
            {
                DateTime throwaway;
                if (inputLine.Length >= 22 && DateTime.TryParse(inputLine.Substring(0, 22), out throwaway))
                {
                    if (!string.IsNullOrWhiteSpace(_parseLine))
                    {
                        _context.Incidents.AddOrUpdate(new Incident(_parseLine, _dataSetIdentifier));
                        _parseLine = "";
                        if (_myCounter % LinesPerSave == 0)
                        {
                            await _context.SaveChangesAsync();
                        }
                        _myCounter++;
                    }

                    _parseLine = inputLine;
                }
                else
                {
                    _parseLine += "," + inputLine;
                }
                _listIteratorCount++;
            }


            if (!string.IsNullOrWhiteSpace(_parseLine))
            {
                _context.Incidents.AddOrUpdate(new Incident(_parseLine, _dataSetIdentifier));
                _myCounter++;
            }
            await _context.SaveChangesAsync();

        }
    }
}
