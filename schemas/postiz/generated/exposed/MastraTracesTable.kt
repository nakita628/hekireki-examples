package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraTracesTable : IdTable<String>("mastra_traces") {
    override val id = text("id").entityId()
    val parentSpanId = text("parentSpanId").nullable()
    val name = text("name")
    val traceId = text("traceId")
    val scope = text("scope")
    val kind = integer("kind")
    val attributes = jsonb<String>("attributes", { it }, { it }).nullable()
    val status = jsonb<String>("status", { it }, { it }).nullable()
    val events = jsonb<String>("events", { it }, { it }).nullable()
    val links = jsonb<String>("links", { it }, { it }).nullable()
    val other = text("other").nullable()
    val startTime = long("startTime")
    val endTime = long("endTime")
    val createdAt = pgTimestamp("createdAt", 6)
    val createdAtZ =
        pgTimestamptz("createdAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "mastra_traces_pkey")

    init {
        index(
            "public_mastra_traces_name_starttime_idx",
            false,
            functions = listOf(indexExpression("\"name\""), indexExpression("\"startTime\" DESC")),
        )
    }
}
