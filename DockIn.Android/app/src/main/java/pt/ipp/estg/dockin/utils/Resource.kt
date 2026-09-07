package pt.ipp.estg.dockin.utils

/**
 * Wrapper sealed interface para gerir o estado de respostas e operações assíncronas.
 */
sealed interface Resource<out T> {
    data class Success<out T>(val data: T) : Resource<T>
    data class Error(val message: String, val cause: Throwable? = null) : Resource<Nothing>
    data object Loading : Resource<Nothing>
}
