package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TagEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TagEntity>(TagTable)

    var name by TagTable.name
    var color by TagTable.color
    var description by TagTable.description
    var teamId by TagTable.teamId
    var team by TeamEntity referencedOn TagTable.teamId
    val items by TagItemEntity referrersOn TagItemTable.tagId
    var createdBy by TagTable.createdBy
    var createdAt by TagTable.createdAt
    var updatedAt by TagTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TagTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
