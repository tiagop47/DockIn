package pt.ipp.estg.dockin.data.model

import com.google.gson.annotations.SerializedName

data class StockDto(
    @SerializedName("stockId")
    val stockId: Int,

    @SerializedName("description")
    val description: String? = null
)
