using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

using StudentsManager.DataAccessLayer;
using DTO = StudentsManager.BusinessLogicLayer.DataTransferObjects;
using StudentsManager.BusinessLogicLayer;

namespace StudentsManager.PresentationLayer.Tools
{
    /// <summary>
    /// Движок логина/логофа текущего пользователя. Используется на всех старницах веб сайта.
    /// Внутри использует сессии.
    /// </summary>
    public static class AccountEngine
    {
        private const String _userLoginStorageName = "CredentialsEngine_CurrentUserName";
        private const String _userTypeStorageName = "CredentialsEngine_CurrentUserType";
        private static String _currentUserLogin
        {
            set
            {
                StorageEngine.Session[_userLoginStorageName] = value;
            }
            get
            {               
                String currentValue = (String)StorageEngine.Session[_userLoginStorageName];
                return currentValue;
            }
        }

        private static DTO.DictionaryItem _currentUserType
        {
            set
            {
                StorageEngine.Session[_userTypeStorageName] = value;
            }
            get
            {
                DTO.DictionaryItem currentValue = (DTO.DictionaryItem)StorageEngine.Session[_userTypeStorageName];
                return currentValue;
            }
        }

        public static String GetUserTypeName()
        {
            if( IsCurrentUserLoggedIn==false )
            {
                throw new Exception("Невозможно получить тип пользователя который незалогинен.");
            }
            String userTypeName = _currentUserType.Name;
            return userTypeName;
        }

        public static Boolean IsCurrentUserLoggedIn
        {
            get
            {
                String currentValue = (String)StorageEngine.Session[_userLoginStorageName];
                if (currentValue == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public static Boolean LogOn(String login, String password)
        {
            if (IsCurrentUserLoggedIn == false)
            {
                DTO.User currUser = UserTools.Read(login, password);
                if( currUser==null )
                {
                    return false;
                }
                else // такой пользователь существует
                {
                    _currentUserLogin = currUser.Login;

                    // загрузим данные по типу пользователя                    
                    _currentUserType = DictionaryTools.Read(currUser.UserType);
                    if( _currentUserType==null )
                    {
                        throw new Exception("Ошибка: При загрузке данных пользователя не удалось загрузить его тип.");
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else
            {
                throw new Exception("Ошибка: Нельзя войти как новый текущий пользователь, если предыдущий не вышел.");
            }
        }

        public static void LogOff()
        {
            if (IsCurrentUserLoggedIn == true)
            {
                _currentUserLogin = null;
                _currentUserType = null;
            }
            else
            {
                throw new Exception("Ошибка: Нельзя выйти текущим пользователем, который не входил.");
            }
        }

        public static String UserLogin
        {
            get
            {
                if (IsCurrentUserLoggedIn == true)
                {
                    return _currentUserLogin;
                }
                else
                {
                    throw new Exception("Ошибка: Невозможно вернуть имя пользователя, который не залогинен.");
                }
            }
        }
    }
}