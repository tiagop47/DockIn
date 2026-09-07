package pt.ipp.estg.dockin.ui.navigation

import androidx.compose.runtime.Composable
import androidx.hilt.navigation.compose.hiltViewModel
import androidx.navigation.NavHostController
import androidx.navigation.compose.NavHost
import androidx.navigation.compose.composable
import androidx.navigation.compose.rememberNavController
import pt.ipp.estg.dockin.ui.artigos.ArtigosScreen
import pt.ipp.estg.dockin.ui.artigos.CriarArtigoScreen
import pt.ipp.estg.dockin.viewmodel.ArtigosViewModel
import pt.ipp.estg.dockin.viewmodel.CriarArtigoViewModel

@Composable
fun AppNavGraph(
    navController: NavHostController = rememberNavController()
) {
    NavHost(
        navController = navController,
        startDestination = Screen.ArtigosList.route
    ) {
        composable(Screen.ArtigosList.route) {
            val viewModel: ArtigosViewModel = hiltViewModel()
            ArtigosScreen(
                viewModel = viewModel,
                onArtigoClick = { _ ->
                },
                onNavigateToCriar = {
                    navController.navigate(Screen.CriarArtigo.route)
                }
            )
        }

        composable(Screen.CriarArtigo.route) {
            val viewModel: CriarArtigoViewModel = hiltViewModel()
            CriarArtigoScreen(
                viewModel = viewModel,
                onNavigateBack = {
                    navController.popBackStack()
                }
            )
        }
    }
}
