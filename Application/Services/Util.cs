using System.Security.Cryptography;
using System.Text;
using Application.IServices;


namespace Application.Services
{
    public class Util
    {
        public static T FillEntity<T>(T entity) where T : class
        {
            try
            {
                var idProperty = entity.GetType().GetProperty("Id");
                if (idProperty != null && idProperty.PropertyType == typeof(string))
                {
                    idProperty.SetValue(entity, Guid.NewGuid().ToString());
                }

                var createdProperty = entity.GetType().GetProperty("Created");
                if (createdProperty != null && createdProperty.PropertyType == typeof(long?))
                {
                    var now = DateTimeOffset.Now.ToUnixTimeSeconds();
                    createdProperty.SetValue(entity, now);
                }

                var updatedProperty = entity.GetType().GetProperty("Updated");
                if (updatedProperty != null && updatedProperty.PropertyType == typeof(long?))
                {
                    updatedProperty.SetValue(entity, null);
                }

                var dateBlockedProperty = entity.GetType().GetProperty("DateBlocked");
                if (dateBlockedProperty != null && dateBlockedProperty.PropertyType == typeof(long?))
                {
                    dateBlockedProperty.SetValue(entity, null);
                }
                

                return entity;
            }
            catch (Exception)
            {
                // Tratar exceções conforme necessário
                throw;
            }
        }
        public static string GerarHashMd5(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                // Convertendo a senha para bytes
                byte[] inputBytes = Encoding.ASCII.GetBytes(password);

                // Calculando o hash MD5
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convertendo o hash de bytes para uma string hexadecimal
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}