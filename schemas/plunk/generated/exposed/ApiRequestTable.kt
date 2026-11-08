package plunk.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ApiRequestTable : IdTable<String>("api_requests") {
    override val id = text("id").entityId()
    val method = text("method")
    val path = text("path")
    val statusCode = integer("statusCode")
    val duration = integer("duration")
    val projectId =
        optReference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"api_requests_projectId_fkey\"",
        )
    val userId = text("userId").nullable()
    val authType = text("authType").nullable()
    val ip = text("ip").nullable()
    val userAgent = text("userAgent").nullable()
    val errorCode = text("errorCode").nullable()
    val errorMessage = text("errorMessage").nullable()
    val requestSize = integer("requestSize").nullable()
    val responseSize = integer("responseSize").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "api_requests_pkey")

    init {
        index(
            "\"api_requests_projectId_createdAt_idx\"",
            false,
            functions = listOf(indexExpression("\"projectId\""), indexExpression("\"createdAt\" DESC")),
        )
        index(
            "\"api_requests_userId_createdAt_idx\"",
            false,
            functions = listOf(indexExpression("\"userId\""), indexExpression("\"createdAt\" DESC")),
        )
        index(
            "\"api_requests_statusCode_createdAt_idx\"",
            false,
            functions = listOf(indexExpression("\"statusCode\""), indexExpression("\"createdAt\" DESC")),
        )
        index(
            "\"api_requests_path_createdAt_idx\"",
            false,
            functions = listOf(indexExpression("\"path\""), indexExpression("\"createdAt\" DESC")),
        )
        index("\"api_requests_createdAt_idx\"", false, functions = listOf(indexExpression("\"createdAt\" DESC")))
        index(
            "\"api_requests_projectId_statusCode_createdAt_idx\"",
            false,
            functions = listOf(indexExpression("\"projectId\""), indexExpression("\"statusCode\""), indexExpression("\"createdAt\" DESC")),
        )
    }
}
