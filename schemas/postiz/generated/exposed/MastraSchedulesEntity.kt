package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraSchedulesEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraSchedulesEntity>(MastraSchedulesTable)

    var target by MastraSchedulesTable.target
    var cron by MastraSchedulesTable.cron
    var timezone by MastraSchedulesTable.timezone
    var status by MastraSchedulesTable.status
    var nextFireAt by MastraSchedulesTable.nextFireAt
    var lastFireAt by MastraSchedulesTable.lastFireAt
    var lastRunId by MastraSchedulesTable.lastRunId
    var createdAt by MastraSchedulesTable.createdAt
    var updatedAt by MastraSchedulesTable.updatedAt
    var metadata by MastraSchedulesTable.metadata
    var ownerType by MastraSchedulesTable.ownerType
    var ownerId by MastraSchedulesTable.ownerId
}
