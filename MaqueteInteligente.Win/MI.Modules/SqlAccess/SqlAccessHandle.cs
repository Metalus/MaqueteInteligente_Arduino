using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace MI.Modules.SqlAccess
{
    public class SqlAccessHandle : IDisposable
    {
        OleDbConnection connection;
        OleDbCommand command;
        public SqlAccessHandle()
        {
            connection = new OleDbConnection(Properties.Settings.Default.ConsumoConnectionString);
            connection.Open();
            command = new OleDbCommand();
            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;

        }

        public Dictionary<DateTime, float> ConsumoAgua(DateTime inicio)
        {
            return ConsumoAgua(inicio, DateTime.Today);
        }

        public Dictionary<DateTime, float> ConsumoAgua(DateTime inicio, DateTime final)
        {
            Dictionary<DateTime, float> Consumo = new Dictionary<DateTime, float>();
            this.command.Parameters.Clear();
            this.command.CommandText = "SELECT * FROM ConsumoAgua WHERE Data BETWEEN @dataI AND @dataF";
            this.command.Parameters.Add("@dataI", OleDbType.Date).Value = inicio.Date;
            this.command.Parameters.Add("@dataF", OleDbType.Date).Value = final.Date;
            var Reader = this.command.ExecuteReader();
            while (Reader.Read())
                Consumo.Add((DateTime)Reader["Data"], Convert.ToSingle(Reader["Volume"]));

            return Consumo;
        }

        public Dictionary<DateTime, float> ConsumoEnergia(DateTime inicio)
        {
            return ConsumoEnergia(inicio, DateTime.Now);
        }

        public Dictionary<DateTime, float> ConsumoEnergia(DateTime inicio, DateTime final)
        {
            Dictionary<DateTime, float> Consumo = new Dictionary<DateTime, float>();
            this.command.Parameters.Clear();
            this.command.CommandText = "SELECT * FROM ConsumoEnergia WHERE Data BETWEEN @dataI AND @dataF";
            this.command.Parameters.Add("@dataI", OleDbType.Date).Value = inicio.Date;
            this.command.Parameters.Add("@dataF", OleDbType.Date).Value = final.Date;
            var Reader = this.command.ExecuteReader();
            while (Reader.Read())
                Consumo.Add((DateTime)Reader["Data"], Convert.ToSingle(Reader["Consumo"]));

            return Consumo;
        }

        public bool AtualizarConsumoEnergia(DateTime data, float Valor)
        {
            this.command.Parameters.Clear();
            this.command.CommandText = "SELECT COUNT(*) FROM ConsumoEnergia WHERE Data=@data";
            this.command.Parameters.Add("@data", OleDbType.Date).Value = data.Date;
            this.command.Parameters.Add("@consumo", OleDbType.Single).Value = Valor;
            this.command.CommandText = (int)this.command.ExecuteScalar() == 1 ?
                                       "UPDATE ConsumoEnergia SET Consumo=Consumo+@consumo WHERE Data=@data" :
                                       "INSERT INTO ConsumoEnergia (Data,Consumo) VALUES(@data,@consumo)";
            return this.command.ExecuteNonQuery() == 1;
        }

        public bool AtualizarConsumoAgua(DateTime data, float Valor)
        {
            this.command.Parameters.Clear();
            this.command.CommandText = "SELECT COUNT(*) FROM ConsumoAgua WHERE Data=@data";
            this.command.Parameters.Add("@data", OleDbType.Date).Value = data.Date;
            this.command.Parameters.Add("@consumo", OleDbType.Single).Value = Valor;
            this.command.CommandText = (int)this.command.ExecuteScalar() == 1 ?
                                       "UPDATE ConsumoAgua SET Volume=Volume+@consumo WHERE Data=@data" :
                                       "INSERT INTO ConsumoAgua (Data,Volume) VALUES(@data,@consumo)";

            return this.command.ExecuteNonQuery() == 1;
        }


        public void Dispose()
        {
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }
    }
}
