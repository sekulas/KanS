namespace KanS.Exceptions;
[Serializable]
internal class BadRequestException : Exception {
    public BadRequestException(string message) : base(message) {
    }
}