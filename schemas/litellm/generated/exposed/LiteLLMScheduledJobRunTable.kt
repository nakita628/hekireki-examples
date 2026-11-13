package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMScheduledJobRunTable : IdTable<String>("\"LiteLLM_ScheduledJobRun\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val jobName = text("job_name")
    val lockGroup = text("lock_group")
    val schedule = text("schedule")
    val scheduledFor = pgTimestamptz("scheduled_for", 3)
    val status =
        pgEnum("status", "\"ScheduledJobRunStatus\"", ScheduledJobRunStatus.entries, ScheduledJobRunStatus::dbName).default(
            ScheduledJobRunStatus.PENDING,
        )
    val claimedBy = text("claimed_by").nullable()
    val claimedAt = pgTimestamptz("claimed_at", 3).nullable()
    val leaseExpiresAt = pgTimestamptz("lease_expires_at", 3).nullable()
    val finishedAt = pgTimestamptz("finished_at", 3).nullable()
    val createdAt = pgTimestamptz("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_ScheduledJobRun_pkey\"")

    init {
        index("\"LiteLLM_ScheduledJobRun_job_name_status_idx\"", false, jobName, status)
        index("\"LiteLLM_ScheduledJobRun_lock_group_status_idx\"", false, lockGroup, status)
    }
}
