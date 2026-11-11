package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMProxyWorkerHeartbeatTable : IdTable<String>("\"LiteLLM_ProxyWorkerHeartbeat\"") {
    override val id = text("worker_id").entityId()
    val hostname = text("hostname")
    val startedAt = pgTimestamp("started_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastHeartbeatAt =
        pgTimestamp("last_heartbeat_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_ProxyWorkerHeartbeat_pkey\"")
}
