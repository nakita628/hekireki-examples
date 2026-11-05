package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

class CohortEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, CohortEntity>(CohortTable)

    var name by CohortTable.name
    var description by CohortTable.description
    var projectId by CohortTable.projectId
    var project by ProjectEntity referencedOn CohortTable.projectId
    var definition by CohortTable.definition
    var isStatic by CohortTable.isStatic
    var profileCount by CohortTable.profileCount
    var lastComputedAt by CohortTable.lastComputedAt
    var createdAt by CohortTable.createdAt
    var updatedAt by CohortTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CohortTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
