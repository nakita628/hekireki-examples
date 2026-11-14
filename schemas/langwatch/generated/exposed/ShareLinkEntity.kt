package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ShareLinkEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ShareLinkEntity>(ShareLinkTable)

    var token by ShareLinkTable.token
    var resourceType by ShareLinkTable.resourceType
    var resourceId by ShareLinkTable.resourceId
    var threadId by ShareLinkTable.threadId
    var projectId by ShareLinkTable.projectId
    var userId by ShareLinkTable.userId
    var visibility by ShareLinkTable.visibility
    var expiresAt by ShareLinkTable.expiresAt
    var maxViews by ShareLinkTable.maxViews
    var viewCount by ShareLinkTable.viewCount
    var createdAt by ShareLinkTable.createdAt
    var updatedAt by ShareLinkTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ShareLinkTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
