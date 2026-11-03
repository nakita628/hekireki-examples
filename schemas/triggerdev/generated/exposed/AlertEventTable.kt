package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object AlertEventTable : CompositeIdTable("\"AlertEvent\"") {
    val id1 = text("id").entityId()
    val alertId = text("alertId")
    val type = text("type")
    val at = pgTimestamptz("at", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }.entityId()
    val facts = jsonb<String>("facts", { it }, { it }).default("{}")
    val actorId = text("actorId").nullable()
    val sourceEventId = text("sourceEventId").nullable()
    override val primaryKey = PrimaryKey(id1, at, name = "\"AlertEvent_pkey\"")

    init {
        index("\"AlertEvent_alertId_at_idx\"", false, functions = listOf(indexExpression("\"alertId\""), indexExpression("\"at\" DESC")))
    }
}
