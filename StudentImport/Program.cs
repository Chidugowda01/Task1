﻿using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.IO;
using CsvHelper;
using ExcelDataReader;

namespace StudentImport
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=.;Initial Catalog=chhida;Integrated Security=True";
            string csvPath = "C:\\Task\\StudentDetails.csv"; 
            using (SqlConnection connection = new SqlConnection(connectionString))
            { 
                connection.Open(); 
                using (SqlCommand command = new SqlCommand("INSERT INTO StudentData(ExternalStudentID, FirstNmae, LastName,DOB, SSN, Addres, City, Staate, Email, MARITALSTATUS) VALUES (@ExternalStudentID, @FirstName, @LastNmae, @DOB, @SSN, @Addres, @City, @Staate, @Email, @MARITALSTATUS)", connection))
                { 
                    using (CsvReader csvReader = new CsvReader(new StreamReader(csvPath), CultureInfo.InvariantCulture))
                    { 
                        DataTable dataTable = new DataTable();
                        dataTable.Load((IDataReader)csvReader); 
                        foreach (DataRow row in dataTable.Rows)
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@ExternalStudentID", row["ExternalStudentID"].ToString());
                            command.Parameters.AddWithValue("@FirstName", row["FirstName"].ToString());
                            command.Parameters.AddWithValue("@LastName", row["LastName"].ToString());
                            command.Parameters.AddWithValue("@DOB", row["DOB"].ToString());
                            command.Parameters.AddWithValue("@SSN", row["SSN"].ToString());
                            command.Parameters.AddWithValue("@Addres", row["Addres"].ToString());
                            command.Parameters.AddWithValue("@City", row["City"].ToString());
                            command.Parameters.AddWithValue("@Staate", row["Staate"].ToString());
                            command.Parameters.AddWithValue("@Email", row["Email"].ToString());
                            command.Parameters.AddWithValue("@MARITALSTATUS", row["CMARITALSTATUS"].ToString());
                            command.ExecuteNonQuery();
                        }
                    }
                } 
                connection.Close();
            }
        }
    }
}
