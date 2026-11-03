package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class BackgroundMigrationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, BackgroundMigrationEntity>(BackgroundMigrationTable)

    var name by BackgroundMigrationTable.name
    var script by BackgroundMigrationTable.script
    var args by BackgroundMigrationTable.args
    var state by BackgroundMigrationTable.state
    var finishedAt by BackgroundMigrationTable.finishedAt
    var failedAt by BackgroundMigrationTable.failedAt
    var failedReason by BackgroundMigrationTable.failedReason
    var workerId by BackgroundMigrationTable.workerId
    var lockedAt by BackgroundMigrationTable.lockedAt
}
