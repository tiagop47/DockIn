package pt.ipp.estg.dockin.data.remote.dto

import java.text.DecimalFormat

data class ArtigoDto(
    val artigoId: Int,
    val description: String,
    val peso: DecimalFormat,
    val dimensoes: DecimalFormat,
    val artigoClasses: ArtigoClasses

) {
    enum class ArtigoClasses {
        A,
        B,
        C
    }
}
