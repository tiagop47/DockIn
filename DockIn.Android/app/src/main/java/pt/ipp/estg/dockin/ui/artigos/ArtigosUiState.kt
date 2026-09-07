package pt.ipp.estg.dockin.ui.artigos

import pt.ipp.estg.dockin.domain.model.Artigo

data class ArtigosUiState(
    val isLoading: Boolean = false,
    val artigos: List<Artigo> = emptyList(),
    val errorMessage: String? = null
)
