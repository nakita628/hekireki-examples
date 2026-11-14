package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ActivationCodeEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ActivationCodeEntity>(ActivationCodeTable)

    var codeHash by ActivationCodeTable.codeHash
    var codeHint by ActivationCodeTable.codeHint
    var organizationId by ActivationCodeTable.organizationId
    var organizationName by ActivationCodeTable.organizationName
    var email by ActivationCodeTable.email
    var planType by ActivationCodeTable.planType
    var maxMembers by ActivationCodeTable.maxMembers
    var maxMembersLite by ActivationCodeTable.maxMembersLite
    var liteSeatsUnlimited by ActivationCodeTable.liteSeatsUnlimited
    var maxMembersDeveloper by ActivationCodeTable.maxMembersDeveloper
    var licenseTermDays by ActivationCodeTable.licenseTermDays
    var services by ActivationCodeTable.services
    var expiresAt by ActivationCodeTable.expiresAt
    var reusable by ActivationCodeTable.reusable
    var redeemedAt by ActivationCodeTable.redeemedAt
    var redeemedByInstanceId by ActivationCodeTable.redeemedByInstanceId
    var issuedLicenseId by ActivationCodeTable.issuedLicenseId
    var redemptionCount by ActivationCodeTable.redemptionCount
    var revokedAt by ActivationCodeTable.revokedAt
    var revokedById by ActivationCodeTable.revokedById
    var createdById by ActivationCodeTable.createdById
    var createdAt by ActivationCodeTable.createdAt
    var updatedAt by ActivationCodeTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ActivationCodeTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
