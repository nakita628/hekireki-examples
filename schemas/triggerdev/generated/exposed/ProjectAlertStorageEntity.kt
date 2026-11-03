package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ProjectAlertStorageEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ProjectAlertStorageEntity>(ProjectAlertStorageTable)

    var project by ProjectEntity referencedOn ProjectAlertStorageTable.projectId
    var projectId by ProjectAlertStorageTable.projectId
    var alertChannel by ProjectAlertChannelEntity referencedOn ProjectAlertStorageTable.alertChannelId
    var alertChannelId by ProjectAlertStorageTable.alertChannelId
    var alertType by ProjectAlertStorageTable.alertType
    var storageId by ProjectAlertStorageTable.storageId
    var storageData by ProjectAlertStorageTable.storageData
    var createdAt by ProjectAlertStorageTable.createdAt
    var updatedAt by ProjectAlertStorageTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ProjectAlertStorageTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
