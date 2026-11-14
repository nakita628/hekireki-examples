package langwatch.models

import org.jetbrains.exposed.v1.core.IntegerColumnType
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object NurturingOrganizationTable : IdTable<String>("\"NurturingOrganization\"") {
    override val id = text("organizationId").entityId()
    val adminUserId = text("adminUserId").nullable()
    val seeded = bool("seeded").default(false)
    val evaluationCount = integer("evaluationCount").default(0)
    val simulationRunCount = integer("simulationRunCount").default(0)
    val codingFirstSessionAt = pgTimestamp("codingFirstSessionAt", 3).nullable()
    val codingAgentTypes = array("codingAgentTypes", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val codingActiveDays = integer("codingActiveDays").default(0)
    val codingLastActiveDay = text("codingLastActiveDay").nullable()
    val codingTerminalTypes = array("codingTerminalTypes", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val codingRepositoryKeys = array("codingRepositoryKeys", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val codingFirstDays = array("codingFirstDays", IntegerColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val lastTraceAt = pgTimestamp("lastTraceAt", 3).nullable()
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"NurturingOrganization_pkey\"")
}
