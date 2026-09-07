package pt.ipp.estg.dockin.utils

class ApiException(
    val statusCode: Int? = null,
    override val message: String,
    cause: Throwable? = null
) : Exception(message, cause)
