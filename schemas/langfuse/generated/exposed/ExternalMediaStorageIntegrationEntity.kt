package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ExternalMediaStorageIntegrationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ExternalMediaStorageIntegrationEntity>(ExternalMediaStorageIntegrationTable)

    val project by ProjectEntity referencedOn ExternalMediaStorageIntegrationTable.id
    var type by ExternalMediaStorageIntegrationTable.type
    var bucketName by ExternalMediaStorageIntegrationTable.bucketName
    var prefix by ExternalMediaStorageIntegrationTable.prefix
    var authType by ExternalMediaStorageIntegrationTable.authType
    var accessKeyId by ExternalMediaStorageIntegrationTable.accessKeyId
    var secretAccessKey by ExternalMediaStorageIntegrationTable.secretAccessKey
    var roleArn by ExternalMediaStorageIntegrationTable.roleArn
    var externalId by ExternalMediaStorageIntegrationTable.externalId
    var region by ExternalMediaStorageIntegrationTable.region
    var endpoint by ExternalMediaStorageIntegrationTable.endpoint
    var forcePathStyle by ExternalMediaStorageIntegrationTable.forcePathStyle
    var createdAt by ExternalMediaStorageIntegrationTable.createdAt
    var updatedAt by ExternalMediaStorageIntegrationTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ExternalMediaStorageIntegrationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
