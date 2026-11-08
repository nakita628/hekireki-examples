package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class BlobStorageIntegrationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, BlobStorageIntegrationEntity>(BlobStorageIntegrationTable)

    val project by ProjectEntity referencedOn BlobStorageIntegrationTable.id
    var type by BlobStorageIntegrationTable.type
    var bucketName by BlobStorageIntegrationTable.bucketName
    var prefix by BlobStorageIntegrationTable.prefix
    var authType by BlobStorageIntegrationTable.authType
    var accessKeyId by BlobStorageIntegrationTable.accessKeyId
    var secretAccessKey by BlobStorageIntegrationTable.secretAccessKey
    var roleArn by BlobStorageIntegrationTable.roleArn
    var externalId by BlobStorageIntegrationTable.externalId
    var region by BlobStorageIntegrationTable.region
    var endpoint by BlobStorageIntegrationTable.endpoint
    var forcePathStyle by BlobStorageIntegrationTable.forcePathStyle
    var nextSyncAt by BlobStorageIntegrationTable.nextSyncAt
    var lastSyncAt by BlobStorageIntegrationTable.lastSyncAt
    var enabled by BlobStorageIntegrationTable.enabled
    var exportFrequency by BlobStorageIntegrationTable.exportFrequency
    var fileType by BlobStorageIntegrationTable.fileType
    var exportMode by BlobStorageIntegrationTable.exportMode
    var exportStartDate by BlobStorageIntegrationTable.exportStartDate
    var exportSource by BlobStorageIntegrationTable.exportSource
    var exportFieldGroups by BlobStorageIntegrationTable.exportFieldGroups
    var compressed by BlobStorageIntegrationTable.compressed
    var exportTuning by BlobStorageIntegrationTable.exportTuning
    var runStartedAt by BlobStorageIntegrationTable.runStartedAt
    var backfill by BlobStorageIntegrationTable.backfill
    var lastError by BlobStorageIntegrationTable.lastError
    var lastErrorAt by BlobStorageIntegrationTable.lastErrorAt
    var lastFailureNotificationSentAt by BlobStorageIntegrationTable.lastFailureNotificationSentAt
    var createdAt by BlobStorageIntegrationTable.createdAt
    var updatedAt by BlobStorageIntegrationTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(BlobStorageIntegrationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
