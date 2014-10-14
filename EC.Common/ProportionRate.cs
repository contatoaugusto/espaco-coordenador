using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace EC.Common
{
    /// <summary>
    /// Estrutura que define vários tipos de proporções de imagens.
    /// </summary>
    public struct ProportionRates
    {
        /// <summary>
        /// Padrão de proporção de fotos para documentos.
        /// </summary>
        public static ProportionRate _3x4
        {
            get
            {
                ProportionRate pr;
                pr.Width = 3;
                pr.Height = 4;
                return pr;
            }
        }
        public static ProportionRate _9x13
        {
            get
            {
                ProportionRate pr;
                pr.Width = 9;
                pr.Height = 13;
                return pr;
            }
        }
        public static ProportionRate _10x15
        {
            get
            {
                ProportionRate pr;
                pr.Width = 10;
                pr.Height = 15;
                return pr;
            }
        }
        public static ProportionRate _13x18
        {
            get
            {
                ProportionRate pr;
                pr.Width = 13;
                pr.Height = 18;
                return pr;
            }
        }
        public static ProportionRate _20x25
        {
            get
            {
                ProportionRate pr;
                pr.Width = 20;
                pr.Height = 25;
                return pr;
            }
        }
    }

    /// <summary>
    /// Estrutura usada para definir uma taxa de proporção usada para imagens.
    /// </summary>
    public struct ProportionRate
    {
        /// <summary>
        /// Largura.
        /// </summary>
        public int Width;

        /// <summary>
        /// Altura.
        /// </summary>
        public int Height;

        /// <summary>
        /// Retorna a taxa de proporção do tamanho (cálculo: Largura/Altura) . Pode ser usado para tratamento de imagens.
        /// </summary>
        public float PropRate
        {
            get { return (float)Width / (float)Height; }
        }

        public ProportionRate(int width, int height)
        { 
            Width = width;
            Height = height;
        }
    }
}
