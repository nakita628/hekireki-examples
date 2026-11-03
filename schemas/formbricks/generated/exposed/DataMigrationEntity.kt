package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class DataMigrationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DataMigrationEntity>(DataMigrationTable)

    var startedAt by DataMigrationTable.startedAt
    var finishedAt by DataMigrationTable.finishedAt
    var name by DataMigrationTable.name
    var status by DataMigrationTable.status
}
