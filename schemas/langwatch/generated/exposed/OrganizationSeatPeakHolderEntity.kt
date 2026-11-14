package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class OrganizationSeatPeakHolderEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<OrganizationSeatPeakHolderEntity>(OrganizationSeatPeakHolderTable)

    val organizationId by OrganizationSeatPeakHolderTable.organizationId
    val holderKind by OrganizationSeatPeakHolderTable.holderKind
    val holderId by OrganizationSeatPeakHolderTable.holderId
    var seat by OrganizationSeatPeakHolderTable.seat
    var lastChangedAt by OrganizationSeatPeakHolderTable.lastChangedAt
    var lastChangeId by OrganizationSeatPeakHolderTable.lastChangeId
    var expiresAt by OrganizationSeatPeakHolderTable.expiresAt
    var expiredByFold by OrganizationSeatPeakHolderTable.expiredByFold
    var suppressedBy by OrganizationSeatPeakHolderTable.suppressedBy
    var restoredByFold by OrganizationSeatPeakHolderTable.restoredByFold
}
