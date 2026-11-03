package hoppscotch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class UserCollectionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, UserCollectionEntity>(UserCollectionTable)

    var parentID by UserCollectionTable.parentID
    var userUid by UserCollectionTable.userUid
    var title by UserCollectionTable.title
    var type by UserCollectionTable.type
    var orderIndex by UserCollectionTable.orderIndex
    var data by UserCollectionTable.data
    var createdOn by UserCollectionTable.createdOn
    var updatedOn by UserCollectionTable.updatedOn
    var parent by UserCollectionEntity optionalReferencedOn UserCollectionTable.parentID
    val children by UserCollectionEntity optionalReferrersOn UserCollectionTable.parentID
    var user by UserEntity referencedOn UserCollectionTable.userUid
    val requests by UserRequestEntity referrersOn UserRequestTable.collectionID

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(UserCollectionTable.updatedOn)) {
                updatedOn = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
