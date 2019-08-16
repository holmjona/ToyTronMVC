using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace ToyTron
{
    public class ToyTronDAL
    {
        //Connection strings should really be stored in a separate location as to not be a security flaw.
        string readonlyConnection = "server=127.0.0.1;uid=dbusr_ToyTron_Reader;pwd=1cr4tt;database=ToyTron";
        string editonlyConnection = "server=127.0.0.1;uid=dbusr_ToyTron_Writer;pwd=1cw4tt;database=ToyTron";
        public List<Program> getPrograms()
        {
            List<Program> listOfPrograms = new List<Program>();
            //build connection
            SqlConnection conn = new SqlConnection(readonlyConnection);
            //build connection
            //build new command
            SqlCommand comm = new SqlCommand();
            //set command properties
            //set connection to command
            //set stored procedure name
            //set type to stored procedure
            // ERROR: Not supported in C#: WithStatement

            //create data reader pointer
            SqlDataReader myDataReader;
            //open the connection toth ethe database
            conn.Open();
            myDataReader = comm.ExecuteReader();
            //cycle through each record returned
            while (myDataReader.Read())
            {
                Program newProgram = new Program();
                newProgram.fillFromDatabase(myDataReader);
                //add program to list
                listOfPrograms.Add(newProgram);
            }
            //close the connection to the database
            conn.Close();
            return listOfPrograms;
        }
        public Program getProgram(int programID)
        {
            Program programToReturn = new Program();
            //build connection
            SqlConnection conn = new SqlConnection(readonlyConnection);
            //build new command
            SqlCommand comm = new SqlCommand();
            //set command properties
            // ERROR: Not supported in C#: WithStatement

            //create data reader pointer
            SqlDataReader myDataReader;
            //open the connection toth ethe database
            conn.Open();
            myDataReader = comm.ExecuteReader();
            //cycle through each record returned
            while (myDataReader.Read())
            {
                programToReturn.fillFromDatabase(myDataReader);
            }
            //close the connection to the database
            conn.Close();
            programToReturn.AddMemoryToEmulator(getInstructions(programID));
            return programToReturn;
        }
        public Memory getInstructions(int programID)
        {
            Memory myMemory = new Memory();
            //build connection
            SqlConnection conn = new SqlConnection(readonlyConnection);
            //build new command
            SqlCommand comm = new SqlCommand();
            //set command properties
            // ERROR: Not supported in C#: WithStatement

            //create data reader pointer
            SqlDataReader myDataReader;
            //open the connection to the database
            conn.Open();
            myDataReader = comm.ExecuteReader();
            while (myDataReader.Read())
            {
                myMemory.Add(myDataReader["Word"].ToString());
            }
            //close the connection to the database
            conn.Close();
            return myMemory;
        }
        public int addProgram(Program myProgram)
        {
            //build connection
            SqlConnection conn = new SqlConnection(editonlyConnection);
            //create a new parameter to return the new progam ID
            SqlParameter myNewID = new SqlParameter();
            //set the direction of the parameter to be an output from the procedure
            myNewID.Direction = System.Data.ParameterDirection.Output;
            //set the type to an integer
            myNewID.DbType = System.Data.DbType.Int32;
            myNewID.ParameterName = "@ProgramID";
            //build new command
            SqlCommand comm = new SqlCommand();
            //set command properties
            // ERROR: Not supported in C#: WithStatement

            //open the connection to the database
            conn.Open();
            //executes the command that does not return a record set
            comm.ExecuteNonQuery();
            //close the connection to the database
            conn.Close();
            //return the new programID 
            //forced to int because all returns from a database are just of type object.
            return (int)myNewID.Value;
        }
        public int addInstuction(MemoryLocation myLoc, int programID)
        {
            //build connection
            SqlConnection conn = new SqlConnection(editonlyConnection);
            //build new command
            SqlCommand comm = new SqlCommand();
            //set command properties
            // ERROR: Not supported in C#: WithStatement

            int retInt;
            //open the connection to the database
            conn.Open();
            //executes the command that does not return a record set
            retInt = comm.ExecuteNonQuery();
            //close the connection to the database
            conn.Close();
            return retInt;
        }
    }
}