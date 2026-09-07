package pt.ipp.estg.dockin.viewmodel

import app.cash.turbine.test
import io.mockk.coEvery
import io.mockk.mockk
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.ExperimentalCoroutinesApi
import kotlinx.coroutines.flow.flowOf
import kotlinx.coroutines.test.StandardTestDispatcher
import kotlinx.coroutines.test.resetMain
import kotlinx.coroutines.test.runTest
import kotlinx.coroutines.test.setMain
import org.junit.After
import org.junit.Assert.assertEquals
import org.junit.Assert.assertFalse
import org.junit.Assert.assertTrue
import org.junit.Before
import org.junit.Test
import pt.ipp.estg.dockin.domain.model.Artigo
import pt.ipp.estg.dockin.domain.model.ClasseArtigo
import pt.ipp.estg.dockin.domain.usecase.ObterArtigosUseCase
import pt.ipp.estg.dockin.utils.Resource

@OptIn(ExperimentalCoroutinesApi::class)
class ArtigosViewModelTest {

    private val testDispatcher = StandardTestDispatcher()
    private val obterArtigosUseCase: ObterArtigosUseCase = mockk()

    @Before
    fun setUp() {
        Dispatchers.setMain(testDispatcher)
    }

    @After
    fun tearDown() {
        Dispatchers.resetMain()
    }

    @Test
    fun `quando carregarArtigos sucesso entao atualiza uiState com lista`() = runTest {
        val artigosMock = listOf(
            Artigo(
                id = 1,
                descricao = "Artigo Teste",
                peso = 10.0,
                dimensoes = 1.5,
                classe = ClasseArtigo.A,
                dataCriacao = "2026-09-05T00:00:00"
            )
        )

        coEvery { obterArtigosUseCase.invoke(any(), any()) } returns flowOf(
            Resource.Loading,
            Resource.Success(artigosMock)
        )

        val viewModel = ArtigosViewModel(obterArtigosUseCase)

        viewModel.uiState.test {
            // Estado inicial
            awaitItem()
            
            // Avança execução das coroutines
            testDispatcher.scheduler.advanceUntilIdle()

            val successState = expectMostRecentItem()
            assertFalse(successState.isLoading)
            assertEquals(1, successState.artigos.size)
            assertEquals("Artigo Teste", successState.artigos[0].descricao)
        }
    }

    @Test
    fun `quando carregarArtigos erro entao atualiza uiState com errorMessage`() = runTest {
        coEvery { obterArtigosUseCase.invoke(any(), any()) } returns flowOf(
            Resource.Loading,
            Resource.Error("Falha de rede")
        )

        val viewModel = ArtigosViewModel(obterArtigosUseCase)

        viewModel.uiState.test {
            awaitItem()
            testDispatcher.scheduler.advanceUntilIdle()

            val errorState = expectMostRecentItem()
            assertFalse(errorState.isLoading)
            assertTrue(errorState.artigos.isEmpty())
            assertEquals("Falha de rede", errorState.errorMessage)
        }
    }
}
