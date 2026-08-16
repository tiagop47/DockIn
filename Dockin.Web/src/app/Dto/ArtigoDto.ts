export interface ArtigoDto {
  artigoId: number;
  description: string;
  peso: number;
  dimensoes: number;
  classeArtigo: ArtigoClasses; // ou string/number se não tiveres enum
  createdAt: string; // ou Date
}
export enum ArtigoClasses {
  A,
  B,
  C,
}
