package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object RoleBindingTable : IdTable<String>("\"RoleBinding\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId")
    val userId = text("userId").nullable()
    val groupId = text("groupId").nullable()
    val apiKeyId = text("apiKeyId").nullable()
    val role = pgEnum("role", "\"TeamUserRole\"", TeamUserRole.entries, TeamUserRole::dbName)
    val customRoleId = text("customRoleId").nullable()
    val scopeType = pgEnum("scopeType", "\"RoleBindingScopeType\"", RoleBindingScopeType.entries, RoleBindingScopeType::dbName)
    val scopeId = text("scopeId")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"RoleBinding_pkey\"")

    init {
        index("\"RoleBinding_organizationId_idx\"", false, organizationId)
        index("\"RoleBinding_userId_idx\"", false, userId)
        index("\"RoleBinding_groupId_idx\"", false, groupId)
        index("\"RoleBinding_apiKeyId_idx\"", false, apiKeyId)
        index("\"RoleBinding_scopeType_scopeId_idx\"", false, scopeType, scopeId)
    }
}
