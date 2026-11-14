package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ProjectSecretEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ProjectSecretEntity>(ProjectSecretTable)

    var projectId by ProjectSecretTable.projectId
    var name by ProjectSecretTable.name
    var encryptedValue by ProjectSecretTable.encryptedValue
    var createdById by ProjectSecretTable.createdById
    var updatedById by ProjectSecretTable.updatedById
    var createdAt by ProjectSecretTable.createdAt
    var updatedAt by ProjectSecretTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ProjectSecretTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
