package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class EnvironmentVariableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, EnvironmentVariableEntity>(EnvironmentVariableTable)

    var friendlyId by EnvironmentVariableTable.friendlyId
    var key by EnvironmentVariableTable.key
    var project by ProjectEntity referencedOn EnvironmentVariableTable.projectId
    var projectId by EnvironmentVariableTable.projectId
    var createdAt by EnvironmentVariableTable.createdAt
    var updatedAt by EnvironmentVariableTable.updatedAt
    val values by EnvironmentVariableValueEntity referrersOn EnvironmentVariableValueTable.variableId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(EnvironmentVariableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
