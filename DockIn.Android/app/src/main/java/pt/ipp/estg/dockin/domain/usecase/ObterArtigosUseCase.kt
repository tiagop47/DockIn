package pt.ipp.estg.dockin.domain.usecase

import kotlinx.coroutines.flow.Flow
import pt.ipp.estg.dockin.domain.model.Artigo
import pt.ipp.estg.dockin.domain.repository.ArtigoRepository
import pt.ipp.estg.dockin.utils.Resource
import javax.inject.Inject

class ObterArtigosUseCase @Inject constructor(
    private val repository: ArtigoRepository
) {
    operator fun invoke(pagina: Int = 1, tamanho: Int = 20): Flow<Resource<List<Artigo>>> {
        return repository.obterArtigos(pagina, tamanho)
    }
}
