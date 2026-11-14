package langwatch.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object AuditLogTable : IdTable<String>("\"AuditLog\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val userId = text("userId").nullable()
    val projectId = text("projectId").nullable()
    val organizationId = text("organizationId").nullable()
    val action = text("action")
    val args = jsonb<String>("args", { it }, { it }).nullable()
    val error = text("error").nullable()
    val ipAddress = text("ipAddress").nullable()
    val userAgent = text("userAgent").nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val actorUserId = text("actorUserId").nullable()
    val targetKind = text("targetKind").nullable()
    val targetId = text("targetId").nullable()
    val before = jsonb<String>("before", { it }, { it }).nullable()
    val after = jsonb<String>("after", { it }, { it }).nullable()
    val idempotencyKey = text("idempotencyKey").nullable().uniqueIndex("\"AuditLog_idempotencyKey_key\"")
    override val primaryKey = PrimaryKey(id, name = "\"AuditLog_pkey\"")

    init {
        index("\"AuditLog_createdAt_idx\"", false, createdAt)
        index("\"AuditLog_userId_idx\"", false, userId)
        index("\"AuditLog_actorUserId_idx\"", false, actorUserId)
        index("\"AuditLog_projectId_idx\"", false, projectId)
        index("\"AuditLog_action_idx\"", false, action)
        index("\"AuditLog_ipAddress_idx\"", false, ipAddress)
        index("\"AuditLog_organizationId_createdAt_idx\"", false, organizationId, createdAt)
        index("\"AuditLog_targetKind_targetId_idx\"", false, targetKind, targetId)
    }
}
