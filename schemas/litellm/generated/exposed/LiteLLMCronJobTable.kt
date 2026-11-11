package litellm.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMCronJobTable : IdTable<String>("\"LiteLLM_CronJob\"") {
    override val id = text("cronjob_id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val podId = text("pod_id")
    val status = pgEnum("status", "\"JobStatus\"", JobStatus.entries, JobStatus::dbName).default(JobStatus.INACTIVE)
    val lastUpdated = pgTimestamp("last_updated", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val ttl = pgTimestamp("ttl", 3)
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_CronJob_pkey\"")
}
