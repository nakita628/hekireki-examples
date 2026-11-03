package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object OrganizationProjectIntegrationTable : IdTable<String>("\"OrganizationProjectIntegration\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val organizationIntegrationId =
        reference(
            "organizationIntegrationId",
            OrganizationIntegrationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OrganizationProjectIntegration_organizationIntegrationId_fkey\"",
        )
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OrganizationProjectIntegration_projectId_fkey\"",
        )
    val externalEntityId = text("externalEntityId")
    val integrationData = jsonb<String>("integrationData", { it }, { it })
    val installedBy = text("installedBy").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"OrganizationProjectIntegration_pkey\"")

    init {
        index("\"OrganizationProjectIntegration_projectId_idx\"", false, projectId)
        index("OrganizationProjectIntegration_projectId_organizationIntegr_idx", false, projectId, organizationIntegrationId)
        index("\"OrganizationProjectIntegration_externalEntityId_idx\"", false, externalEntityId)
    }
}
