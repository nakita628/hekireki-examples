package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class UserEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, UserEntity>(UserTable)

    var email by UserTable.email
    var firstName by UserTable.firstName
    var lastName by UserTable.lastName
    var name by UserTable.name
    var emailVerified by UserTable.emailVerified
    var image by UserTable.image
    var twoFactorEnabled by UserTable.twoFactorEnabled
    val twoFactor by TwoFactorEntity optionalBackReferencedOn TwoFactorTable.userId
    val createdOrganizations by OrganizationEntity optionalReferrersOn OrganizationTable.createdByUserId
    val subscriptions by OrganizationEntity optionalReferrersOn OrganizationTable.subscriptionCreatedByUserId
    val membership by MemberEntity optionalReferrersOn MemberTable.userId
    val sentInvites by MemberEntity optionalReferrersOn MemberTable.invitedById
    var createdAt by UserTable.createdAt
    var updatedAt by UserTable.updatedAt
    var deletedAt by UserTable.deletedAt
    val sessions by SessionEntity referrersOn SessionTable.userId
    val accounts by AccountEntity referrersOn AccountTable.userId
    val invites by InviteEntity referrersOn InviteTable.createdById
    val conversations by ConversationEntity referrersOn ConversationTable.userId
    val totp by UserTotpEntity optionalBackReferencedOn UserTotpTable.userId
    val twoFactorChallenges by TwoFactorChallengeEntity referrersOn TwoFactorChallengeTable.userId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(UserTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
