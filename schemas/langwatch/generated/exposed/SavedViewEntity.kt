package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SavedViewEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SavedViewEntity>(SavedViewTable)

    var projectId by SavedViewTable.projectId
    var userId by SavedViewTable.userId
    var name by SavedViewTable.name
    var filters by SavedViewTable.filters
    var query by SavedViewTable.query
    var period by SavedViewTable.period
    var order by SavedViewTable.order
    var kind by SavedViewTable.kind
    var createdAt by SavedViewTable.createdAt
    var updatedAt by SavedViewTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SavedViewTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
