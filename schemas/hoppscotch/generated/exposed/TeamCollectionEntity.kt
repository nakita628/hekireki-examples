package hoppscotch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TeamCollectionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TeamCollectionEntity>(TeamCollectionTable)

    var parentID by TeamCollectionTable.parentID
    var teamID by TeamCollectionTable.teamID
    var title by TeamCollectionTable.title
    var orderIndex by TeamCollectionTable.orderIndex
    var data by TeamCollectionTable.data
    var createdOn by TeamCollectionTable.createdOn
    var updatedOn by TeamCollectionTable.updatedOn
    var parent by TeamCollectionEntity optionalReferencedOn TeamCollectionTable.parentID
    val children by TeamCollectionEntity optionalReferrersOn TeamCollectionTable.parentID
    var team by TeamEntity referencedOn TeamCollectionTable.teamID
    val requests by TeamRequestEntity referrersOn TeamRequestTable.collectionID

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TeamCollectionTable.updatedOn)) {
                updatedOn = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
