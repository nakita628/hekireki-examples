package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ClippingClipEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ClippingClipEntity>(ClippingClipTable)

    var clippingId by ClippingClipTable.clippingId
    var title by ClippingClipTable.title
    var content by ClippingClipTable.content
    var start by ClippingClipTable.start
    var end by ClippingClipTable.end
    var trimStart by ClippingClipTable.trimStart
    var status by ClippingClipTable.status
    var error by ClippingClipTable.error
    var mediaId by ClippingClipTable.mediaId
    var path by ClippingClipTable.path
    var thumbnail by ClippingClipTable.thumbnail
    var draftedAt by ClippingClipTable.draftedAt
    var createdAt by ClippingClipTable.createdAt
    var updatedAt by ClippingClipTable.updatedAt
    var clipping by ClippingEntity referencedOn ClippingClipTable.clippingId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ClippingClipTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
