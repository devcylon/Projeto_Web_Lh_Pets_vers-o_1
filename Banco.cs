using System.Data.SqlClient;

namespace Projeto_Web_Lh_Pets_versão_1
{
    class Banco
    {   
	
    private List<Clientes> lista = new List<Clientes>();
    public List<Clientes> GetLista(){
        return lista;
    }
	public Banco()
	{
	 	try
                {
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(
                    "User ID=sa;Password=464972006;" +
                    "Server=localhost\\CYLON;" +
                    "Database=vendas;" +
                    "Trusted_Connection=False;"
                    );

                    using (SqlConnection conexao = new SqlConnection(builder.ConnectionString))
                    {
                        String sql = "SELECT * FROM tblclientes";
                        using (SqlCommand comando = new SqlCommand(sql, conexao ))
                        {
                            conexao.Open();
                            using (SqlDataReader tabela = comando.ExecuteReader())
                            {

                                while(tabela.Read())
                                {
                                    lista.Add(new Clientes()
                                    {
                                        cpf_cnpj = tabela["cpf_cnpj"].ToString(),
                                        nome = tabela["nome"].ToString(),
                                        endereco = tabela["endereco"].ToString(),
                                        rg_ie = tabela["rg_ie"].ToString(),
                                        tipo = tabela["tipo"].ToString(),
                                        valor = (float)Convert.ToDecimal(tabela["valor"]),
                                        valor_imposto = (float)Convert.ToDecimal(tabela["valor_imposto"]),
                                        total = (float)Convert.ToDecimal(tabela["total"])
                                    });
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
	}
	
  
 
	public String GetListaString()
	{
		string enviar= "<!DOCTYPE html><html><head><meta charset='utf-8' />"+
                      "<title>Cadastro de Clientes</title></head><body>";
        enviar = enviar + "<style>tr:nth-child(even){background-color: #D6EEEE;}tr:hover {background-color: #000000; color:#FFFFFF;}</style><H1>Lista de Clientes</h1><table style='width:100%'><tr><th>CPF/CPNJ</th><th>Nome</th><th>Endereço</th><th>RG/IE</th><th>Tipo</th><th>Valor</th><th>Valor imposto</th><th>Valor Total</th></tr>";

		foreach (Clientes cli in GetLista())
                {
                    enviar = enviar + 
                   "<tr><th>" +
                    cli.cpf_cnpj + "</th><th>" + 
                    cli.nome + "</th><th>" + cli.endereco + " </th><th>" + cli.rg_ie + " </th><th> " +
                    cli.tipo + " </th><th> " + cli.valor.ToString("C") + "</th><th>" + 
                    cli.valor_imposto.ToString("C") + " </th><th> " + cli.total.ToString("C") + "</th></tr>";
                }
                enviar = enviar + "</table>";
		return enviar;
	}

	public void imprimirListaConsole(){

                foreach (Clientes cli in GetLista())
                {
                    Console.WriteLine(cli.cpf_cnpj + " - " + 
                    cli.nome + " - " + cli.endereco + " - " + cli.rg_ie + " - " +
                    cli.tipo + " - " + cli.valor.ToString("C") + " - " + 
                    cli.valor_imposto.ToString("C") + " - " + cli.total.ToString("C"));
                }
        }

        
    }
}