using System.Collections.Concurrent;

namespace SQLPass
{
    class Cupom
    {

        public void FichaDeAvaliacao()
        {
            Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<bmp></bmp>", 0);
            Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<confgui>P</confgui>", 0);
            for (int i = 0; i < 8; i++)
            {

                Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<fe><b>Código da sessão:</b>_______________________" +
                                                                   "<sl>1</sl><b>Seu nome:</b>_______________________________" +
                                                                   "<sl>2</sl><b>Expectativa:</b>" +
                                                                   "<sl>1</sl>    Não atendeu | Atendeu | Excedeu" +
                                                                   "<sl>1</sl><b>Nota:</b>   1    2     3     4     5" +
                                                                   "<ce><sl>2</sl><b>Impresso na DR700 DARUMA" +
                                                                   "<l></l> www.desenvolvedoresdaruma.com.br</b></ce></fe>", 0);
                if (i == 7)
                {
                    Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<confgui>T</confgui>", 0);
                    Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<sl>3</sl><gui></gui>", 0);
                }
                else
                    Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<sl>3</sl><gui></gui>", 0);
            }
        }

        public void Imprimir(string nome, string email, string food)
        {
            {

                //MessageBox.Show("Impressora Ligada!");
                Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<bmp></bmp>", 0);
                Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<ce><e><b><da>" + nome + "<l></l></da></b>" +
                                                                "<fe>" + email +
                                                                "</e></fe>" +
                                                                "<sl>1</sl><l></l>Impresso na DR700 DARUMA" +
                                                                "<l></l> www.desenvolvedoresdaruma.com.br</ce>", 0);


                //PULANDO LINHA PARA LOGO
                Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<sl>1</sl>", 0);

                if (food != "" && food != "0")
                {
                    Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<confgui>P</confgui>", 0);
                    Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<gui></gui>", 0);
                    //MessageBox.Show("Impressora Ligada!");
                    //Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<bmp></bmp>", 0);
                    Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<ce><e><b><da>" + "LunchBox" + "<l></l></da></b>" +
                                                                    "<fe>" + email +
                                                                    "</e></fe>" +
                                                                    "<l></l>" + nome +
                                                                    "</ce>", 0);


                    //PULANDO LINHA PARA LOGO
                    Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<sl>1</sl>", 0);
                }
                Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<confgui>T</confgui>", 0);
                Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<gui></gui>", 0);

                ////SIOS
                //Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<gui></gui>", 0);

                //Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<ce><e><b><da> SIOS <sl>2</sl></da>" +
                //                                                nome + "<l></l></b>" +
                //                                                "<fe>" + email + "</fe></e>" +
                //                                                "<sl>1</sl> Deposite este ticket na urna SIOS!" +
                //                                                "<sl>1</sl><b>Impresso na DR700 DARUMA" +
                //                                                "<l></l> www.desenvolvedoresdaruma.com.br</ce>", 0);

                //Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<sl>3</sl><gui></gui>", 0);

                ////NGR Solutions
                //Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<ce><e><b><da> NGR Solutions </b></da>" +
                //                                                "<sl>2</sl><b>" + nome + "<l></l>" +
                //                                                "<fe>" + email + "</b></e></fe>" +
                //                                                "<sl>1</sl> Deposite este ticket na urna NGR Solutions!" +
                //                                                "<sl>1</sl><b>Impresso na DR700 DARUMA" +
                //                                                "<l></l> www.desenvolvedoresdaruma.com.br</ce>", 0);

                //Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<sl>3</sl><gui></gui>", 0);

                ////Idera
                //Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<ce><e><b><da> Idera </b></da>" +
                //                                                "<sl>2</sl><b>" + nome + "<l></l>" +
                //                                                "<fe>" + email + "</b></e></fe>" +
                //                                                "<sl>1</sl> Deposite este ticket na urna Idera!" +
                //                                                "<sl>1</sl><b>Impresso na DR700 DARUMA" +
                //                                                "<l></l> www.desenvolvedoresdaruma.com.br</ce>", 0);

                //Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<sl>3</sl><gui></gui>", 0);

                ////Mainworks Confio
                //Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<ce><e><b><da> Mainworks Confio </b></da>" +
                //                                                "<sl>2</sl><b>" + nome + "<l></l>" +
                //                                                "<fe>" + email + "</b></e></fe>" +
                //                                                "<sl>1</sl> Deposite este ticket na urna Mainworks Confio!" +
                //                                                "<sl>1</sl><b>Impresso na DR700 DARUMA" +
                //                                                "<l></l> www.desenvolvedoresdaruma.com.br</ce>", 0);

                //Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<gui></gui>", 0);
                //// Declaracoes.iImprimirTexto_DUAL_DarumaFramework("<confgui>P</confgui>", 0);
            }
        }
    }
}
