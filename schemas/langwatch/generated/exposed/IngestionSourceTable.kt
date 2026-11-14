package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object IngestionSourceTable : IdTable<String>("\"IngestionSource\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId")
    val teamId = text("teamId").nullable()
    val sourceType = text("sourceType")
    val name = text("name")
    val description = text("description").nullable()
    val ingestSecretHash = text("ingestSecretHash")
    val parserConfig = jsonb<String>("parserConfig", { it }, { it }).default("{}")
    val pollerCursor = jsonb<String>("pollerCursor", { it }, { it }).nullable()
    val errorCount = integer("errorCount").default(0)
    val pullSchedule = text("pullSchedule").nullable()
    val status = text("status").default("awaiting_first_event")
    val traceProjectId = text("traceProjectId").nullable()
    val providerAccountId = text("providerAccountId").nullable()
    val lastEventAt = pgTimestamp("lastEventAt", 3).nullable()
    val lastSuccessAt = pgTimestamp("lastSuccessAt", 3).nullable()
    val lastReadThroughAt = pgTimestamp("lastReadThroughAt", 3).nullable()
    val lastRunCompleteness = text("lastRunCompleteness").nullable()
    val unpricedUsageSince = pgTimestamp("unpricedUsageSince", 3).nullable()
    val unpricedUsageThrough = pgTimestamp("unpricedUsageThrough", 3).nullable()
    val archivedAt = pgTimestamp("archivedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdById = text("createdById").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"IngestionSource_pkey\"")

    init {
        index("\"IngestionSource_organizationId_idx\"", false, organizationId)
        index("\"IngestionSource_teamId_idx\"", false, teamId)
        index("\"IngestionSource_sourceType_idx\"", false, sourceType)
        uniqueIndex("\"IngestionSource_organizationId_name_key\"", organizationId, name)
    }
}
