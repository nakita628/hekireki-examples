package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object CronJobsTable : IdTable<String>("cron_jobs") {
    override val id = text("name").entityId()
    val lastRun = pgTimestamp("last_run", 3).nullable()
    val jobStartedAt = pgTimestamp("job_started_at", 3).nullable()
    val state = text("state").nullable()
    override val primaryKey = PrimaryKey(id, name = "cron_jobs_pkey")
}
