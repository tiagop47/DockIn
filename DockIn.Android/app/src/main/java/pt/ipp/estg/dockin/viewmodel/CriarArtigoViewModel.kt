package pt.ipp.estg.dockin.viewmodel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import pt.ipp.estg.dockin.domain.model.ClasseArtigo
import pt.ipp.estg.dockin.domain.usecase.CriarArtigoUseCase
import pt.ipp.estg.dockin.ui.artigos.CriarArtigoUiState
import pt.ipp.estg.dockin.utils.Resource
import javax.inject.Inject

@HiltViewModel
class CriarArtigoViewModel @Inject constructor(
    private val criarArtigoUseCase: CriarArtigoUseCase
) : ViewModel() {

    private val _uiState = MutableStateFlow(CriarArtigoUiState())
    val uiState: StateFlow<CriarArtigoUiState> = _uiState.asStateFlow()

    fun onDescricaoChanged(value: String) {
        _uiState.update { it.copy(descricao = value, errorMessage = null) }
    }

    fun onPesoChanged(value: String) {
        _uiState.update { it.copy(peso = value, errorMessage = null) }
    }

    fun onDimensoesChanged(value: String) {
        _uiState.update { it.copy(dimensoes = value, errorMessage = null) }
    }

    fun onClasseChanged(classe: ClasseArtigo) {
        _uiState.update { it.copy(classe = classe) }
    }

    fun submeterArtigo() {
        val currentState = _uiState.value
        val pesoDouble = currentState.peso.toDoubleOrNull()
        val dimensoesDouble = currentState.dimensoes.toDoubleOrNull()

        if (pesoDouble == null) {
            _uiState.update { it.copy(errorMessage = "Insira um peso válido.") }
            return
        }

        if (dimensoesDouble == null) {
            _uiState.update { it.copy(errorMessage = "Insira dimensões válidas.") }
            return
        }

        viewModelScope.launch {
            criarArtigoUseCase(
                descricao = currentState.descricao,
                peso = pesoDouble,
                dimensoes = dimensoesDouble,
                classe = currentState.classe
            ).collect { resource ->
                when (resource) {
                    is Resource.Loading -> {
                        _uiState.update { it.copy(isLoading = true, errorMessage = null) }
                    }
                    is Resource.Success -> {
                        _uiState.update { it.copy(isLoading = false, isSuccess = true) }
                    }
                    is Resource.Error -> {
                        _uiState.update { it.copy(isLoading = false, errorMessage = resource.message) }
                    }
                }
            }
        }
    }
}
