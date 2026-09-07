package pt.ipp.estg.dockin.domain.repository

import kotlinx.coroutines.flow.Flow
import pt.ipp.estg.dockin.domain.model.Artigo
import pt.ipp.estg.dockin.domain.model.ClasseArtigo
import pt.ipp.estg.dockin.utils.Resource

interface ArtigoRepository {
    fun obterArtigos(pagina: Int = 1, tamanho: Int = 20): Flow<Resource<List<Artigo>>>
    fun obterArtigoPorId(id: Int): Flow<Resource<Artigo>>
    fun criarArtigo(descricao: String, peso: Double, dimensoes: Double, classe: ClasseArtigo): Flow<Resource<Artigo>>
}
