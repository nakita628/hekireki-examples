package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class NurturingOrganizationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, NurturingOrganizationEntity>(NurturingOrganizationTable)

    var adminUserId by NurturingOrganizationTable.adminUserId
    var seeded by NurturingOrganizationTable.seeded
    var evaluationCount by NurturingOrganizationTable.evaluationCount
    var simulationRunCount by NurturingOrganizationTable.simulationRunCount
    var codingFirstSessionAt by NurturingOrganizationTable.codingFirstSessionAt
    var codingAgentTypes by NurturingOrganizationTable.codingAgentTypes
    var codingActiveDays by NurturingOrganizationTable.codingActiveDays
    var codingLastActiveDay by NurturingOrganizationTable.codingLastActiveDay
    var codingTerminalTypes by NurturingOrganizationTable.codingTerminalTypes
    var codingRepositoryKeys by NurturingOrganizationTable.codingRepositoryKeys
    var codingFirstDays by NurturingOrganizationTable.codingFirstDays
    var lastTraceAt by NurturingOrganizationTable.lastTraceAt
    var updatedAt by NurturingOrganizationTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(NurturingOrganizationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
