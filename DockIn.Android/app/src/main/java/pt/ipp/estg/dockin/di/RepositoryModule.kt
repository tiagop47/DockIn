package pt.ipp.estg.dockin.di

import dagger.Binds
import dagger.Module
import dagger.hilt.InstallIn
import dagger.hilt.components.SingletonComponent
import pt.ipp.estg.dockin.data.repository.ArtigoRepositoryImpl
import pt.ipp.estg.dockin.domain.repository.ArtigoRepository
import javax.inject.Singleton

@Module
@InstallIn(SingletonComponent::class)
abstract class RepositoryModule {

    @Binds
    @Singleton
    abstract fun bindArtigoRepository(
        artigoRepositoryImpl: ArtigoRepositoryImpl
    ): ArtigoRepository
}
