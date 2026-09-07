package pt.ipp.estg.dockin.data.model

import pt.ipp.estg.dockin.domain.model.Artigo
import pt.ipp.estg.dockin.domain.model.ClasseArtigo

fun ArtigoDto.toDomain(): Artigo {
    return Artigo(
        id = this.artigoId,
        descricao = this.description,
        peso = this.peso,
        dimensoes = this.dimensoes,
        classe = ClasseArtigo.fromInt(this.classeArtigo),
        dataCriacao = this.createdAt
    )
}

fun List<ArtigoDto>.toDomain(): List<Artigo> {
    return this.map { it.toDomain() }
}
