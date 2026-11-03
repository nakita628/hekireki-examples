package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object SessionTable : IdTable<String>("\"Session\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val friendlyId = text("friendlyId").uniqueIndex("\"Session_friendlyId_key\"")
    val externalId = text("externalId").nullable()
    val type = text("type")
    val projectId = text("projectId")
    val runtimeEnvironmentId = text("runtimeEnvironmentId")
    val environmentType =
        pgEnum("environmentType", "\"RuntimeEnvironmentType\"", RuntimeEnvironmentType.entries, RuntimeEnvironmentType::dbName)
    val organizationId = text("organizationId")
    val taskIdentifier = text("taskIdentifier")
    val triggerConfig = jsonb<String>("triggerConfig", { it }, { it })
    val isTest = bool("isTest").default(false)
    val tags = array("tags", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val currentRunId = text("currentRunId").nullable()
    val currentRunVersion = integer("currentRunVersion").default(0)
    val closedAt = pgTimestamp("closedAt", 3).nullable()
    val closedReason = text("closedReason").nullable()
    val expiresAt = pgTimestamp("expiresAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val streamBasinName = text("streamBasinName").nullable()
    val chatSnapshotStoragePath = text("chatSnapshotStoragePath").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Session_pkey\"")

    init {
        index("\"Session_expiresAt_idx\"", false, expiresAt)
        index("\"Session_currentRunId_idx\"", false, currentRunId)
        uniqueIndex("\"Session_runtimeEnvironmentId_externalId_key\"", runtimeEnvironmentId, externalId)
    }
}
