package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class FilterSegmentEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, FilterSegmentEntity>(FilterSegmentTable)

    var name by FilterSegmentTable.name
    var tableIdentifier by FilterSegmentTable.tableIdentifier
    var scope by FilterSegmentTable.scope
    var activeFilters by FilterSegmentTable.activeFilters
    var sorting by FilterSegmentTable.sorting
    var columnVisibility by FilterSegmentTable.columnVisibility
    var columnSizing by FilterSegmentTable.columnSizing
    var perPage by FilterSegmentTable.perPage
    var searchTerm by FilterSegmentTable.searchTerm
    var createdAt by FilterSegmentTable.createdAt
    var updatedAt by FilterSegmentTable.updatedAt
    var user by UserEntity referencedOn FilterSegmentTable.userId
    var userId by FilterSegmentTable.userId
    var team by TeamEntity optionalReferencedOn FilterSegmentTable.teamId
    var teamId by FilterSegmentTable.teamId
    val userPreferences by UserFilterSegmentPreferenceEntity optionalReferrersOn UserFilterSegmentPreferenceTable.segmentId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(FilterSegmentTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
