package jitsu.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object AuditLogTable : IdTable<String>("\"AuditLog\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val timestamp = pgTimestamp("timestamp", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val type = text("type")
    val severity = text("severity").nullable().default("info")
    val userId = text("userId").nullable()
    val workspaceId = text("workspaceId").nullable()
    val objectId = text("objectId").nullable()
    val changes = jsonb<String>("changes", { it }, { it }).nullable()
    val authType = text("authType").nullable()
    val tokenId = text("tokenId").nullable()
    val ip = text("ip").nullable()
    val requestHeaders = jsonb<String>("requestHeaders", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"AuditLog_pkey\"")

    init {
        index("\"AuditLog_timestamp_idx\"", false, timestamp)
        index("\"AuditLog_type_idx\"", false, type)
        index("\"AuditLog_workspaceId_timestamp_idx\"", false, workspaceId, timestamp)
    }
}
