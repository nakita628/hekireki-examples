package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class UserEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, UserEntity>(UserTable)

    var name by UserTable.name
    var email by UserTable.email
    var emailVerified by UserTable.emailVerified
    var image by UserTable.image
    var createdAt by UserTable.createdAt
    val accounts by AccountEntity referrersOn AccountTable.userId
    val sessions by SessionEntity referrersOn SessionTable.userId
    val documents by DocumentEntity optionalReferrersOn DocumentTable.ownerId
    val teams by UserTeamEntity referrersOn UserTeamTable.userId
    val userDatarooms by UserDataroomEntity referrersOn UserDataroomTable.userId
    val domains by DomainEntity optionalReferrersOn DomainTable.userId
    val chats by ChatEntity optionalReferrersOn ChatTable.userId
    var contactId by UserTable.contactId
    var plan by UserTable.plan
    var stripeId by UserTable.stripeId
    var subscriptionId by UserTable.subscriptionId
    var startsAt by UserTable.startsAt
    var endsAt by UserTable.endsAt
    val restrictedTokens by RestrictedTokenEntity referrersOn RestrictedTokenTable.userId
    val participatedConversations by ConversationParticipantEntity optionalReferrersOn ConversationParticipantTable.userId
    val messages by MessageEntity optionalReferrersOn MessageTable.userId
    val createdDataroomQuestions by DataroomQuestionEntity optionalReferrersOn DataroomQuestionTable.createdByUserId
    val publishedFaqItems by DataroomFaqItemEntity referrersOn DataroomFaqItemTable.publishedByUserId
    val createdAnnotations by DocumentAnnotationEntity referrersOn DocumentAnnotationTable.createdById
    val createdRedactionJobs by DocumentRedactionJobEntity referrersOn DocumentRedactionJobTable.createdById
    val installedIntegrations by InstalledIntegrationEntity optionalReferrersOn InstalledIntegrationTable.userId
    val links by LinkEntity optionalReferrersOn LinkTable.ownerId
    val frozenDatarooms by DataroomEntity optionalReferrersOn DataroomTable.frozenBy
    val createdTasks by TaskEntity optionalReferrersOn TaskTable.createdByUserId
    val taskActivities by TaskActivityEntity optionalReferrersOn TaskActivityTable.userId
    val notificationPreferences by NotificationPreferenceEntity referrersOn NotificationPreferenceTable.userId
    var referralTrack by UserTable.referralTrack
    var referralTrackSelectedAt by UserTable.referralTrackSelectedAt
    var referralProfile by UserTable.referralProfile
    val referralInvites by ReferralInviteEntity referrersOn ReferralInviteTable.userId
}
