package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class DatasetVersionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DatasetVersionEntity>(DatasetVersionTable)

    var projectId by DatasetVersionTable.projectId
    var datasetId by DatasetVersionTable.datasetId
    var version by DatasetVersionTable.version
    var name by DatasetVersionTable.name
    var trigger by DatasetVersionTable.trigger
    var contentHash by DatasetVersionTable.contentHash
    var columnTypes by DatasetVersionTable.columnTypes
    var rowCount by DatasetVersionTable.rowCount
    var sizeBytes by DatasetVersionTable.sizeBytes
    var chunkCount by DatasetVersionTable.chunkCount
    var restoredFromVersion by DatasetVersionTable.restoredFromVersion
    var authorId by DatasetVersionTable.authorId
    var authorKind by DatasetVersionTable.authorKind
    var pinnedAt by DatasetVersionTable.pinnedAt
    var createdAt by DatasetVersionTable.createdAt
}
