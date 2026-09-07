package pt.ipp.estg.dockin.ui.navigation

sealed class Screen(val route: String) {
    data object ArtigosList : Screen("artigos_list")
    data object CriarArtigo : Screen("criar_artigo")
    data object ArtigoDetalhe : Screen("artigo_detalhe/{artigoId}") {
        fun createRoute(artigoId: Int) = "artigo_detalhe/$artigoId"
    }
}
