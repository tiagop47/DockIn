export interface MensagemDto<T> {
  mensagem: string;
  statusCode?: number;
  data?: T;
}
