using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        // Product Messages
        public static string ProductAdded = "Ürün başarıyla eklendi.";
        public static string ProductUpdated = "Ürün başarıyla güncellendi.";
        public static string ProductDeleted = "Ürün başarıyla silindi.";
        public static string ProductNameAlreadyExists = "Ürün ismi zaten mevcut.";

        // Category Messages
        public static string CategoryAdded = "Kategori başarıyla eklendi.";
        public static string CategoryUpdated = "Kategori başarıyla güncellendi.";
        public static string CategoryDeleted = "Kategori başarıyla silindi.";
        public static string CategoryNotEnabled = "Kategoride yeterli sayıda ürün yok.";

        // User Messages
        public static string UserAdded = "Kullanıcı başarıyla eklendi.";
        public static string UserNotFound = "Kullanıcı bulunamadı.";
        public static string PasswordError = "Şifre hatalı.";
        public static string SuccessfulLogin = "Sisteme giriş başarılı.";
        public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut.";
        public static string UserRegistered = "Kullanıcı başarıyla kayıt edildi.";
        public static string AccessTokenCreated = "Token başarıyla oluşturuldu.";

        public static string AuthorizationDenied = "Yetkiniz yok.";
    }
}
