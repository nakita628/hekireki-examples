package linkwarden.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class UsersAndCollectionsEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<UsersAndCollectionsEntity>(UsersAndCollectionsTable)

    val user by UserEntity referencedOn UsersAndCollectionsTable.userId
    val userId by UsersAndCollectionsTable.userId
    val collection by CollectionEntity referencedOn UsersAndCollectionsTable.collectionId
    val collectionId by UsersAndCollectionsTable.collectionId
    var canCreate by UsersAndCollectionsTable.canCreate
    var canUpdate by UsersAndCollectionsTable.canUpdate
    var canDelete by UsersAndCollectionsTable.canDelete
    var createdAt by UsersAndCollectionsTable.createdAt
    var updatedAt by UsersAndCollectionsTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(UsersAndCollectionsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
