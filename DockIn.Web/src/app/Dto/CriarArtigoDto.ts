import { ArtigoClasses } from './ArtigoDto';

export interface CriarArtigoDto {
  Description: string;
  Peso: number;
  Dimensoes: number;
  ClasseArtigo: ArtigoClasses;
}
