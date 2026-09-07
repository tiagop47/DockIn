package pt.ipp.estg.dockin.domain.model

enum class ClasseArtigo(val valor: Int, val label: String) {
    A(0, "Classe A"),
    B(1, "Classe B"),
    C(2, "Classe C");

    companion object {
        fun fromInt(valor: Int): ClasseArtigo {
            return entries.find { it.valor == valor } ?: A
        }
    }
}

data class Artigo(
    val id: Int,
    val descricao: String,
    val peso: Double,
    val dimensoes: Double,
    val classe: ClasseArtigo,
    val dataCriacao: String?
)
