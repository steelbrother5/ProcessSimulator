using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessSimulator.Module.Resources
{
    public static class RegularExpressions
    {

        public const string OnlyLetter = @"^[a-zA-Z]+$";

        public const string OnlyNumbers = @"^\d+$";

        public const string LettersNumbers = @"^[a-zA-Z0-9]+$";

        public const string WithOutSpecialCharacters = @"^[a-zA-Z0-9\sÑñáéíóúÁÉÍÓÚ]+$";

        public const string AlphanumericWithSpacesAndAccents = @"^[a-zA-Z0-9\sñÑáéíóúÁÉÍÓÚ\.]+$";

        public const string Email = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        public const string LongitudMinMax = @"^.{5,10}$";

        public const string LettersNumbersHyphenApostrophe = @"^[a-zA-Z0-9\s'-ñÑáéíóúÁÉÍÓÚ]+$";

        public const string OnlyPlusAndNumbers = @"^\+?[0-9]+$";

        public const string LettersNumbersHyphenBackslash = @"^[a-zA-Z0-9\s\-/]+$";

        public const string LettersNumbersAmpersanHyphenDotComma = @"^[a-zA-Z0-9\s&\-\.,]+$";

        public const string LettersNumbersHyphenDotSlashComma = @"^[a-zA-Z0-9\s\-/,#\.]+$";

        public const string LettersNumbersHyphenDotSlashCommaWithAccents = @"^[a-zA-Z0-9\s\&\-\./,ñÑáéíóúÁÉÍÓÚ]+$";

        public const string NumbersHyphenMostUnderline = @"^[0-9\s\-_+]+$";

        public const string NumbersHyphenMostUnderlineDotAt = @"^[a-zA-Z0-9@.\-_+]+$";

        public const string LettersNumbersHyphen = @"^[a-zA-Z0-9\s\-]+$";

        public const string NumbersHypenUnderline = @"^[0-9\-_]+$";

        public const string NumbersHypenMost = @"^[0-9\-_+]+$";

        public const string NumbersPlusAndMinus = @"^[0-9\+\-]+$";

        public const string NumbersPlusAndMinusLetters = @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ0-9\+\-\s]+$";

        public const string LettersNumbersWithAccents = @"^^[A-Za-z0-9 \-\'ÑñáéíóúÁÉÍÓÚ]+$";

        public const string LettersNumbersWithAccentsAndPoint = @"^^[A-Za-z0-9 \-\&.'ÑñáéíóúÁÉÍÓÚ]+$";

        // Francisco Mayorga : 03/03/2025 - Maintenance/Feature #32586
        public const string WithAllowedSpecialCharacters = @"^[a-zA-Z0-9\s\-_\.ñÑáéíóúÁÉÍÓÚ]+$";

    }
}
