package pt.ipp.estg.dockin.data.remote

import com.google.gson.Gson
import com.google.gson.JsonObject
import pt.ipp.estg.dockin.utils.ApiException
import pt.ipp.estg.dockin.utils.Resource
import retrofit2.Response

/**
 * Utilitário seguro para executar chamadas de rede Retrofit e mapear erros ASP.NET Core
 * para o wrapper [Resource].
 */
suspend fun <T, R> safeApiCall(
    apiCall: suspend () -> Response<T>,
    transform: (T) -> R
): Resource<R> {
    return try {
        val response = apiCall()
        if (response.isSuccessful) {
            val body = response.body()
            if (body != null) {
                Resource.Success(transform(body))
            } else {
                Resource.Error("Resposta do servidor vazia.")
            }
        } else {
            val errorBodyString = response.errorBody()?.string()
            val parsedErrorMessage = try {
                val jsonObject = Gson().fromJson(errorBodyString, JsonObject::class.java)
                when {
                    jsonObject.has("mensagem") -> jsonObject.get("mensagem").asString
                    jsonObject.has("message") -> jsonObject.get("message").asString
                    jsonObject.has("title") -> jsonObject.get("title").asString
                    else -> null
                }
            } catch (e: Exception) {
                null
            }

            val errorMessage = parsedErrorMessage ?: when (response.code()) {
                400 -> "Pedido inválido (Bad Request)."
                401 -> "Sessão expirada ou não autorizada. Faça login novamente."
                403 -> "Sem permissão para aceder a este recurso."
                404 -> "Recurso não encontrado no servidor."
                500 -> "Erro interno no servidor ASP.NET Core."
                else -> "Erro na comunicação: HTTP ${response.code()}"
            }

            Resource.Error(errorMessage, ApiException(statusCode = response.code(), message = errorMessage))
        }
    } catch (e: java.net.ConnectException) {
        Resource.Error("Não foi possível conectar ao servidor. Verifique a ligação e o backend.", e)
    } catch (e: java.net.SocketTimeoutException) {
        Resource.Error("O pedido expirou (Timeout). Tente novamente.", e)
    } catch (e: Exception) {
        Resource.Error(e.localizedMessage ?: "Ocorreu um erro inesperado.", e)
    }
}
