package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object OrganizationIntegrationTable : IdTable<String>("\"OrganizationIntegration\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val friendlyId = text("friendlyId").uniqueIndex("\"OrganizationIntegration_friendlyId_key\"")
    val service = pgEnum("service", "\"IntegrationService\"", IntegrationService.entries, IntegrationService::dbName)
    val externalOrganizationId = text("externalOrganizationId").nullable()
    val integrationData = jsonb<String>("integrationData", { it }, { it })
    val tokenReferenceId =
        reference(
            "tokenReferenceId",
            SecretReferenceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OrganizationIntegration_tokenReferenceId_fkey\"",
        )
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OrganizationIntegration_organizationId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"OrganizationIntegration_pkey\"")

    init {
        index("\"OrganizationIntegration_externalOrganizationId_idx\"", false, externalOrganizationId)
    }
}
