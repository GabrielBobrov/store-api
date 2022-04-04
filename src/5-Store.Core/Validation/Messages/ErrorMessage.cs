namespace Store.Core.Validations.Message
{
    public static class ErrorMessages
    {
        public const string CostumerNotFound = "Não existe nenhum cliente com o id informado.";

        public static string CostumerInvalid(string errors)
            => "Os campos informados para o cliente estão inválidos" + errors;
    }
}