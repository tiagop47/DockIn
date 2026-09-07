package pt.ipp.estg.dockin.ui.artigos

import pt.ipp.estg.dockin.domain.model.ClasseArtigo

data class CriarArtigoUiState(
    val descricao: String = "",
    val peso: String = "",
    val dimensoes: String = "",
    val classe: ClasseArtigo = ClasseArtigo.A,
    val isLoading: Boolean = false,
    val errorMessage: String? = null,
    val isSuccess: Boolean = false
)
