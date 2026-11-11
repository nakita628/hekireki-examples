package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import org.jetbrains.exposed.v1.json.jsonb

object LiteLLMLensTraceSignalTable : CompositeIdTable("\"LiteLLM_LensTraceSignal\"") {
    val traceId = text("trace_id").entityId()
    val traceRef = text("trace_ref").default("").entityId()
    val configKey = text("config_key")
    val spanCount = integer("span_count")
    val claimedUntil = pgTimestamp("claimed_until", 3).nullable()
    val classifiedAt = pgTimestamp("classified_at", 3).nullable()
    val data = jsonb<String>("data", { it }, { it })
    override val primaryKey = PrimaryKey(traceId, traceRef, name = "\"LiteLLM_LensTraceSignal_pkey\"")
}
