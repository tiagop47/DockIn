package pt.ipp.estg.dockin.data.model

import com.google.gson.annotations.SerializedName

data class ArtigoDto(
    @SerializedName("artigoId")
    val artigoId: Int,

    @SerializedName("description")
    val description: String,

    @SerializedName("peso")
    val peso: Double,

    @SerializedName("dimensoes")
    val dimensoes: Double,

    @SerializedName("classeArtigo")
    val classeArtigo: Int,

    @SerializedName("createdAt")
    val createdAt: String? = null
)

data class CriarArtigoRequest(
    @SerializedName("description")
    val description: String,

    @SerializedName("peso")
    val peso: Double,

    @SerializedName("dimensoes")
    val dimensoes: Double,

    @SerializedName("classeArtigo")
    val classeArtigo: Int
)
