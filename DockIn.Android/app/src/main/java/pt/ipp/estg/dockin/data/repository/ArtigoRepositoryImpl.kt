package pt.ipp.estg.dockin.data.repository

import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.flow
import kotlinx.coroutines.flow.flowOn
import pt.ipp.estg.dockin.data.model.CriarArtigoRequest
import pt.ipp.estg.dockin.data.model.toDomain
import pt.ipp.estg.dockin.data.remote.api.ArtigoApiService
import pt.ipp.estg.dockin.data.remote.safeApiCall
import pt.ipp.estg.dockin.domain.model.Artigo
import pt.ipp.estg.dockin.domain.model.ClasseArtigo
import pt.ipp.estg.dockin.domain.repository.ArtigoRepository
import pt.ipp.estg.dockin.utils.Resource
import javax.inject.Inject
import javax.inject.Singleton

@Singleton
class ArtigoRepositoryImpl @Inject constructor(
    private val apiService: ArtigoApiService
) : ArtigoRepository {

    override fun obterArtigos(pagina: Int, tamanho: Int): Flow<Resource<List<Artigo>>> = flow {
        emit(Resource.Loading)
        val result = safeApiCall(
            apiCall = { apiService.obterArtigosPaginados(pagina, tamanho) },
            transform = { listDto -> listDto.toDomain() }
        )
        emit(result)
    }.flowOn(Dispatchers.IO)

    override fun obterArtigoPorId(id: Int): Flow<Resource<Artigo>> = flow {
        emit(Resource.Loading)
        val result = safeApiCall(
            apiCall = { apiService.obterArtigoPorId(id) },
            transform = { dto -> dto.toDomain() }
        )
        emit(result)
    }.flowOn(Dispatchers.IO)

    override fun criarArtigo(
        descricao: String,
        peso: Double,
        dimensoes: Double,
        classe: ClasseArtigo
    ): Flow<Resource<Artigo>> = flow {
        emit(Resource.Loading)
        val request = CriarArtigoRequest(
            description = descricao,
            peso = peso,
            dimensoes = dimensoes,
            classeArtigo = classe.valor
        )
        val result = safeApiCall(
            apiCall = { apiService.criarArtigo(request) },
            transform = { dto -> dto.toDomain() }
        )
        emit(result)
    }.flowOn(Dispatchers.IO)
}
