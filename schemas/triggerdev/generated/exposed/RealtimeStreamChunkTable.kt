package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object RealtimeStreamChunkTable : IdTable<String>("\"RealtimeStreamChunk\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val key = text("key")
    val value = text("value")
    val sequence = integer("sequence")
    val runId = text("runId")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"RealtimeStreamChunk_pkey\"")

    init {
        index("\"RealtimeStreamChunk_runId_idx\"", false, runId)
        index("\"RealtimeStreamChunk_createdAt_idx\"", false, createdAt)
    }
}
