using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;


//İnternetten bulduğum bir 'Employee' isimli csv dosya verilerini C# ile alıp istenilen database(test isimli) içerisine
//gerekli tablolaların oluşturulması ve verilerin ilgili yerlere yerleştirilmesi.

//Burada database bağlantısını Windows Authentication ile bağlantı gerçekleştirdim. Fakat izinli bir user tanımlandıktan
//sonra Sql Server Authenticaion üzerinden ilgili connection string içerisine kullanıcı adı ve password bilgisi girerek
//erişilebilir.

namespace Soru5
{
    internal class Program
    {
        static void Main(string[] args)
        {
			List<string> sqlcumle = new List<string>();	
            string sql = "";
            var satir= File.ReadAllLines("../../../Employee.csv");
			bool create= false;
            for(int i=0; i<satir.Length;i++)
            {
               var kelime= satir[i].Split(';');
                if (create == false)
                {
                  sql = $@"CREATE TABLE [dbo].[Employees](
							    	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
							    	[LastName] [nvarchar](20) NOT NULL,
							    	[FirstName] [nvarchar](10) NOT NULL,
							    	[Title] [nvarchar](30) NULL,
							    	[TitleOfCourtesy] [nvarchar](25) NULL,
							    	[BirthDate] [datetime] NULL,
							    	[HireDate] [datetime] NULL,
							    	[Address] [nvarchar](60) NULL,
							    	[City] [nvarchar](15) NULL,
							    	[Region] [nvarchar](15) NULL,
							    	[PostalCode] [nvarchar](10) NULL,
							    	[Country] [nvarchar](15) NULL,
							    	[HomePhone] [nvarchar](24) NULL,
							    	[Extension] [nvarchar](4) NULL,
							    	[Notes] [ntext] NULL,
							    	[ReportsTo] [int] NULL
							    	CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
							    	(
							    		[EmployeeID] ASC
							    	))";
				  sqlcumle.Add(sql);
			 	  create = true;
                }
				sql = $@"INSERT INTO [dbo].[Employees]
                                                         ([LastName]
                                                         ,[FirstName]
                                                         ,[Title]
                                                         ,[TitleOfCourtesy]
                                                         ,[BirthDate]
                                                         ,[HireDate]
                                                         ,[Address]
                                                         ,[City]
                                                         ,[Region]
                                                         ,[PostalCode]
                                                         ,[Country]
                                                         ,[HomePhone]
                                                         ,[Extension]
                                                         ,[Notes]
                                                         ,[ReportsTo])
                                                 VALUES(
                                                          '{kelime[1]}',
                                                          '{kelime[2]}',
                                                          '{kelime[3]}',
                                                          '{kelime[4]}',
                                                          convert(datetime,'{kelime[5]}',102),
                                                          convert(datetime,'{kelime[6]}',102),
                                                          '{kelime[7]}',
                                                          '{kelime[8]}',
                                                          '{kelime[9]}',
                                                          '{kelime[10]}',   
                                                          '{kelime[11]}',
                                                          '{kelime[12]}',
                                                          '{kelime[13]}',
                                                          '{kelime[14]}',                            
                                                          {(kelime[15]=="NULL"? 0 : int.Parse(kelime[15]))}
                                                        )";
                sqlcumle.Add(sql);             
            }
                      foreach (var item in sqlcumle)
                      {
                          sqlcumletransfer(item);
                      }      
        }

        public static int sqlcumletransfer(string sql)
        {
            int affectedrow = 0;
            string conadres = "Server=.;Database=test;Trusted_Connection=true;trust server certificate=true";
            SqlConnection con = new SqlConnection(conadres);
            
            try
            {
                con.Open();
                SqlCommand sqlcmd = new SqlCommand(sql, con);
                affectedrow= sqlcmd.ExecuteNonQuery();
                return affectedrow; 
                
            }
            catch (Exception ex)
            {
                throw new Exception($@"Hata: {ex.Message}");
            }
            finally
            {
                con.Close();
            }
            
        }
    }
}
