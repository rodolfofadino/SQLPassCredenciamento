using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SQLPass
{
    public partial class Form1 : Form
    {
        private ParticipanteRepository _participantesRepository;
        private ParticipanteRepository _unipRepository;
        private ParticipanteRepository _esperaRepository;
        private LogRepository _logRepository;

        public Form1()
        {
            InitializeComponent();
            lblFood.Text = "";

            int retorno = Declaracoes.eBuscarPortaVelocidade_DUAL_DarumaFramework();
            if (retorno != 1)
            {
                MessageBox.Show("Impressora Desligada!");

            }

            _participantesRepository = new ParticipanteRepository("PARTICIPANTE");
            _unipRepository = new ParticipanteRepository("UNIP");
            _esperaRepository = new ParticipanteRepository("ESPERA");
            _logRepository = new LogRepository();
            AtualizarContadores();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtEmail.Text.Contains("@"))
            {
                var participante = _participantesRepository.Buscar(txtEmail.Text);
                if (participante != null)
                {
                    txtNome.Text = participante.Nome;
                    lblFood.Text = participante.Comida;

                    if (participante.Impresso)
                    {
                        MessageBox.Show("Credenciamento já realizado");
                    }
                    else
                    {
                        btnImprimir.Enabled = true;
                        btnImprimir.Focus();
                    }
                }
                else
                {
                    txtNome.Text = string.Empty;
                    lblFood.Text = string.Empty;
                    btnImprimir.Enabled = false;

                    var participanteUnip = _unipRepository.Buscar(txtEmail.Text);

                    if (participanteUnip != null)
                    {
                        if (participanteUnip.Impresso)
                        {
                            MessageBox.Show("Credenciamento já realizado");
                        }
                        else
                        {
                            txtNome.Text = participanteUnip.Nome;
                            lblFood.Text = participanteUnip.Comida;
                            btnImprimir.Enabled = true;
                            btnImprimir.Focus();
                        }
                    }
                    else
                    {
                        txtNome.Text = string.Empty;
                        btnImprimir.Enabled = false;

                        var participanteEspera = _esperaRepository.Buscar(txtEmail.Text);

                        if (participanteEspera != null)
                        {
                            txtNome.Text = participanteEspera.Nome;
                            lblFood.Text = participanteEspera.Comida;

                            if (IsHorario())
                            {
                                if (participanteEspera.Impresso)
                                {
                                    MessageBox.Show("Credenciamento já realizado");
                                }
                                else
                                {
                                    btnImprimir.Enabled = true;
                                    btnImprimir.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Lista de espera, aguarde até as 9h10.");
                            }
                        }
                        else
                        {
                            txtNome.Text = string.Empty;
                            lblFood.Text = string.Empty;
                            btnImprimir.Enabled = false;
                        }
                    }
                }
            }
        }

        private bool IsHorario()
        {
            if (DateTime.Now.Hour >= 9 && DateTime.Now.Minute >= 10)
                return true;
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnImprimir.Enabled = false;

            var cupom = new Cupom();
            cupom.Imprimir(txtNome.Text, txtEmail.Text, lblFood.Text);

            _logRepository.MarcarRegistro(txtEmail.Text);

            _participantesRepository.MarcarImpressao(txtEmail.Text);
            _unipRepository.MarcarImpressao(txtEmail.Text);
            _esperaRepository.MarcarImpressao(txtEmail.Text);


            txtEmail.Text = string.Empty;
            txtNome.Text = string.Empty;
            lblFood.Text = string.Empty;
            this.AtualizarContadores();
        }

        private void AtualizarContadores()
        {
            int total = 0;
            int totalInscritos = 0;
            if (_participantesRepository.Participantes != null)
            {
                total = _participantesRepository.Participantes.Count(a => a.Impresso);
                txtInscritos.Text = _participantesRepository.Participantes.Count(a => a.Impresso).ToString();
            }

            if (_unipRepository.Participantes != null)
            {
                total = total + _unipRepository.Participantes.Count(a => a.Impresso);
                txtAlunos.Text = _unipRepository.Participantes.Count(a => a.Impresso).ToString();
            }

            if (_esperaRepository.Participantes != null)
            {
                total = total + _esperaRepository.Participantes.Count(a => a.Impresso);
                txtEspera.Text = _esperaRepository.Participantes.Count(a => a.Impresso).ToString();
            }

            txtTotal.Text = total.ToString();
        }

    }

    internal class LogRepository
    {
        public void MarcarRegistro(string text)
        {
            using (var file = new System.IO.StreamWriter("c:\\Temp\\LogPessoas.csv", true))
            {
                file.WriteLine(text);
                file.Close();
            }
        }
    }

    internal class ParticipanteRepository
    {
        private string _arquivo;
        public List<Participante> Participantes;

        public ParticipanteRepository(string arquivo)
        {
            this._arquivo = arquivo;
            var file = "C://temp/Registration.csv";
            if (arquivo == "UNIP")
                file = "C://temp/UNIP.csv";
            else if (arquivo == "ESPERA")
                file = "C://temp/ESPERA.csv";

            Participantes = LoadFile(file);
            AtualizaRegistro();
        }



        private List<Participante> LoadFile(string file)
        {
            var retorno = new List<Participante>();

            using (var streamReader = new StreamReader(file))
            {
                var readLine = streamReader.ReadLine();
                int sobrenomeIndex = 0;
                int emailIndex = 0;
                int nomeIndex = 0;
                int comidaIndex = 0;
                if (readLine != null)
                {
                    var header = readLine.Split(';').ToArray();
                    sobrenomeIndex = Array.IndexOf(header, "Last Name");
                    nomeIndex= Array.IndexOf(header, "First Name");
                    emailIndex=Array.IndexOf(header, "Email");
                    comidaIndex = Array.IndexOf(header, "Payment Amount Received");

                }
                while (streamReader.Peek() >= 0)
                {
                    var line = streamReader.ReadLine();
                    if (line != null)
                    {
                        var itens = line.Split(';');
                        var sobrenome = itens[sobrenomeIndex];
                        var nome = itens[nomeIndex];
                        var email = itens[emailIndex];
                        var comida = "";
                        if (comidaIndex>=0)
                        {
                            if (itens[comidaIndex] != "0")
                            {
                                comida = "LunchBox";
                            }
                            else
                            {
                                comida = "";
                            }
                        }

                        retorno.Add(new Participante() { Email = email, Nome = UppercaseWords(nome + " " + sobrenome), Comida = comida });
                    }
                }
            }
            return retorno;
        }

        public Participante Buscar(string email)
        {
            return this.Participantes.FirstOrDefault(a => a.Email.ToLower() == email.ToLower());
        }

        public string UppercaseWords(string value)
        {
            value = value.ToLower();
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }

        public void MarcarImpressao(string email)
        {
            var participante = this.Participantes.FirstOrDefault(a => a.Email.ToLower() == email.ToLower());
            if (participante != null)
            {
                using (var file = new System.IO.StreamWriter(string.Format("c:\\Temp\\Log_{0}.csv", this._arquivo), true))
                {
                    file.WriteLine(email);
                    file.Close();
                }
                participante.Impresso = true;
            }
        }
        private void AtualizaRegistro()
        {
            var retorno = new List<string>();
            var arquivo = string.Format("c:\\Temp\\Log_{0}.csv", this._arquivo);
            var info = new FileInfo(arquivo);
            if (info.Exists)
            {
                using (var streamReader = new StreamReader(arquivo))
                {
                    while (streamReader.Peek() >= 0)
                    {
                        var line = streamReader.ReadLine();
                        if (line != null)
                        {
                            var itens = line.Split(';');
                            var email = itens[0];
                            var participante = Participantes.FirstOrDefault(a => a.Email.ToLower() == email.ToLower());
                            if (participante != null)
                            {
                                participante.Impresso = true;
                            }
                        }
                    }
                }
            }
        }
    }


    public class Participante
    {
        public string Email { get; set; }
        public string Nome { get; set; }
        public bool Impresso { get; set; }
        public string Comida { get; set; }
    }
}
