import { Component, inject, numberAttribute, OnInit, signal } from '@angular/core';
import { ArtigosService } from '../services/ArtigosService';
import { ArtigoDto } from '../Dto/ArtigoDto';
import { HttpErrorResponse } from '@angular/common/http';
import { MensagemDto } from '../Dto/MensagemDto';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { debounceTime, distinctUntilChanged } from 'rxjs';
import { PaginacaoDto } from '../Dto/PaginacaoDto';
import { RouterLink, RouterLinkActive } from "@angular/router";

@Component({
  selector: 'app-home-page',
  imports: [ReactiveFormsModule, RouterLink, RouterLinkActive],
  templateUrl: './home-page.html',
  styleUrl: './home-page.css',
})
export class HomePage implements OnInit {
  private _service = inject(ArtigosService);
  artigos = signal<ArtigoDto[]>([]);
  erro = signal<MensagemDto<string> | null>(null);
  artigoSelecionado = signal<ArtigoDto | null>(null);

  pesquisaController = new FormControl<number | null>(null);

  paginacaoAtual = signal<PaginacaoDto>({ pagina: 1, tamanho: 5 });
  isUltimaPagina = signal<boolean>(false);

  ngOnInit(): void {
    this.carregarPaginacaoArtigos(this.paginacaoAtual());

    this.pesquisaController.valueChanges
      .pipe(debounceTime(400), distinctUntilChanged())
      .subscribe((id) => {
        if (id) {
          this.verDetalhesArtigo(id);
        } else {
          this.artigoSelecionado.set(null);
          this.erro.set(null);
        }
      });
  }

  carregarPaginacaoArtigos(pagina: PaginacaoDto): void {
    this.erro.set(null);
    this._service.getArtigos(pagina).subscribe({
      next: (artigos) => {
        this.artigos.set(artigos);

        const chegouAoFim = artigos.length < this.paginacaoAtual().tamanho;
        this.isUltimaPagina.set(chegouAoFim);
      },
      error: (err: HttpErrorResponse) => {
        const apiErro = err.error as MensagemDto<string> | null;

        this.erro.set({
          mensagem: apiErro?.mensagem || 'Erro ao carregar lista de artigos.',
          statusCode: apiErro?.statusCode || err.status,
        });
      },
    });
  }

  mudarPagina(numeroPagina: number): void {
    const novaPagina: PaginacaoDto = {
      pagina: numeroPagina,
      tamanho: this.paginacaoAtual().tamanho,
    };

    this.paginacaoAtual.set(novaPagina);
    this.carregarPaginacaoArtigos(novaPagina);
  }

  verDetalhesArtigo(id: number): void {
    this.erro.set(null);
    this._service.getArtigoById(id).subscribe({
      next: (artigo) => {
        this.artigoSelecionado.set(artigo);
        this.erro.set(null);
      },
      error: (err: HttpErrorResponse) => {
        this.artigoSelecionado.set(null);
        const apiErro = err.error as MensagemDto<string> | null;

        this.erro.set({
          mensagem: apiErro?.mensagem || 'Artigo não encontrado.',
          statusCode: apiErro?.statusCode || err.status,
        });
      },
    });
  }
}
