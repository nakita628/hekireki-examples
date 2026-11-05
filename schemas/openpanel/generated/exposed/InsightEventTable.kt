package openpanel.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object InsightEventTable : IdTable<UUID>("insight_events") {
    override val id = javaUUID("id").databaseDefault("gen_random_uuid()").entityId()
    val insightId =
        reference(
            "insightId",
            ProjectInsightTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"insight_events_insightId_fkey\"",
        )
    val eventKind = text("eventKind")
    val changeFrom = jsonb<String>("changeFrom", { it }, { it }).nullable()
    val changeTo = jsonb<String>("changeTo", { it }, { it }).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "insight_events_pkey")

    init {
        index("\"insight_events_insightId_createdAt_idx\"", false, insightId, createdAt)
    }
}
