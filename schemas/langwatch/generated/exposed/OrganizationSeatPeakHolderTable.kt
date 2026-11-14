package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object OrganizationSeatPeakHolderTable : CompositeIdTable("\"OrganizationSeatPeakHolder\"") {
    val organizationId = text("organizationId").entityId()
    val holderKind = text("holderKind").entityId()
    val holderId = text("holderId").entityId()
    val seat = text("seat")
    val lastChangedAt = double("lastChangedAt")
    val lastChangeId = text("lastChangeId").nullable()
    val expiresAt = double("expiresAt").nullable()
    val expiredByFold = bool("expiredByFold").default(false)
    val suppressedBy = text("suppressedBy").nullable()
    val restoredByFold = bool("restoredByFold").default(false)
    override val primaryKey = PrimaryKey(organizationId, holderKind, holderId, name = "\"OrganizationSeatPeakHolder_pkey\"")

    init {
        index("OrganizationSeatPeakHolder_organizationId_holderKind_seat_e_idx", false, organizationId, holderKind, seat, expiresAt)
        index("\"OrganizationSeatPeakHolder_organizationId_suppressedBy_idx\"", false, organizationId, suppressedBy)
    }
}
