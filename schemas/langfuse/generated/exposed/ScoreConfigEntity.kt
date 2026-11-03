package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ScoreConfigEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ScoreConfigEntity>(ScoreConfigTable)

    var createdAt by ScoreConfigTable.createdAt
    var updatedAt by ScoreConfigTable.updatedAt
    var projectId by ScoreConfigTable.projectId
    var project by ProjectEntity referencedOn ScoreConfigTable.projectId
    var name by ScoreConfigTable.name
    var dataType by ScoreConfigTable.dataType
    var isArchived by ScoreConfigTable.isArchived
    var minValue by ScoreConfigTable.minValue
    var maxValue by ScoreConfigTable.maxValue
    var categories by ScoreConfigTable.categories
    var description by ScoreConfigTable.description

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ScoreConfigTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
