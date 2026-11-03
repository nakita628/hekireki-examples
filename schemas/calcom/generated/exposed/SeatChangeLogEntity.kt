package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class SeatChangeLogEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SeatChangeLogEntity>(SeatChangeLogTable)

    var teamId by SeatChangeLogTable.teamId
    var team by TeamEntity referencedOn SeatChangeLogTable.teamId
    var changeType by SeatChangeLogTable.changeType
    var seatCount by SeatChangeLogTable.seatCount
    var userId by SeatChangeLogTable.userId
    var triggeredBy by SeatChangeLogTable.triggeredBy
    var changeDate by SeatChangeLogTable.changeDate
    var monthKey by SeatChangeLogTable.monthKey
    var operationId by SeatChangeLogTable.operationId
    var processedInProrationId by SeatChangeLogTable.processedInProrationId
    var proration by MonthlyProrationEntity optionalReferencedOn SeatChangeLogTable.processedInProrationId
    var metadata by SeatChangeLogTable.metadata
    var createdAt by SeatChangeLogTable.createdAt
    var teamBillingId by SeatChangeLogTable.teamBillingId
    var teamBilling by TeamBillingEntity optionalReferencedOn SeatChangeLogTable.teamBillingId
    var organizationBillingId by SeatChangeLogTable.organizationBillingId
    var organizationBilling by OrganizationBillingEntity optionalReferencedOn SeatChangeLogTable.organizationBillingId
}
