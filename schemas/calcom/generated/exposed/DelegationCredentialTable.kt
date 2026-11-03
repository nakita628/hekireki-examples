package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object DelegationCredentialTable : IdTable<String>("\"DelegationCredential\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val workspacePlatformId =
        reference(
            "workspacePlatformId",
            WorkspacePlatformTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DelegationCredential_workspacePlatformId_fkey\"",
        )
    val serviceAccountKey = jsonb<String>("serviceAccountKey", { it }, { it })
    val enabled = bool("enabled").default(false)
    val lastEnabledAt = pgTimestamp("lastEnabledAt", 3).nullable()
    val lastDisabledAt = pgTimestamp("lastDisabledAt", 3).nullable()
    val organizationId =
        reference(
            "organizationId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DelegationCredential_organizationId_fkey\"",
        )
    val domain = text("domain")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"DelegationCredential_pkey\"")

    init {
        index("\"DelegationCredential_enabled_idx\"", false, enabled)
        uniqueIndex("\"DelegationCredential_organizationId_domain_key\"", organizationId, domain)
    }
}
