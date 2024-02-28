using System.ComponentModel;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Enum
{
    public enum TypeProfile
    {
        [EnumMember(Value = "Master")]
        [Description("Master")]
        Master = 0,
        [EnumMember(Value = "Administrador")]
        [Description("Administrador")]
        Admin = 1,
        [EnumMember(Value = "Usuario")]
        [Description("Usuario")]
        Profile = 2,
        [EnumMember(Value = "Professor")]
        [Description("Professor")]
        Teacher = 3,
        [EnumMember(Value = "Estudante")]
        [Description("Estudante")]
        Student = 4,
        [EnumMember(Value = "Vendedor")]
        [Description("Vendedor")]
        Seller = 4,
        [EnumMember(Value = "Suporte")]
        [Description("Suporte")]
        Suport = 4,
    }
    public enum TypeChat{
        [EnumMember(Value = "Chamado")]
        [Description("Chamado")]
        Called = 4,
        [EnumMember(Value = "Live")]
        [Description("Live")]
        Live = 4,
        [EnumMember(Value = "Chat da turma")]
        [Description("Chat da turma")]
        ChatClassrom = 4,
    }
    public enum MenuItems
    {
        [EnumMember(Value = "Administratores")]
        [Description("Administratores")]
        Admin = 0,
        [EnumMember(Value = "Usuarios")]
        [Description("Usuarios")]
        Users = 1,
        [EnumMember(Value = "Chamados")]
        [Description("Chamados")]
        Called = 2,
        [EnumMember(Value = "Professores")]
        [Description("Professores")]
        Teachers = 3,
        [EnumMember(Value = "Chats das turmas")]
        [Description("Chats das turmas")]
        ClassHassle = 4,
        [EnumMember(Value = "Aula ao vivo")]
        [Description("Aula ao vivo")]
        Live = 5,
        [EnumMember(Value = "Turmas")]
        [Description("Turmas")]
        Classes = 6,
        [EnumMember(Value = "Estudantes")]
        [Description("Estudantes")]
        Students = 7,
        [EnumMember(Value = "Listas de exercicios")]
        [Description("Listas de exercicios")]
        ExerciseLists = 8,
        [EnumMember(Value = "Produtos")]
        [Description("Produtos")]
        Products = 9,
        [EnumMember(Value = "Chat com o vendedor")]
        [Description("Chat com o vendedor")]
        ChatWithSeller = 10,
        [EnumMember(Value = "Relatorio Financeiro")]
        [Description("Relatorio Financeiro")]
        FinancialReport = 11,


    }
}
