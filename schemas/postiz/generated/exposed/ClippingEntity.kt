package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ClippingEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ClippingEntity>(ClippingTable)

    var organizationId by ClippingTable.organizationId
    var url by ClippingTable.url
    var status by ClippingTable.status
    var error by ClippingTable.error
    var title by ClippingTable.title
    var thumbnail by ClippingTable.thumbnail
    var duration by ClippingTable.duration
    var maxClips by ClippingTable.maxClips
    var fit by ClippingTable.fit
    var integrations by ClippingTable.integrations
    var creditsId by ClippingTable.creditsId
    var deletedAt by ClippingTable.deletedAt
    var createdAt by ClippingTable.createdAt
    var updatedAt by ClippingTable.updatedAt
    var organization by OrganizationEntity referencedOn ClippingTable.organizationId
    val clips by ClippingClipEntity referrersOn ClippingClipTable.clippingId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ClippingTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
