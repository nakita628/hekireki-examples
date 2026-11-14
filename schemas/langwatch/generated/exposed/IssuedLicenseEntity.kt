package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class IssuedLicenseEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, IssuedLicenseEntity>(IssuedLicenseTable)

    var licenseId by IssuedLicenseTable.licenseId
    var tokenHash by IssuedLicenseTable.tokenHash
    var organizationId by IssuedLicenseTable.organizationId
    var organizationName by IssuedLicenseTable.organizationName
    var email by IssuedLicenseTable.email
    var planType by IssuedLicenseTable.planType
    var maxMembers by IssuedLicenseTable.maxMembers
    var maxMembersLite by IssuedLicenseTable.maxMembersLite
    var liteSeatsUnlimited by IssuedLicenseTable.liteSeatsUnlimited
    var maxMembersDeveloper by IssuedLicenseTable.maxMembersDeveloper
    var issuedAt by IssuedLicenseTable.issuedAt
    var expiresAt by IssuedLicenseTable.expiresAt
    var source by IssuedLicenseTable.source1
    var issuedById by IssuedLicenseTable.issuedById
    var revokedAt by IssuedLicenseTable.revokedAt
    var revokedById by IssuedLicenseTable.revokedById
    var revokedReason by IssuedLicenseTable.revokedReason
    var supersededAt by IssuedLicenseTable.supersededAt
    var replacesId by IssuedLicenseTable.replacesId
    var pendingDeliveryLicense by IssuedLicenseTable.pendingDeliveryLicense
    var services by IssuedLicenseTable.services
    var seatRateCents by IssuedLicenseTable.seatRateCents
    var seatCurrency by IssuedLicenseTable.seatCurrency
    var commitUsdCents by IssuedLicenseTable.commitUsdCents
    var overageEnabled by IssuedLicenseTable.overageEnabled
    var overageMaxUsdCents by IssuedLicenseTable.overageMaxUsdCents
    var instanceId by IssuedLicenseTable.instanceId
    var instanceBoundAt by IssuedLicenseTable.instanceBoundAt
    var lastSyncAt by IssuedLicenseTable.lastSyncAt
    var lastSyncVersion by IssuedLicenseTable.lastSyncVersion
    var reportedMembers by IssuedLicenseTable.reportedMembers
    var reportedMembersLite by IssuedLicenseTable.reportedMembersLite
    var virtualKeyId by IssuedLicenseTable.virtualKeyId
    var seatsRaisedFrom by IssuedLicenseTable.seatsRaisedFrom
    var createdAt by IssuedLicenseTable.createdAt
    var updatedAt by IssuedLicenseTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(IssuedLicenseTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
