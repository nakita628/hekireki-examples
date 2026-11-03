package hoppscotch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class UserEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, UserEntity>(UserTable)

    var displayName by UserTable.displayName
    var email by UserTable.email
    var photoURL by UserTable.photoURL
    var isAdmin by UserTable.isAdmin
    var refreshToken by UserTable.refreshToken
    var currentRESTSession by UserTable.currentRESTSession
    var currentGQLSession by UserTable.currentGQLSession
    var createdOn by UserTable.createdOn
    var lastLoggedOn by UserTable.lastLoggedOn
    var lastActiveOn by UserTable.lastActiveOn
    val providerAccounts by AccountEntity referrersOn AccountTable.userId
    val invitedUsers by InvitedUsersEntity referrersOn InvitedUsersTable.adminUid
    val mockServers by MockServerEntity optionalReferrersOn MockServerTable.creatorUid
    val personalAccessTokens by PersonalAccessTokenEntity referrersOn PersonalAccessTokenTable.userUid
    val shortcodes by ShortcodeEntity optionalReferrersOn ShortcodeTable.creatorUid
    val userCollections by UserCollectionEntity referrersOn UserCollectionTable.userUid
    val environments by UserEnvironmentEntity referrersOn UserEnvironmentTable.userUid
    val history by UserHistoryEntity referrersOn UserHistoryTable.userUid
    val userRequests by UserRequestEntity referrersOn UserRequestTable.userUid
    val settings by UserSettingsEntity optionalBackReferencedOn UserSettingsTable.userUid
    val verificationTokens by VerificationTokenEntity referrersOn VerificationTokenTable.userUid
}
