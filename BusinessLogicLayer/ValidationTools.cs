using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StudentsManager.BusinessLogicLayer
{
    // если возвращается Null, то все хорошо, иначе будет строка с причиной
    public static class ValidationTools
    {
        public static String CheckLogin(String inputStr)
        {
            String result = null;

            if( String.IsNullOrWhiteSpace(inputStr)==true )
            {
                result = @"Логин пустой или отсутствует.";
            }
            else
            {
                Match match = Regex.Match(inputStr, @"^[a-z0-9_-]{6,32}$", RegexOptions.IgnoreCase);
                if (match.Success==false)
                {
                    result = @"Введенный логин некорректен. Логин должен состоять из латинских букв, цифр, дефисов и подчёркиваний, от 6 до 32 символов.";
                }
            }

            return result;
        }
        public static String CheckPassword(String passwordStr)
        {
            String result = null;

            if (String.IsNullOrWhiteSpace(passwordStr) == true)
            {
                result = @"Пароль пустой или отсутствует.";
            }
            else
            {
                Match match = Regex.Match(passwordStr, @"^[a-z0-9_-]{6,32}$", RegexOptions.IgnoreCase);
                if (match.Success == false)
                {
                    result = @"Введенный пароль некорректен. Пароль должен состоять из латинских букв, цифр, дефисов и подчёркиваний, от 6 до 32 символов.";
                }
            }

            return result;
        }

        public static String CheckSurnameAndInitials(String sourceStr)
        {
            String result = null;

            if (String.IsNullOrWhiteSpace(sourceStr) == true)
            {
                return null;
            }

            Match match = Regex.Match(sourceStr, @"^[А-ЯA-Z][а-яА-Яa-zA-Z]{0,24}[\s][А-ЯA-Z][.][А-ЯA-Z][.]$", RegexOptions.IgnoreCase);
            if (match.Success == false)
            {
                result = "Фамилия и инициалы некорректны. Формат такой - \"Фамилия И.О.\" Буквы могут быть латинскими. Регистр соблюдать как в примере.";
            }

            return result;
        }

        public static String CheckEMail(String sourceStr)
        {
            String result = null;

            if (String.IsNullOrWhiteSpace(sourceStr) == true)
            {
                return null;
            }

            Match match = Regex.Match(sourceStr, @"^([a-z0-9_\.-]+)@([a-z0-9_\.-]+)\.([a-z\.]{2,6})$", RegexOptions.IgnoreCase);
            if (match.Success == false)
            {
                result = @"Введенная эл. почта некорректна. Общий вид — логин@поддомен.домен. Допустимы буквы, цифры, подчёркивания, дефисы и точки.";
            }

            return result;
        }

        public static String CheckPhone(String sourceStr)
        {
            String result = null;

            if (String.IsNullOrWhiteSpace(sourceStr) == true)
            {
                result = @"Номер телефона пустой или отсутствует.";
            }
            else
            {
                Match match = Regex.Match(sourceStr, @"((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{5,10}", RegexOptions.IgnoreCase);
                if (match.Success == false)
                {
                    result = "Формат номера некорректен. Нужно ввести номер российского стационарного или мобильного телефона длинной от 5 до 10 цифр (допустимы символы +, -, круглые скобки).";
                }
            }

            return result;
        }
        
        public static String CheckName(String sourceStr)
        {
            String result = null;

            if (String.IsNullOrWhiteSpace(sourceStr) == true)
            {
                result = "Поле \"Имя\" пустое или отсутствует.";
            }
            else
            {
                Match match = Regex.Match(sourceStr, @"^[А-ЯA-Z][а-яА-Яa-zA-Z]{1,49}$", RegexOptions.IgnoreCase);
                if (match.Success == false)
                {
                    result = "Имя некорректно. Формат такой - \"Имя\". Буквы могут быть латинскими. Регистр соблюдать как в примере. Длинна от 1-го до 50-ти символов.";
                }
            }

            return result;
        }

        public static String CheckSurname(String sourceStr)
        {
            String result = null;

            if (String.IsNullOrWhiteSpace(sourceStr) == true)
            {
                result = "Поле \"Фамилия\" пустое или отсутствует.";
            }
            else
            {
                Match match = Regex.Match(sourceStr, @"^[А-ЯA-Z][а-яА-Яa-zA-Z]{1,49}$", RegexOptions.IgnoreCase);
                if (match.Success == false)
                {
                    result = "Фамилия некорректна. Формат такой - \"Фамилия\". Буквы могут быть латинскими. Регистр соблюдать как в примере. Длинна от 1-го до 50-ти символов.";
                }
            }

            return result;
        }

        public static String CheckPatronomic(String sourceStr)
        {
            String result = null;

            if (String.IsNullOrWhiteSpace(sourceStr) == true)
            {
                return null;
            }


            Match match = Regex.Match(sourceStr, @"^[А-ЯA-Z][а-яА-Яa-zA-Z]{1,49}$", RegexOptions.IgnoreCase);
            if (match.Success == false)
            {
                result = "Отчество некорректно. Формат такой - \"Отчество\". Буквы могут быть латинскими. Регистр соблюдать как в примере. Длинна от 1-го до 50-ти символов.";
            }

            return result;
        }

    }
}
