package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraDatasetVersionsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraDatasetVersionsEntity>(MastraDatasetVersionsTable)

    var datasetId by MastraDatasetVersionsTable.datasetId
    var version by MastraDatasetVersionsTable.version
    var createdAt by MastraDatasetVersionsTable.createdAt
    var createdAtZ by MastraDatasetVersionsTable.createdAtZ
}
