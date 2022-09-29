/*Colegio Técnico Antônio Teixeira Fernandes (Univap)
 *Curso Técnico em Informática - Data de Entrega: 06 / 09 / 2022
* Autores do Projeto: Henrique Curtis Correa Marques
*                     Arthur Kinderman Peres de Oliveira
*
* Turma: 2F
* PROJETO 3° BIMESTRE
 * Observação: 
 * label1: axuliar para o usuario digitar o valor
 * textbox1: caixa de texto que recebe o valor
 * label2: auxiliar para o usuario digitar a parcela
 * textbox2: caixa de texto que recebe as parcelas
 * label3: auxiliar para o usario inserir a data
 * datetimePicker1: caixa de datar da compra
 * button1: botao que calcula as parcelas 
 * label4: auxiliar para a listbox1
 * listbox1: caixa de lista que mostra as parcelas, o valor e a data
 * label5: auxiliar para a data do pagamento
 * datetimePicker2: caixa de data para pagar as parcelas
 * label6: auxiliar para mostrar o quanto falta a pagar
 * label7: label que mostra o valor que falta a pagar
 * button2: botao para efetuar o pagamento da parcela
 * label8: auxuliar para finalizar o programa
 * button3: finaliza o programa
 * 
 * 
 * ******************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        // CRIAÇÃO DAS VARIAVEIS PARCELAS, VALOR E NOVO VALOR GLOBALMENTE
        // CRIAÇÃO DO ARRAY QUE RECEBERA AS DATAS
        int parcelas = 0;
        double valor = 0;
        double novo_valor = 0;
        DateTime[] array = new DateTime[0];
        public Form1()
        {
            
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // TESTE PARA NÃO DEIXAR O CAMPO VAZIO E NÃO DEIXAR ENTRAR UM VALOR DIFERENTE DE NUMERICO
            if(double.TryParse(textBox1.Text, out valor) == false && int.TryParse(textBox2.Text, out parcelas) == false)
            {
                MessageBox.Show("Digite um valor numerico!!");
            }
            if (double.TryParse(textBox1.Text, out valor) == true && int.TryParse(textBox2.Text, out parcelas) == true)
            {
                if (Math.Abs(double.Parse(textBox1.Text)) == double.Parse(textBox1.Text) && Math.Abs(int.Parse(textBox2.Text)) == int.Parse(textBox2.Text))
                {
                    // DATETIME DATA: CAIXA DE DATA, VALOR: NUMERO INSERIDO NA TEXTBOX1, PARCELAS: VALOR INSERIDO NA TEXTBOX2,
                    //ARRAY.RESIZE: AJUSTA O TAMANHO DO ARRAY CONFORME O VALOR INSERIDO NA TEXTBOX2
                    DateTime data = dateTimePicker1.Value.Date;
                    valor = double.Parse(textBox1.Text);
                    parcelas = int.Parse(textBox2.Text);
                    Array.Resize(ref array, array.Length + parcelas);
                    // LAÇO PARA ADICIONAR UM MES ATE O NUMERO ESPECIFICADO DE PARCELAS 
                    for (int i = 1; i <= parcelas; i++)
                    {
                        data = data.AddMonths(1);
                        // CALCULO DAS PARCELAS
                        novo_valor = valor / parcelas;
                        // CRIAÇÃO DA VARIAVEL "aux" QUE RECEBE O DIA DA SEMANA POR EXTENSO E FAZ A VERIFICAÇÃO SE E DIA UTIL OU NAO SE FOR ELE ADICIONA 1 OU 2 DIAS 
                        string aux = data.ToString("dddd");
                        if (aux == "domingo")
                        {
                            data = data.AddDays(1);
                        }
                        if (aux == "sábado")
                        {
                            data = data.AddDays(2);
                        }
                        array[i - 1] = data;
                        // COLOCANDO AS PARCELAS NA LISTBOX1
                        listBox1.Items.Add(i + "° parcela " + novo_valor.ToString("C") + " " + data.ToString("dd/MM/yyyy"));


                    }
                    // COMANDOS PARA EVITAR QUE O USUARIO APERTE BOTOES QUE NÃO DEVE APERTAR
                    label6.Visible = true;
                    label7.Visible = true;
                    button2.Enabled = true;
                    button2.Visible = true;
                    dateTimePicker2.Visible = true;
                    label5.Visible = true;
                    label4.Visible = true;
                    listBox1.Visible = true;

                }
                else
                {
                    MessageBox.Show("Digite valores validos!!");
                }


            }
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // DATA2: CAIXA DE DATA, JUROS: VARIAVEL PARA CALCULAR O JUROS, INDICE: PEGAR O INDICE DA LISTBOX
            DateTime data2 = dateTimePicker2.Value.Date;
            double juros = 0;
            int indice = listBox1.SelectedIndex;
            
            // VERIFICA SE O INDICE 0 DA LISTBOX1 ESTA SELECIONADO
            if (listBox1.GetSelected(0))
            {
                // COMPARA AS DATAS DA PARCELA COM A DATA QUE O USUARIO PAGOU PARA VER SE ESTA ATRASADA OU NÃO
                if (DateTime.Compare(array[indice], data2) >= 0 && indice != -1)
                {
                    listBox1.Items.RemoveAt(0);
                    valor = valor - novo_valor;
                    label7.Text = valor.ToString("C");
                    label7.ForeColor = Color.Green;
                    label8.Text = "Pacela valida!";
                    label8.ForeColor = Color.Green;
                    // ORDENANDO O ARRAY COM AS DATAS
                    for (int i = 0; i < array.Length - 1; i++)
                    {
                        array[i] = array[i + 1];
                    }
                    Array.Resize(ref array, array.Length - 1);
                    // CONDIÇÃO PARA QUANDO FINALIZAR A COMPRA DESABILITAR O BOTAO DE PAGAR 
                    if (listBox1.Items.Count == 0)
                    {
                        button2.Enabled = false;
                        dateTimePicker2.Enabled = false;
                    }
                    
                }
                // COMPARANDO SE AS DATAS NÃO SAO IGUAIS E CALCULANDO O JUROS
                else
                {
                    listBox1.Items.RemoveAt(0);
                    valor = valor - novo_valor;
                    juros = novo_valor * 0.03;
                    label7.Text = valor.ToString("C");
                    label7.ForeColor = Color.Green;
                    double res = novo_valor + juros;
                    label8.Text = "Pacela atrasada! Novo valor reajustado: " + (res.ToString("C"));
                    label8.ForeColor = Color.Red;
                    //ORDENANDO O ARRAY QUE CONTEM AS DATAS
                    for (int i = 0; i < array.Length - 1; i++)
                    {
                        array[i] = array[i + 1];
                    }
                    Array.Resize(ref array, array.Length - 1);
                    if (listBox1.Items.Count == 0)
                    {
                        button2.Enabled = false;
                        dateTimePicker2.Enabled = false;
                    }
                }

               
            }
            // TESTE PARA NÃO DEIXAR ELE PAGAR UMA PARCELA SEM PAGAR A ANTERIOR
            else if (listBox1.GetSelected(1) == true && listBox1.Items[0] == null)
            {
                // EXCLUSÃO DA PARCELA PAGA
                listBox1.Items.RemoveAt(1);

            }
            else
            {
                MessageBox.Show("Pague a parcela anterior antes de continuar");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // MÉTODO PARA FECHAR O WINDOWS FORMS
            Close();
        }
    }
}
