package teable.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object TableQueryObservationShardTable : CompositeIdTable("table_query_observation_shard") {
    val spaceId = text("space_id").nullable()
    val baseId = text("base_id")
    val tableId = text("table_id").entityId()
    val queryKind = text("query_kind").entityId()
    val shapeHash = text("shape_hash").entityId()
    val windowStart = pgTimestamptz("window_start", 6).entityId()
    val writerId = text("writer_id").entityId()
    val windowSizeSeconds = integer("window_size_seconds")
    val requestCount = integer("request_count")
    val slowCount = integer("slow_count")
    val timeoutCount = integer("timeout_count")
    val dbErrorCount = integer("db_error_count")
    val totalDurationMs = double("total_duration_ms")
    val maxDurationMs = double("max_duration_ms")
    val totalDbDurationMs = double("total_db_duration_ms").nullable()
    val maxDbDurationMs = double("max_db_duration_ms").nullable()
    val shape = jsonb<String>("shape", { it }, { it })
    val sqlDiagnostics = jsonb<String>("sql_diagnostics", { it }, { it }).nullable()
    val createdTime = pgTimestamptz("created_time", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastModifiedTime = pgTimestamptz("last_modified_time", 6).nullable()
    override val primaryKey = PrimaryKey(tableId, queryKind, shapeHash, windowStart, writerId, name = "table_query_observation_shard_pkey")

    init {
        index("table_query_observation_shard_window_start_idx", false, windowStart)
        index("table_query_observation_shard_table_start_idx", false, tableId, windowStart)
        index("table_query_observation_shard_search_activity_idx", false, queryKind, tableId, windowStart)
        index("table_query_observation_shard_base_start_idx", false, baseId, windowStart)
    }
}
