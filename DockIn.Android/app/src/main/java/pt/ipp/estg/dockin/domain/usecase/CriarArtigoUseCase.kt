package pt.ipp.estg.dockin.domain.usecase

import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.flow
import pt.ipp.estg.dockin.domain.model.Artigo
import pt.ipp.estg.dockin.domain.model.ClasseArtigo
import pt.ipp.estg.dockin.domain.repository.ArtigoRepository
import pt.ipp.estg.dockin.utils.Resource
import javax.inject.Inject

class CriarArtigoUseCase @Inject constructor(
    private val repository: ArtigoRepository
) {
    operator fun invoke(
        descricao: String,
        peso: Double,
        dimensoes: Double,
        classe: ClasseArtigo
    ): Flow<Resource<Artigo>> {
        if (descricao.isBlank()) {
            return flow {
                emit(Resource.Error("A descrição do artigo é obrigatória."))
            }
        }
        if (peso <= 0) {
            return flow {
                emit(Resource.Error("O peso deve ser superior a zero."))
            }
        }
        if (dimensoes <= 0) {
            return flow {
                emit(Resource.Error("As dimensões devem ser superiores a zero."))
            }
        }
        return repository.criarArtigo(descricao.trim(), peso, dimensoes, classe)
    }
}
