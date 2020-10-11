using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Lawyer_Project
{
     class sqlbaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-8NNOKBJ\\SQLEXPRESS;Initial Catalog=DbLawyer;Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
