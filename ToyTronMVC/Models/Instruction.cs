using Microsoft.VisualBasic;
using System.Collections;
namespace ToyTron
{
    public class Instruction
    {
        //Class is for easy way of knowing what integer values returned from Emulator Object mean what Instruction.
        #region "Private Subs"
        private static void fillValues(Hashtable _Values)
        {
            //fill hashtable with instructions 
            //TODO: make this pull from the database in the future.
            Hashtable readInstructions = new Hashtable();
            readInstructions.Add("Command", "Read");
            readInstructions.Add("Explanation", "Read a word from the keyboard (input box) into a specific location in memory.");
            _Values.Add(10, readInstructions);

            Hashtable writeInstructions = new Hashtable();
            writeInstructions.Add("Command", "Write");
            writeInstructions.Add("Explanation", "Write a word from a specific memory location to the screen (msgbox).");
            _Values.Add(11, writeInstructions);

            Hashtable loadInstructions = new Hashtable();
            loadInstructions.Add("Command", "Load");
            loadInstructions.Add("Explanation", "Load a word from a specific location in memory into the accumulator in the CPU.");
            _Values.Add(20, loadInstructions);

            Hashtable storeInstructions = new Hashtable();
            storeInstructions.Add("Command", "Store");
            storeInstructions.Add("Explanation", "Store a word from the accumulator into a specific location in memory.");
            _Values.Add(21, storeInstructions);

            Hashtable addInstructions = new Hashtable();
            addInstructions.Add("Command", "Add");
            addInstructions.Add("Explanation", "Add a word from a specific location in memory to the word in the accumulator, leaving the results in the accumulator.");
            _Values.Add(30, addInstructions);

            Hashtable subtractInstructions = new Hashtable();
            subtractInstructions.Add("Command", "Subtract");
            subtractInstructions.Add("Explanation", "Subtract a word from a specific location in memory from the word in the accumulator, leaving the results in the accumulator.");
            _Values.Add(31, subtractInstructions);

            Hashtable divideInstructions = new Hashtable();
            divideInstructions.Add("Command", "Divide");
            divideInstructions.Add("Explanation", "Divide a word from a specific location in memory into the word in the accumulator, leaving the results in the accumulator.");
            _Values.Add(32, divideInstructions);

            Hashtable multiplyInstructions = new Hashtable();
            multiplyInstructions.Add("Command", "Multiply");
            multiplyInstructions.Add("Explanation", "Multiply a word from a specific location in memory by the word in the accumulator, leaving the results in the accumulator.");
            _Values.Add(33, multiplyInstructions);

            Hashtable branchInstructions = new Hashtable();
            branchInstructions.Add("Command", "Branch");
            branchInstructions.Add("Explanation", "Branch (goto) a specific location in memory.");
            _Values.Add(40, branchInstructions);

            Hashtable branchNegInstructions = new Hashtable();
            branchNegInstructions.Add("Command", "BranchNegative");
            branchNegInstructions.Add("Explanation", "Branch to a specific location in memory IFF (If and only if) the value in the accumulator is negative");
            _Values.Add(41, branchNegInstructions);

            Hashtable branchZeroInstructions = new Hashtable();
            branchZeroInstructions.Add("Command", "BranchZero");
            branchZeroInstructions.Add("Explanation", "Branch to a specific location in memory IFF the value in the accumulator is Zero.");
            _Values.Add(42, branchZeroInstructions);

            Hashtable haltInstructions = new Hashtable();
            haltInstructions.Add("Command", "Halt");
            haltInstructions.Add("Explanation", "Halt - The program has completed its task.");
            _Values.Add(43, haltInstructions);

        }
        private static string getTextNeeded(string whatIsNeeded, int instr)
        {
            //returns either the command or expliantion from the hashtable for a given instruction)
            Hashtable _Values = new Hashtable();
            fillValues(_Values);
            Hashtable smallHash = (Hashtable)_Values[instr];
            if (smallHash == null)
                return "Unknown";
            return (string)smallHash[whatIsNeeded];
        }
        #endregion
        #region "Public Properties"
       public static string Text(int instructionValue)
        {
            //gets text value for a given instruction value 
            // e.x. 10 returns "Read"
            return getTextNeeded("Command", instructionValue);
        }
        public static string LongText(int instructionValue)
        {
            //gets the text description of instruction 
            // e.x. 10 returns "Read a word from input ..."
            return getTextNeeded("Explanation", instructionValue);
        }
        static Hashtable Values
        {
            //gets all instructions as a hash table
            get
            {
                Hashtable _Values = new Hashtable();
                fillValues(_Values);
                return _Values;
            }
        }
        public static SortedList SortedValues
        {
            //returns all instructions sorted by instruction value
            get
            {
                Hashtable _Values = new Hashtable();
                fillValues(_Values);
                SortedList retValues = new SortedList();
                foreach (int keyValue in _Values.Keys)
                {
                    retValues.Add(keyValue, _Values[keyValue]);
                }
                return retValues;
            }
        }
        #endregion
        #region "Shared Properties"

        public const int Read = 10;
        public const int Write = 11;
        public const int Load = 20;
        public const int Store = 21;
        public const int Add = 30;
        public const int Substract = 31;
        public const int Divide = 32;
        public const int Multiply = 33;
        public const int Branch = 40;
        public const int BranchNegative = 41;
        public const int BranchZero = 42;
        public const int Halt = 43;


        //public static int Read {
        //    get { return 10; }
        //}
        //public static int Write
        //{
        //    get { return 11; }
        //}

        //public static int Load
        //{
        //    get { return 20; }
        //}
        //public static int Store
        //{
        //    get { return 21; }
        //}
        //public static int Add
        //{
        //    get { return 30; }
        //}
        //public static int Substract
        //{
        //    get { return 31; }
        //}
        //public static int Divide
        //{
        //    get { return 32; }
        //}
        //public static int Multiply
        //{
        //    get { return 33; }
        //}
        //public static int Branch
        //{
        //    get { return 40; }
        //}
        //public static int BranchNegative
        //{
        //    get { return 41; }
        //}
        //public static int BranchZero
        //{
        //    get { return 42; }
        //}
        //public static int Halt
        //{
        //    get { return 43; }
        //}
        #endregion
    }
}