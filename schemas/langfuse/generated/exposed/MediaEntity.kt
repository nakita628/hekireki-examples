package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class MediaEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<MediaEntity>(MediaTable)

    val id1 by MediaTable.id1
    var sha256Hash by MediaTable.sha256Hash
    val projectId by MediaTable.projectId
    val project by ProjectEntity referencedOn MediaTable.projectId
    var createdAt by MediaTable.createdAt
    var updatedAt by MediaTable.updatedAt
    var uploadedAt by MediaTable.uploadedAt
    var uploadHttpStatus by MediaTable.uploadHttpStatus
    var uploadHttpError by MediaTable.uploadHttpError
    var bucketPath by MediaTable.bucketPath
    var bucketName by MediaTable.bucketName
    var contentType by MediaTable.contentType
    var contentLength by MediaTable.contentLength

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(MediaTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
