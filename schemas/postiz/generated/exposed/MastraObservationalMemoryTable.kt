package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraObservationalMemoryTable : IdTable<String>("mastra_observational_memory") {
    override val id = text("id").entityId()
    val lookupKey = text("lookupKey")
    val scope = text("scope")
    val resourceId = text("resourceId").nullable()
    val threadId = text("threadId").nullable()
    val activeObservations = text("activeObservations")
    val activeObservationsPendingUpdate = text("activeObservationsPendingUpdate").nullable()
    val originType = text("originType")
    val config = text("config")
    val generationCount = integer("generationCount")
    val lastObservedAt = pgTimestamp("lastObservedAt", 6).nullable()
    val lastReflectionAt = pgTimestamp("lastReflectionAt", 6).nullable()
    val pendingMessageTokens = integer("pendingMessageTokens")
    val totalTokensObserved = integer("totalTokensObserved")
    val observationTokenCount = integer("observationTokenCount")
    val isObserving = bool("isObserving")
    val isReflecting = bool("isReflecting")
    val observedMessageIds = jsonb<String>("observedMessageIds", { it }, { it }).nullable()
    val observedTimezone = text("observedTimezone").nullable()
    val bufferedObservations = text("bufferedObservations").nullable()
    val bufferedObservationTokens = integer("bufferedObservationTokens").nullable()
    val bufferedMessageIds = jsonb<String>("bufferedMessageIds", { it }, { it }).nullable()
    val bufferedReflection = text("bufferedReflection").nullable()
    val bufferedReflectionTokens = integer("bufferedReflectionTokens").nullable()
    val bufferedReflectionInputTokens = integer("bufferedReflectionInputTokens").nullable()
    val reflectedObservationLineCount = integer("reflectedObservationLineCount").nullable()
    val bufferedObservationChunks = jsonb<String>("bufferedObservationChunks", { it }, { it }).nullable()
    val isBufferingObservation = bool("isBufferingObservation")
    val isBufferingReflection = bool("isBufferingReflection")
    val lastBufferedAtTokens = integer("lastBufferedAtTokens")
    val lastBufferedAtTime = pgTimestamp("lastBufferedAtTime", 6).nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val createdAt = pgTimestamp("createdAt", 6)
    val updatedAt = pgTimestamp("updatedAt", 6)
    val lastObservedAtZ =
        pgTimestamptz("lastObservedAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastReflectionAtZ =
        pgTimestamptz("lastReflectionAtZ", 6)
            .nullable()
            .databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastBufferedAtTimeZ =
        pgTimestamptz("lastBufferedAtTimeZ", 6)
            .nullable()
            .databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdAtZ =
        pgTimestamptz("createdAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAtZ =
        pgTimestamptz("updatedAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "mastra_observational_memory_pkey")

    init {
        index("idx_om_lookup_key", false, lookupKey)
    }
}
