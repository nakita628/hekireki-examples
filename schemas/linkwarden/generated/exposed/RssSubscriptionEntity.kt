package linkwarden.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class RssSubscriptionEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, RssSubscriptionEntity>(RssSubscriptionTable)

    var url by RssSubscriptionTable.url
    var name by RssSubscriptionTable.name
    var lastBuildDate by RssSubscriptionTable.lastBuildDate
    var collection by CollectionEntity referencedOn RssSubscriptionTable.collectionId
    var collectionId by RssSubscriptionTable.collectionId
    var ownerId by RssSubscriptionTable.ownerId
    var createdAt by RssSubscriptionTable.createdAt
    var updatedAt by RssSubscriptionTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(RssSubscriptionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
