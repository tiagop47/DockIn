import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { ArtigoDto } from '../Dto/ArtigoDto';
import { Observable } from 'rxjs';
import { PaginacaoDto } from '../Dto/PaginacaoDto';
import { CriarArtigoDto } from '../Dto/CriarArtigoDto';

@Injectable({
  providedIn: 'root',
})
export class ArtigosService {
  private http = inject(HttpClient);
  private readonly API_URL = environment.apiUrl;

  getArtigos(pagina: PaginacaoDto): Observable<ArtigoDto[]> {
    return this.http.get<ArtigoDto[]>(`${this.API_URL}/artigos`, {
      params: {
        pagina: pagina.pagina,
        tamanhoPagina: pagina.tamanhoPagina,
      },
    });
  }

  getArtigoById(id: number): Observable<ArtigoDto> {
    return this.http.get<ArtigoDto>(`${this.API_URL}/artigos/${id}`);
  }

  criarArtigos(artigo: CriarArtigoDto): Observable<ArtigoDto> {
    return this.http.post<ArtigoDto>(`${this.API_URL}/artigos`, artigo);
  }
}
