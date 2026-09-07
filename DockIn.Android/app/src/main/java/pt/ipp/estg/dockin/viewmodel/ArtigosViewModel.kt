package pt.ipp.estg.dockin.viewmodel

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.asStateFlow
import kotlinx.coroutines.flow.update
import kotlinx.coroutines.launch
import pt.ipp.estg.dockin.domain.usecase.ObterArtigosUseCase
import pt.ipp.estg.dockin.ui.artigos.ArtigosUiState
import pt.ipp.estg.dockin.utils.Resource
import javax.inject.Inject

@HiltViewModel
class ArtigosViewModel @Inject constructor(
    private val obterArtigosUseCase: ObterArtigosUseCase
) : ViewModel() {

    private val _uiState = MutableStateFlow(ArtigosUiState())
    val uiState: StateFlow<ArtigosUiState> = _uiState.asStateFlow()

    init {
        carregarArtigos()
    }

    fun carregarArtigos() {
        viewModelScope.launch {
            obterArtigosUseCase().collect { resource ->
                when (resource) {
                    is Resource.Loading -> {
                        _uiState.update { it.copy(isLoading = true, errorMessage = null) }
                    }
                    is Resource.Success -> {
                        _uiState.update {
                            it.copy(
                                isLoading = false,
                                artigos = resource.data,
                                errorMessage = null
                            )
                        }
                    }
                    is Resource.Error -> {
                        _uiState.update {
                            it.copy(
                                isLoading = false,
                                errorMessage = resource.message
                            )
                        }
                    }
                }
            }
        }
    }
}
