package documenso.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object BackgroundJobTable : IdTable<String>("\"BackgroundJob\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val status =
        pgEnum("status", "\"BackgroundJobStatus\"", BackgroundJobStatus.entries, BackgroundJobStatus::dbName).default(
            BackgroundJobStatus.PENDING,
        )
    val payload = jsonb<String>("payload", { it }, { it }).nullable()
    val retried = integer("retried").default(0)
    val maxRetries = integer("maxRetries").default(3)
    val jobId = text("jobId")
    val name = text("name")
    val version = text("version")
    val submittedAt = pgTimestamp("submittedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val completedAt = pgTimestamp("completedAt", 3).nullable()
    val lastRetriedAt = pgTimestamp("lastRetriedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"BackgroundJob_pkey\"")
}
