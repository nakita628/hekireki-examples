package postiz.models

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
    var password by UserTable.password
    var providerName by UserTable.providerName
    var name by UserTable.name
    var lastName by UserTable.lastName
    var isSuperAdmin by UserTable.isSuperAdmin
    var bio by UserTable.bio
    var audience by UserTable.audience
    var pictureId by UserTable.pictureId
    var providerId by UserTable.providerId
    var timezone by UserTable.timezone
    var timezoneName by UserTable.timezoneName
    var createdAt by UserTable.createdAt
    var updatedAt by UserTable.updatedAt
    var lastReadNotifications by UserTable.lastReadNotifications
    var inviteId by UserTable.inviteId
    var activated by UserTable.activated
    var account by UserTable.account
    var connectedAccount by UserTable.connectedAccount
    var lastOnline by UserTable.lastOnline
    var ip by UserTable.ip
    var agent by UserTable.agent
    var deletedAt by UserTable.deletedAt
    val comments by CommentsEntity optionalReferrersOn CommentsTable.userId
    val items by ItemUserEntity referrersOn ItemUserTable.userId
    val groupBuyer by MessagesGroupEntity referrersOn MessagesGroupTable.buyerId
    val groupSeller by MessagesGroupEntity referrersOn MessagesGroupTable.sellerId
    val orderBuyer by OrdersEntity referrersOn OrdersTable.buyerId
    val orderSeller by OrdersEntity referrersOn OrdersTable.sellerId
    val payoutProblems by PayoutProblemsEntity referrersOn PayoutProblemsTable.userId
    val agencies by SocialMediaAgencyEntity optionalBackReferencedOn SocialMediaAgencyTable.userId
    var picture by MediaEntity optionalReferencedOn UserTable.pictureId
    val organizations by UserOrganizationEntity referrersOn UserOrganizationTable.userId
    var sendSuccessEmails by UserTable.sendSuccessEmails
    var sendFailureEmails by UserTable.sendFailureEmails
    var sendStreakEmails by UserTable.sendStreakEmails
    val oauthAuthorizations by OAuthAuthorizationEntity referrersOn OAuthAuthorizationTable.userId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(UserTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
