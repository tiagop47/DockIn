package pt.ipp.estg.dockin.data.remote

object ApiConfig {
    // Via adb reverse (cabo USB) ou emulador: localhost aponta diretamente para o backend
    const val BASE_URL = "http://localhost:5038/"
    const val CONNECT_TIMEOUT_SECONDS = 30L
    const val READ_TIMEOUT_SECONDS = 30L
}
