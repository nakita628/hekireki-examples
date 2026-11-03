package linkwarden.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class AppMigrationEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, AppMigrationEntity>(AppMigrationTable)

    var name by AppMigrationTable.name
    var status by AppMigrationTable.status
    var finishedAt by AppMigrationTable.finishedAt
    var createdAt by AppMigrationTable.createdAt
}
