package pt.ipp.estg.dockin.data.remote.api

import pt.ipp.estg.dockin.data.model.ArtigoDto
import pt.ipp.estg.dockin.data.model.CriarArtigoRequest
import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST
import retrofit2.http.Path
import retrofit2.http.Query

interface ArtigoApiService {

    @GET("api/artigos")
    suspend fun obterArtigosPaginados(
        @Query("pagina") pagina: Int = 1,
        @Query("tamanho") tamanho: Int = 20
    ): Response<List<ArtigoDto>>

    @GET("api/artigos/{id}")
    suspend fun obterArtigoPorId(
        @Path("id") id: Int
    ): Response<ArtigoDto>

    @POST("api/artigos")
    suspend fun criarArtigo(
        @Body request: CriarArtigoRequest
    ): Response<ArtigoDto>
}
