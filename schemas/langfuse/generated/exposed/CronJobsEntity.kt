package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class CronJobsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CronJobsEntity>(CronJobsTable)

    var lastRun by CronJobsTable.lastRun
    var jobStartedAt by CronJobsTable.jobStartedAt
    var state by CronJobsTable.state
}
