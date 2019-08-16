using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Collections;
using System;
namespace ToyTron
{
    public class Memory
    {
        #region "Private Variables"
        private MemoryLocation _FirstLocation;
        private MemoryLocation _CurrentLocation;
        #endregion
        private MemoryLocation _LastLocation;
        #region "Public Properties"
        public MemoryLocation FirstLocation
        {
            //returns the first memory location
            get { return _FirstLocation; }
        }
        public MemoryLocation CurrentLocation
        {
            //returns the current memory location
            get { return _CurrentLocation; }
        }
        public MemoryLocation LastLocation
        {
            //returns the last location
            get { return _LastLocation; }
        }
        public List<MemoryLocation> Locations()
        {
            //returns all memory locations in order
                List<MemoryLocation> retList = new List<MemoryLocation>();
                MemoryLocation loc = _FirstLocation;
                while (loc != null)
                {
                    retList.Add(loc);
                    loc = loc.NextLocation;
                }
                return retList;
        }
        public MemoryLocation Locations(int index)
        {
            //gets a memory location by ID
          
                addLocationsToIndex(index);
                // add locations until given index is filled
                foreach (MemoryLocation loc in Locations())
                {
                    if (loc.ID == index)
                    {
                        return loc;
                    }
                }
                return null;
          
        }
        #endregion
        #region "Public Functions"
        public int Count()
        {
            //returns the count of memory locations
            int countInt = 0;
            MemoryLocation loc = _FirstLocation;
            while (loc != null)
            {
                countInt += 1;
                loc = loc.NextLocation;
            }
            return countInt;
        }
        #endregion
        #region "Public Sub"
        public void Add(MemoryLocation memLoc)
        {
            //public wrapper for adding a memory location
            try
            {
                this.addLocationToList(memLoc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Add(string wordValue)
        {
            //public wrappper for adding a word value
            MemoryLocation newLoc = new MemoryLocation();
            int result;
            try
            {
                newLoc.Word(wordValue);

                result = 1;
            }
            catch
            {
                newLoc.Word("0000");
                newLoc.HasError = true;
                result = -1;
            }
            this.Add(newLoc);
            return result;
        }
        public void AddAfter(int index, MemoryLocation memLoc)
        {
            //add location after a given index in the list
            try
            {
                _CurrentLocation = Locations(index);
                this.addLocationToList(memLoc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AddAfter(int index, string wordValue)
        {
            //add given word after given index in the list.
            MemoryLocation newLoc = new MemoryLocation();
            newLoc.Word(wordValue);
            this.AddAfter(index, newLoc);
        }
        public void Remove(int index)
        {
            //public wrapper for removing a location
            Remove(Locations(index));
        }
        public void Remove(MemoryLocation memLoc)
        {
            //public wrapper for removing a location
            _CurrentLocation = memLoc;
            removeCurrentLocation();
            renumberMemoryLocations();
        }
        public void moveToNextLocation()
        {
            //move along memory locations to next in list from current location
            if (_CurrentLocation != null)
            {
                if (_CurrentLocation.NextLocation != null)
                {
                    _CurrentLocation = _CurrentLocation.NextLocation;
                }
            }
        }
        public void moveToPreviousLocation()
        {
            //move along memory location to previous in list from the current location
            if (_CurrentLocation != null)
            {
                if (_CurrentLocation.PreviousLocation != null)
                {
                    _CurrentLocation = _CurrentLocation.PreviousLocation;
                }
            }
        }
        public void removeCurrentLocation()
        {
            //removes current location from list
            if (_CurrentLocation != null)
            {
                if (_CurrentLocation.PreviousLocation != null)
                {
                    if (_CurrentLocation.NextLocation != null)
                    {
                        removeCurrentLocationFromList();
                    }
                    else
                    {
                        removeLastLocationFromList();
                    }
                }
                else
                {
                    if (_CurrentLocation.NextLocation != null)
                    {
                        removeFirstLocationFromList();
                    }
                    else
                    {
                        removeOnlyLocationFromList();
                    }
                }
            }
            renumberMemoryLocations();
        }
        #endregion
        #region "Private Subs"
        private void addLocationToList(MemoryLocation n)
        {
            //add memory location to list
            if (_FirstLocation == null)
            {
                addLocationToStartOfList(n);
            }
            else
            {
                if (_CurrentLocation.NextLocation != null)
                {
                    addLocationToListAfterCurrentLocation(n);
                }
                else
                {
                    addLocationToEndOfList(n);
                }
            }
            renumberMemoryLocations();
        }
        private void addLocationToStartOfList(MemoryLocation n)
        {
            //add location to the start of the list
            if (_CurrentLocation == null)
            {
                _LastLocation = n;
            }
            _FirstLocation = n;
            _CurrentLocation = n;
        }
        private void addLocationToEndOfList(MemoryLocation n)
        {
            // add location to the end of the list
            n.PreviousLocation = _LastLocation;
            n.PreviousLocation.NextLocation = n;
            _LastLocation = n;
            _CurrentLocation = n;
        }
        private void addLocationToListAfterCurrentLocation(MemoryLocation n)
        {
            // add location to a specified location in the list
            n.PreviousLocation = _CurrentLocation;
            n.NextLocation = _CurrentLocation.NextLocation;
            _CurrentLocation.NextLocation = n;
            _CurrentLocation = n;
        }
        private void addLocationsToIndex(int indexToStopAt)
        {
            //add locations to the end of list, stopping at the desired index
            if (Locations().Count < indexToStopAt + 1)
            {
                addLocationToEndOfList(new MemoryLocation("0000"));
                renumberMemoryLocations();
                addLocationsToIndex(indexToStopAt);
            }
        }
        private void removeOnlyLocationFromList()
        {
            //if only one location in list remove it.
            _FirstLocation = null;
            _CurrentLocation = null;
            _LastLocation = null;
        }
        private void removeFirstLocationFromList()
        {
            //remove the first location in list
            _FirstLocation = _CurrentLocation.NextLocation;
            _FirstLocation.PreviousLocation = null;
            _CurrentLocation = _FirstLocation;
        }
        private void removeCurrentLocationFromList()
        {
            //remove location from the middle of the list
            _CurrentLocation.NextLocation.PreviousLocation = _CurrentLocation.PreviousLocation;
            _CurrentLocation.PreviousLocation.NextLocation = _CurrentLocation.NextLocation;
            _CurrentLocation = _CurrentLocation.PreviousLocation;
        }
        private void removeLastLocationFromList()
        {
            //remove location from the end of the list
            _LastLocation = _CurrentLocation.PreviousLocation;
            _LastLocation.NextLocation = null;
            _CurrentLocation = _LastLocation;
        }
        private void addLocationToArrayList(MemoryLocation curLocation, ref ArrayList alLocations)
        {
            //add locations to array list given
            if (curLocation == null)
            {
                //do nothing
            }
            else
            {
                string s = "";
                s += curLocation.Word().WordString;
                alLocations.Add(s);
                addLocationToArrayList(curLocation.NextLocation,ref alLocations);
            }
        }
        private void addLocationToLocationList(MemoryLocation curLocation, ref System.Collections.Generic.List<MemoryLocation> alLocations)
        {
            //Add location to Generic List given
            if (curLocation == null)
            {
                //do nothing
            }
            else
            {
                alLocations.Add(curLocation);
                addLocationToLocationList(curLocation.NextLocation,ref alLocations);
            }
        }
        private void renumberMemoryLocations()
        {
            //set the id of each location to proper numbers zero based index.
            MemoryLocation memLoc = _FirstLocation;
            int memLocCounter = 0;
            while (memLoc != null)
            {
                memLoc.ID = memLocCounter;
                memLoc = memLoc.NextLocation;
                memLocCounter += 1;
            }
            //_CurrentLocation = _LastLocation
        }
        #endregion


    }
}