package hoppscotch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class UserRequestEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, UserRequestEntity>(UserRequestTable)

    var collectionID by UserRequestTable.collectionID
    var userUid by UserRequestTable.userUid
    var title by UserRequestTable.title
    var request by UserRequestTable.request
    var type by UserRequestTable.type
    var mockExamples by UserRequestTable.mockExamples
    var orderIndex by UserRequestTable.orderIndex
    var createdOn by UserRequestTable.createdOn
    var updatedOn by UserRequestTable.updatedOn
    var userCollection by UserCollectionEntity referencedOn UserRequestTable.collectionID
    var user by UserEntity referencedOn UserRequestTable.userUid

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(UserRequestTable.updatedOn)) {
                updatedOn = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
