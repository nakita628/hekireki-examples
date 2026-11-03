package documenso.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object BackgroundJobTaskTable : IdTable<String>("\"BackgroundJobTask\"") {
    override val id = text("id").entityId()
    val name = text("name")
    val status =
        pgEnum("status", "\"BackgroundJobTaskStatus\"", BackgroundJobTaskStatus.entries, BackgroundJobTaskStatus::dbName).default(
            BackgroundJobTaskStatus.PENDING,
        )
    val result = jsonb<String>("result", { it }, { it }).nullable()
    val retried = integer("retried").default(0)
    val maxRetries = integer("maxRetries").default(3)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val completedAt = pgTimestamp("completedAt", 3).nullable()
    val jobId =
        reference(
            "jobId",
            BackgroundJobTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BackgroundJobTask_jobId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"BackgroundJobTask_pkey\"")
}
