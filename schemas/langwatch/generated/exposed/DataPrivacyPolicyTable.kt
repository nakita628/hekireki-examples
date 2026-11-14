package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object DataPrivacyPolicyTable : IdTable<String>("\"DataPrivacyPolicy\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId")
    val scopeType = pgEnum("scopeType", "\"DataPrivacyScopeType\"", DataPrivacyScopeType.entries, DataPrivacyScopeType::dbName)
    val scopeId = text("scopeId")
    val personalOnly = bool("personalOnly").default(false)
    val config = jsonb<String>("config", { it }, { it })
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"DataPrivacyPolicy_pkey\"")

    init {
        index("\"DataPrivacyPolicy_organizationId_idx\"", false, organizationId)
        index("\"DataPrivacyPolicy_scopeType_scopeId_idx\"", false, scopeType, scopeId)
        uniqueIndex("\"DataPrivacyPolicy_scopeType_scopeId_personalOnly_key\"", scopeType, scopeId, personalOnly)
    }
}
