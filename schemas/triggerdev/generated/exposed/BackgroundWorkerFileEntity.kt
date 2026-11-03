package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class BackgroundWorkerFileEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, BackgroundWorkerFileEntity>(BackgroundWorkerFileTable)

    var friendlyId by BackgroundWorkerFileTable.friendlyId
    var filePath by BackgroundWorkerFileTable.filePath
    var contentHash by BackgroundWorkerFileTable.contentHash
    var contents by BackgroundWorkerFileTable.contents
    var project by ProjectEntity referencedOn BackgroundWorkerFileTable.projectId
    var projectId by BackgroundWorkerFileTable.projectId
    var backgroundWorkers by BackgroundWorkerEntity via BackgroundWorkerToBackgroundWorkerFileTable
    val tasks by BackgroundWorkerTaskEntity optionalReferrersOn BackgroundWorkerTaskTable.fileId
    var createdAt by BackgroundWorkerFileTable.createdAt
}
