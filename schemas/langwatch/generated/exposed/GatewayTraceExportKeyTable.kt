package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object GatewayTraceExportKeyTable : IdTable<String>("\"GatewayTraceExportKey\"") {
    override val id = text("projectId").entityId()
    val apiKeyId = text("apiKeyId")
    val encryptedToken = text("encryptedToken")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"GatewayTraceExportKey_pkey\"")
}
