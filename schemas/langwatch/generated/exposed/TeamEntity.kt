package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TeamEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TeamEntity>(TeamTable)

    var name by TeamTable.name
    var slug by TeamTable.slug
    var organizationId by TeamTable.organizationId
    var createdAt by TeamTable.createdAt
    var updatedAt by TeamTable.updatedAt
    var archivedAt by TeamTable.archivedAt
    var isPersonal by TeamTable.isPersonal
    var ownerUserId by TeamTable.ownerUserId
    var departmentId by TeamTable.departmentId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TeamTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
