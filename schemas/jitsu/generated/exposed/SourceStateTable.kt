package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object SourceStateTable : CompositeIdTable("source_state") {
    val syncId = text("sync_id").entityId()
    val stream = text("stream").entityId()
    val state = jsonb<String>("state", { it }, { it })
    val timestamp = pgTimestamptz("timestamp", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(syncId, stream, name = "source_state_pkey")
}
