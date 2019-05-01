using Examen.Models;
using System.Web.Http;

namespace Examen.Controllers
{
    public class PalabrasController : ApiController
    {
        public IHttpActionResult GetPalabra(string tPalabra)
        {
            Palabra pal = new Palabra();
            BuscadorPalabra componente = new BuscadorPalabra();
            pal.coordenadas = componente.getPosiciones(tPalabra.ToUpper());
            return Ok(pal);
        }
    }

    public class BuscadorPalabra
    {
        private readonly string[] secuencias = { "AGVNFT", "XJILSB", "CHAOHD", "ERCVTQ", "ASOYAO", "ERMYUA", "TELEFE" };
        private readonly int[] dirx = { -1, -1, -1, 0, 0, 1, 1, 1 };
        private readonly int[] diry = { -1, 0, 1, -1, 1, -1, 0, 1 };

        private char[,] grilla = null;
        private int maxFil;
        private int maxCol;
        

        public BuscadorPalabra()
        {
            maxFil = secuencias.Length;
            maxCol = secuencias[0].Length;
            grilla = new char[maxFil, maxCol];
            for (int i = 0; i < maxFil; i++)
            {
                char[] letrasPal = secuencias[i].ToCharArray();
                for (int j = 0; j < letrasPal.Length; j++)
                {
                    grilla[i, j] = letrasPal[j];
                }
            }
        }


        private int[] BuscarLetraInicial(char letra, int aparicion)
        {
            bool encontro = false;
            int ocurrencia = 0;
            int i = 0;

            int[] retorno = new int[2];

            do
            {
                int j = 0;
                do
                {
                    if (grilla[i, j] == letra)
                    {
                        ocurrencia++;
                        if (ocurrencia == aparicion)
                        {
                            encontro = true;
                            retorno[0] = i;
                            retorno[1] = j;
                        }
                    };
                    j++;
                } while (!encontro && i < maxFil && j < maxCol);

                i++;
            } while (!encontro && i < maxFil);

            if (!encontro)
            {
                retorno[0] = -1;
                retorno[1] = -1;
            }

            return retorno;
        }
        
        private int[] BuscarSiguienteLetra(char letra, int fil, int col, int direc)
        {
            int[] retorno = new int[2] { -1, -1 };

            int auxFil = fil + dirx[direc - 1];
            int auxCol = col + diry[direc - 1];

            if (EsLaLetra(letra, auxFil, auxCol))
            {
                retorno[0] = auxFil;
                retorno[1] = auxCol;
            }
            return retorno;
        }

        private bool EsLaLetra(char letra, int fil, int col)
        {
            return (fil != -1 && fil < maxFil && col != -1 && col < maxCol && grilla[fil, col] == letra);
        }


        private void ReiniciarMatriz(int[,] matriz)
        {
            for (int i = 0; i < matriz.Length / 2; i++)
            {
                matriz[i, 0] = -1;
                matriz[i, 1] = -1;
            }
        }


        private void AumentarMatriz(int[,] matriz)
        {
            for (int i = 0; i < matriz.Length / 2; i++)
            {
                matriz[i, 0]++;
                matriz[i, 1]++;
            }
        }

        public int[,] getPosiciones(string tPalabra)
        {
            int longitud = tPalabra.Length;
            bool finProceso = false;

            int[,] retorno = new int[longitud, 2];
            ReiniciarMatriz(retorno);

            int aparPrimerLetra = 0;
            do
            {
                aparPrimerLetra++;
                var coord = BuscarLetraInicial(tPalabra[0], aparPrimerLetra);

                if (coord[0] != -1 && coord[1] != -1)
                {
                    retorno[0, 0] = coord[0];
                    retorno[0, 1] = coord[1];

                    if (longitud>1)
                    {
                        int auxFil = coord[0];
                        int auxCol = coord[1];
                        int nLetra = 0;

                        int direccion = 0;
                        bool encontroLetra = false;

                        do
                        {
                            encontroLetra = false;
                            direccion++;

                            do
                            {
                                auxFil = coord[0];
                                auxCol = coord[1];
                                nLetra = 0;

                                do
                                {
                                    encontroLetra = false;
                                    nLetra++;
                                    int[] coordLetra = BuscarSiguienteLetra(tPalabra[nLetra], auxFil, auxCol, direccion);

                                    if (coordLetra[0] > -1 && coordLetra[1] > -1)
                                    {
                                        encontroLetra = true;
                                        retorno[nLetra, 0] = coordLetra[0];
                                        retorno[nLetra, 1] = coordLetra[1];

                                        auxFil = retorno[nLetra, 0];
                                        auxCol = retorno[nLetra, 1];

                                    };

                                } while (encontroLetra && nLetra < longitud - 1);

                                if (!encontroLetra)
                                {
                                    direccion++;
                                }

                            } while (!encontroLetra && direccion < 9);


                        } while (nLetra < longitud - 1 && encontroLetra);
                    }

                    if (retorno[longitud - 1, 0] != -1)
                    {
                        finProceso = true;
                    }
                }
                else
                {
                    finProceso = true;
                    ReiniciarMatriz(retorno);
                }

            } while (!finProceso);

            AumentarMatriz(retorno);
            return retorno;
        }

    }
}
