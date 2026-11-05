package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object CleanupThreadTable : IdTable<String>("\"CleanupThread\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val threadId = text("threadId")
    val archived = bool("archived")
    val jobId =
        reference(
            "jobId",
            CleanupJobTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CleanupThread_jobId_fkey\"",
        )
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CleanupThread_emailAccountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"CleanupThread_pkey\"")

    init {
        index("\"CleanupThread_jobId_idx\"", false, jobId)
        index("\"CleanupThread_emailAccountId_idx\"", false, emailAccountId)
    }
}
