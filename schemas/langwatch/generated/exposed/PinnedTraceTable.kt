package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object PinnedTraceTable : IdTable<String>("\"PinnedTrace\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val projectId = text("projectId")
    val traceId = text("traceId")
    val userId = text("userId").nullable()
    val source1 = pgEnum("source", "\"PinSource\"", PinSource.entries, PinSource::dbName).default(PinSource.MANUAL)
    val reason = text("reason").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"PinnedTrace_pkey\"")

    init {
        index("\"PinnedTrace_projectId_idx\"", false, projectId)
        uniqueIndex("\"PinnedTrace_projectId_traceId_key\"", projectId, traceId)
    }
}
