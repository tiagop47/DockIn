import { Component, inject, signal } from '@angular/core';
import { ArtigosService } from '../services/ArtigosService';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MensagemDto } from '../Dto/MensagemDto';
import { ArtigoDto } from '../Dto/ArtigoDto';
import { CriarArtigoDto } from '../Dto/CriarArtigoDto';
import { HttpErrorResponse } from '@angular/common/http';
@Component({
  selector: 'app-gestao-artigos',
  imports: [ReactiveFormsModule],
  templateUrl: './gestao-artigos.html',
  styleUrl: './gestao-artigos.css',
})
export class GestaoArtigos {
  private readonly _service = inject(ArtigosService);

  artigoForm = new FormGroup({
    description: new FormControl('', [Validators.required]),
    peso: new FormControl<number | null>(null, [Validators.required, Validators.min(0.1)]),
    dimensoes: new FormControl<number | null>(null, [Validators.required]),
    classeArtigo: new FormControl<number>(0, [Validators.required]),
  });

  resposta = signal<MensagemDto<ArtigoDto> | null>(null);
  artigoCriado = signal<ArtigoDto | null>(null);

  submeterFormulario(): void {
    if (this.artigoForm.invalid) {
      this.artigoForm.markAllAsTouched();
      return;
    }

    this.resposta.set(null);

    const novoArtigo = this.artigoForm.value as CriarArtigoDto;

    this._service.criarArtigos(novoArtigo).subscribe({
      next: (artigoCriado: ArtigoDto) => {
        this.artigoCriado.set(artigoCriado);
        this.resposta.set(null);

        this.artigoForm.reset({
          description: '',
          peso: null,
          dimensoes: null,
          classeArtigo: 0,
        });
      },
      error: (err: HttpErrorResponse) => {
        this.artigoCriado.set(null);
        const apiErro = err.error as MensagemDto<string> | null;
        this.resposta.set({
          mensagem: apiErro?.mensagem || 'Erro ao criar artigo.',
          statusCode: err.status,
        });
      },
    });
  }
}
