package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object IntegrationAttributeSyncTable : IdTable<String>("\"IntegrationAttributeSync\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val organizationId =
        reference(
            "organizationId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"IntegrationAttributeSync_organizationId_fkey\"",
        )
    val name = text("name")
    val integration = text("integration")
    val credentialId =
        optReference(
            "credentialId",
            CredentialTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"IntegrationAttributeSync_credentialId_fkey\"",
        )
    val enabled = bool("enabled")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"IntegrationAttributeSync_pkey\"")

    init {
        index("\"IntegrationAttributeSync_organizationId_idx\"", false, organizationId)
        index("\"IntegrationAttributeSync_credentialId_idx\"", false, credentialId)
    }
}
