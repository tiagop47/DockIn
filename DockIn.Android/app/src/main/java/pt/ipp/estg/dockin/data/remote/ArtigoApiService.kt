package pt.ipp.estg.dockin.data.remote

import pt.ipp.estg.dockin.data.remote.dto.ArtigoDto
import retrofit2.http.GET
import retrofit2.http.Query

interface ArtigoApiService {
    @GET("api/artigos")
    suspend fun listarArtigosPaginado(
        @Query("pagina") pagina: Int = 1,
        @Query("tamanho") tamanho: Int = 10
    ): List<ArtigoDto>;
}