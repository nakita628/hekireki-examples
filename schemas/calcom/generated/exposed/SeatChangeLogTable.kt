package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object SeatChangeLogTable : IdTable<String>("\"SeatChangeLog\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"SeatChangeLog_teamId_fkey\"",
        )
    val changeType = pgEnum("changeType", "\"SeatChangeType\"", SeatChangeType.entries, SeatChangeType::dbName)
    val seatCount = integer("seatCount")
    val userId = integer("userId").nullable()
    val triggeredBy = integer("triggeredBy").nullable()
    val changeDate = pgTimestamp("changeDate", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val monthKey = text("monthKey")
    val operationId = text("operationId").nullable()
    val processedInProrationId =
        optReference(
            "processedInProrationId",
            MonthlyProrationTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"SeatChangeLog_processedInProrationId_fkey\"",
        )
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val teamBillingId =
        optReference(
            "teamBillingId",
            TeamBillingTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"SeatChangeLog_teamBillingId_fkey\"",
        )
    val organizationBillingId =
        optReference(
            "organizationBillingId",
            OrganizationBillingTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"SeatChangeLog_organizationBillingId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"SeatChangeLog_pkey\"")

    init {
        index("\"SeatChangeLog_teamId_monthKey_idx\"", false, teamId, monthKey)
        index("\"SeatChangeLog_teamId_processedInProrationId_idx\"", false, teamId, processedInProrationId)
        index("\"SeatChangeLog_monthKey_idx\"", false, monthKey)
        uniqueIndex("\"SeatChangeLog_teamId_operationId_key\"", teamId, operationId)
    }
}
