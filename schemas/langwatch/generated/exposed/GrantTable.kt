package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object GrantTable : IdTable<String>("\"Grant\"") {
    override val id = text("id").entityId()
    val organizationId = text("organizationId")
    val principalType = pgEnum("principalType", "\"GrantPrincipalType\"", GrantPrincipalType.entries, GrantPrincipalType::dbName)
    val principalId = text("principalId").nullable()
    val roleKey = text("roleKey").nullable()
    val legacyRole = text("legacyRole").nullable()
    val source1 = text("source")
    val scopeType = pgEnum("scopeType", "\"GrantScopeType\"", GrantScopeType.entries, GrantScopeType::dbName)
    val scopeId = text("scopeId")
    val token = text("token").nullable().uniqueIndex("\"Grant_token_key\"")
    val permission = text("permission").nullable()
    val resourceKind = text("resourceKind").nullable()
    val projectId = text("projectId").nullable()
    val createdByUserId = text("createdByUserId").nullable()
    val expiresAt = pgTimestamp("expiresAt", 3).nullable()
    val maxViews = integer("maxViews").nullable()
    val condition = jsonb<String>("condition", { it }, { it }).nullable()
    val replacesGrantId = text("replacesGrantId").nullable()
    val occurredAt = pgTimestamp("occurredAt", 3)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val revokedAt = pgTimestamp("revokedAt", 3).nullable()
    val revokedReason = text("revokedReason").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Grant_pkey\"")

    init {
        index("\"Grant_organizationId_scopeType_scopeId_idx\"", false, organizationId, scopeType, scopeId)
        index("\"Grant_projectId_resourceKind_scopeId_idx\"", false, projectId, resourceKind, scopeId)
    }
}
