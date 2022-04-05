namespace Store.Core.Validations.Message
{
    public static class ErrorMessages
    {
        public const string CostumerNotFound = "Não existe nenhum cliente com o id informado.";
        public const string CostumerAlreadyExists = "Cliente já cadastrado.";

        public static string CostumerInvalid(string errors)
            => "Os campos informados para o cliente estão inválidos" + errors;

        public static string OrderInvalid(string errors)
           => "Os campos informados para a ordem estão inválidos" + errors;
    }
}