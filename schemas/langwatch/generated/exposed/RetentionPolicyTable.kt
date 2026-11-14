package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object RetentionPolicyTable : IdTable<String>("\"RetentionPolicy\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId")
    val scopeType = pgEnum("scopeType", "\"RetentionPolicyScopeType\"", RetentionPolicyScopeType.entries, RetentionPolicyScopeType::dbName)
    val scopeId = text("scopeId")
    val category = text("category")
    val retentionDays = integer("retentionDays")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"RetentionPolicy_pkey\"")

    init {
        index("\"RetentionPolicy_organizationId_idx\"", false, organizationId)
        index("\"RetentionPolicy_scopeType_scopeId_idx\"", false, scopeType, scopeId)
        uniqueIndex("\"RetentionPolicy_scopeType_scopeId_category_key\"", scopeType, scopeId, category)
    }
}
