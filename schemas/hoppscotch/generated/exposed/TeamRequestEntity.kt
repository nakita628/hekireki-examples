package hoppscotch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TeamRequestEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TeamRequestEntity>(TeamRequestTable)

    var collectionID by TeamRequestTable.collectionID
    var teamID by TeamRequestTable.teamID
    var title by TeamRequestTable.title
    var request by TeamRequestTable.request
    var mockExamples by TeamRequestTable.mockExamples
    var orderIndex by TeamRequestTable.orderIndex
    var createdOn by TeamRequestTable.createdOn
    var updatedOn by TeamRequestTable.updatedOn
    var collection by TeamCollectionEntity referencedOn TeamRequestTable.collectionID
    var team by TeamEntity referencedOn TeamRequestTable.teamID

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TeamRequestTable.updatedOn)) {
                updatedOn = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
