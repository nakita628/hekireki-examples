package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ManagedOrganizationTable : IdTable<Int>("\"ManagedOrganization\"") {
    override val id =
        integer("managedOrganizationId")
            .references(
                TeamTable.id,
                onDelete = ReferenceOption.CASCADE,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "\"ManagedOrganization_managedOrganizationId_fkey\"",
            ).entityId()
    val managerOrganizationId =
        reference(
            "managerOrganizationId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ManagedOrganization_managerOrganizationId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }

    init {
        uniqueIndex("\"ManagedOrganization_managedOrganizationId_key\"", id)
        index("\"ManagedOrganization_managerOrganizationId_idx\"", false, managerOrganizationId)
        uniqueIndex("ManagedOrganization_managerOrganizationId_managedOrganizati_key", managerOrganizationId, id)
    }
}
