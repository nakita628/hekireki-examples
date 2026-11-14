package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class StoredObjectEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<StoredObjectEntity>(StoredObjectTable)

    val tenantId by StoredObjectTable.tenantId
    val id1 by StoredObjectTable.id1
    var status by StoredObjectTable.status
    var purpose by StoredObjectTable.purpose
    var ownerKind by StoredObjectTable.ownerKind
    var ownerId by StoredObjectTable.ownerId
    var filename by StoredObjectTable.filename
    var sha256 by StoredObjectTable.sha256
    var sizeBytes by StoredObjectTable.sizeBytes
    var mediaType by StoredObjectTable.mediaType
    var mediaTypeVerified by StoredObjectTable.mediaTypeVerified
    var storageProvider by StoredObjectTable.storageProvider
    var storageDestinationId by StoredObjectTable.storageDestinationId
    var storageProviderRelativeId by StoredObjectTable.storageProviderRelativeId
    var generation by StoredObjectTable.generation
    var audiences by StoredObjectTable.audiences
    var expiresAt by StoredObjectTable.expiresAt
    var availableAt by StoredObjectTable.availableAt
    var deletedAt by StoredObjectTable.deletedAt
    var source by StoredObjectTable.source1
    var legacyFingerprint by StoredObjectTable.legacyFingerprint
    var createdAt by StoredObjectTable.createdAt
    var updatedAt by StoredObjectTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(StoredObjectTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
