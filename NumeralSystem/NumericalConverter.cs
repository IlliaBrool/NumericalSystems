using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NumericalSystems
{
    class newNumericalConverter
    {
        private string Input;
        private int Base;
        private string Decimal;
        private string Binary;
        private string Octal;
        private string Hex;
        private char separator1 = '.';
        private char separator2 = ',';
        private char OctSign = '0';
        private string HexSign = "0x";
        private char[] Hexagions;

        private string[] TK = null;

        public newNumericalConverter(string newInput)
        {
            Hexagions = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            if (newInput.Contains(separator1)) 
                newInput = newInput.Replace(separator1, separator2);
            this.Input = newInput;
            this.Base = getNewBase(this.Input);
                if (this.Base == 10) this.Decimal = this.Input;
                    else this.Decimal = NewToDecimal(this.Input, this.Base);
                if (this.Base == 16) this.Hex = this.Input;
                    else this.Hex = HexSign + NewFromDecimal(this.Decimal, 16);
                if (this.Base == 8) this.Octal = this.Input;
                    else this.Octal = OctSign + NewFromDecimal(this.Decimal, 8);
                if (this.Base == 2) this.Binary = this.Input;
                    else this.Binary = NewFromDecimal(this.Decimal, 2);
        }

        public string PrintDecimalForm { get { return $"DECIMAL: {this.Decimal}"; } }
        public string PrintBinaryForm { get { return $"BINARY: {this.Binary}"; } }
        public string PrintOctalForm { get { return $"OCTAL: {this.Octal}"; } }
        public string PrintHexForm { get { return $"HEXADECIMAL: {this.Hex}"; } }

        private string NewToDecimal(string Code, int Base)
        {
            if (Code.Contains(separator1)) 
                Code = Code.Replace(separator1, separator2);
            if (Code.Contains(separator2)) 
                TK = Code.Split(separator2);
            if (!Code.Contains(separator2)) 
                return IntPartConverterToDecimal(Code, Base);
            if (TK[0] == $"0{separator2}") 
                return separator2 + FloatPartConverterToDecimal(TK[1], Base);

            return IntPartConverterToDecimal(TK[0], Base) + FloatPartConverterToDecimal(TK[1], Base);
        }
        private string IntPartConverterToDecimal(string Input, int Base)
        {
            double Result = 0;
                if (Input.StartsWith(HexSign)) 
                    Input = Input.Replace(HexSign, "");
                if (Input.StartsWith(OctSign.ToString())) 
                    Input = Input.Replace(OctSign.ToString(), "");

            Input = ReverseString(Input); 

            for (int i = 0; i < Input.Length; i++)
            {
                int HexIndex = Array.IndexOf(Hexagions, Input[i]);

                if (!int.TryParse(Input[i].ToString(), out int t) && HexIndex != -1) 
                    Result += HexIndex * Math.Pow(Base, i);
                else if (int.TryParse(Input[i].ToString(), out int t1)) 
                    Result += t1 * Math.Pow(Base, i);
            }

            return Result.ToString();
        }
        private string FloatPartConverterToDecimal(string Input, int Base)
        {
            double Result = 0;
                for (int i = 0; i < Input.Length; i++)
                {
                    int HexIndex = Array.IndexOf(Hexagions, Input[i]);
                        if (!int.TryParse(Input[i].ToString(), out int t) && HexIndex != -1)
                            Result += HexIndex * Math.Pow(Base, -i - 1);
                        else if (int.TryParse(Input[i].ToString(), out int t1))
                            Result += t1 * Math.Pow(Base, -i - 1);
                }
            return Result.ToString();
        }

        private string NewFromDecimal(string Code, int Base)
        {
            if (Code.Contains(separator1)) 
                Code = Code.Replace(separator1, separator2);
            if (Code.Contains(separator2)) 
                TK = Code.Split(separator2);
            if (!Code.Contains(separator2))
                return IntPartConverterFromDecimal(Code, Base);
            if (Code.Contains(separator2) && IntPartConverterFromDecimal(TK[0], Base) == "")
                return "0" + separator2 + FloatPartConverterFromDecimal(TK[1], Base);
            return IntPartConverterFromDecimal(TK[0], Base) + separator2 + FloatPartConverterFromDecimal(TK[1], Base);
        }
        private string IntPartConverterFromDecimal(string Input, int Base)
        {
            int Value = int.Parse(Input);
            string Result = "";
                while (Value > 0)
                {
                    int remainder = Value % Base;
                        if (remainder > 9) 
                            Result = Result + Hexagions[remainder];
                        if (remainder <= 9) 
                            Result = Result + remainder.ToString();
                    Value = Value / Base;
                }
            return ReverseString(Result);
        }
        private string FloatPartConverterFromDecimal(string Input, int Base)
        {
            double Value = double.Parse(Input) / Math.Pow(10, Input.Length);
            string Result = "";
            int t = 0;
                while (t < 10)
                {
                    Value = Value * Base;
                    int remainder = (int)Value;
                        if (remainder > 9) 
                    Result = Result + Hexagions[remainder];
                        if (remainder <= 9) 
                    Result = Result + remainder.ToString();
                    Value = Value - (int)Value;
                    t++;
                }
            return Result;
        }

        private string ReverseString(string Word)
        {
            return string.Join("", Word.Reverse());
        }
        private int getNewBase(string Input)
        {
            int t;
            double d;
            if (Input.Contains(separator1)) 
                Input = Input.Replace(separator1, separator2);
            if ((!Input.StartsWith(OctSign.ToString()) && int.TryParse(Input, out t))
                || (double.TryParse(Input, out d) && string.Equals(d.ToString(), Input))) return 10;
            else if ((Input.StartsWith(OctSign.ToString())
                && double.TryParse(Input, out d)) || Input.StartsWith($"{OctSign}0.")) return 8;
            else if ((Input.StartsWith(HexSign) || Input.StartsWith($"{HexSign}0."))
                && Input.Split(separator2).Length - 1 < 2) return 16;
            else 
                throw new Exception("Wrong Input format");
        }
        private bool arrayTermInString(string String, char[] array)
        {
            for (int i = 0; i < array.Length; i++)
                if (String.ToLower().Contains(array[i].ToString().ToLower())) return true;
            return false;
        }

    }
}