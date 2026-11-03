package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SessionRunTable : IdTable<String>("\"SessionRun\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val sessionId =
        reference(
            "sessionId",
            SessionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"SessionRun_sessionId_fkey\"",
        )
    val runId = text("runId").uniqueIndex("\"SessionRun_runId_key\"")
    val reason = text("reason")
    val triggeredAt = pgTimestamp("triggeredAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"SessionRun_pkey\"")

    init {
        index("\"SessionRun_sessionId_idx\"", false, sessionId)
        index("\"SessionRun_sessionId_triggeredAt_id_idx\"", false, sessionId, triggeredAt, id)
    }
}
