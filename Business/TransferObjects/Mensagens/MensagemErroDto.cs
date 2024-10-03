namespace Business.TransferObjects.Mensagens
{
    public class MensagemErroDto
    {
        public MensagemErroDto() { }
        public MensagemErroDto(string mensagem)
        {
            this.msg = mensagem;
            this.status = 0;
        }

        public MensagemErroDto(string mensagem, int status, object data)
        {
            this.msg = mensagem;
            this.status = status;
            this.data = data;   
        }

        public MensagemErroDto(string mensagem, int status)
        {
            this.msg = mensagem;
            this.status = status;
            this.data = null;
        }

        public string msg { get; set; }
        public int status { get; set; }
        public object data { get; set; }
    }
}
