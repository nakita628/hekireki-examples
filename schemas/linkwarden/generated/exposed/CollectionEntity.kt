package linkwarden.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CollectionEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, CollectionEntity>(CollectionTable)

    var name by CollectionTable.name
    var description by CollectionTable.description
    var icon by CollectionTable.icon
    var iconWeight by CollectionTable.iconWeight
    var color by CollectionTable.color
    var parentId by CollectionTable.parentId
    var parent by CollectionEntity optionalReferencedOn CollectionTable.parentId
    val subCollections by CollectionEntity optionalReferrersOn CollectionTable.parentId
    var isPublic by CollectionTable.isPublic
    var owner by UserEntity referencedOn CollectionTable.ownerId
    var ownerId by CollectionTable.ownerId
    val members by UsersAndCollectionsEntity referrersOn UsersAndCollectionsTable.collectionId
    var createdBy by UserEntity optionalReferencedOn CollectionTable.createdById
    var createdById by CollectionTable.createdById
    val links by LinkEntity referrersOn LinkTable.collectionId
    val rssSubscriptions by RssSubscriptionEntity referrersOn RssSubscriptionTable.collectionId
    var createdAt by CollectionTable.createdAt
    var updatedAt by CollectionTable.updatedAt
    val dashboardSection by DashboardSectionEntity optionalReferrersOn DashboardSectionTable.collectionId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CollectionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
