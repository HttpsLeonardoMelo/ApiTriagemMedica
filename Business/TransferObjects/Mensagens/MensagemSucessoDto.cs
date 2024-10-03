namespace Business.TransferObjects.Mensagens
{
    public class MensagemSucessoDto
    {
        public MensagemSucessoDto() { }
        public MensagemSucessoDto(string msg, int status)
        {
            this.msg = msg;
            this.status = status;
        }

		public MensagemSucessoDto(string msg, int status, object data)
		{
			this.msg = msg;
			this.status = status;
            this.data = data;
		}

		public string msg { get; set; }
        public int status { get; set; }
        public object data { get; set; }
    }
}
