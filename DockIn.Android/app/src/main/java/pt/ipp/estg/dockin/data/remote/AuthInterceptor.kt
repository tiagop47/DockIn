package pt.ipp.estg.dockin.data.remote

import kotlinx.coroutines.runBlocking
import okhttp3.Interceptor
import okhttp3.Response
import pt.ipp.estg.dockin.data.local.SessionDataStore
import javax.inject.Inject
import javax.inject.Singleton

@Singleton
class AuthInterceptor @Inject constructor(
    private val sessionDataStore: SessionDataStore
) : Interceptor {

    override fun intercept(chain: Interceptor.Chain): Response {
        val originalRequest = chain.request()

        // Obtém o token JWT guardado no DataStore de forma síncrona dentro do pipeline do OkHttp
        val token = runBlocking {
            sessionDataStore.getAuthToken()
        }

        val requestBuilder = originalRequest.newBuilder()
            .header("Accept", "application/json")

        if (!token.isNullOrBlank()) {
            requestBuilder.header("Authorization", "Bearer $token")
        }

        val response = chain.proceed(requestBuilder.build())

        // Se a API responder 401 Unauthorized, pode-se limpar a sessão caso necessário
        if (response.code == 401) {
            runBlocking {
                sessionDataStore.clearSession()
            }
        }

        return response
    }
}
