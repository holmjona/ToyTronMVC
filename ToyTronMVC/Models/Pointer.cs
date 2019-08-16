using Microsoft.VisualBasic;
namespace ToyTron
{
    //Like a memory location but needs to advance for the Emulator to use it.
    public class Pointer : MemoryLocation
    {
        public void Bind(MemoryLocation memLoc)
        {
            //This is done because the = operator for setting can not be overloaded.
            this.NextLocation = memLoc.NextLocation;
            this.PreviousLocation = memLoc.PreviousLocation;
            this.ID = memLoc.ID;
            this.HasError = memLoc.HasError;
            this.Word(memLoc.Word());
        }
        public static bool  operator  ==(Pointer point, MemoryLocation memLoc)
        {
            return object.ReferenceEquals(point, memLoc);
        }
       public static bool  operator !=(Pointer point, MemoryLocation memLoc)
        {
            return !(object.ReferenceEquals(point, memLoc));
        }
        public void Advance()
        {
            //move pointer to the next location.
            Bind(NextLocation);
        }
    }
}