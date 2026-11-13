package superproductivity.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class UserEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, UserEntity>(UserTable)

    var email by UserTable.email
    var passwordHash by UserTable.passwordHash
    var isVerified by UserTable.isVerified
    var verificationToken by UserTable.verificationToken
    var verificationTokenExpiresAt by UserTable.verificationTokenExpiresAt
    var verificationResendCount by UserTable.verificationResendCount
    var resetPasswordToken by UserTable.resetPasswordToken
    var resetPasswordTokenExpiresAt by UserTable.resetPasswordTokenExpiresAt
    var passkeyRecoveryToken by UserTable.passkeyRecoveryToken
    var passkeyRecoveryTokenExpiresAt by UserTable.passkeyRecoveryTokenExpiresAt
    var loginToken by UserTable.loginToken
    var loginTokenExpiresAt by UserTable.loginTokenExpiresAt
    var failedLoginAttempts by UserTable.failedLoginAttempts
    var lockedUntil by UserTable.lockedUntil
    var tokenVersion by UserTable.tokenVersion
    var termsAcceptedAt by UserTable.termsAcceptedAt
    var createdAt by UserTable.createdAt
    var storageQuotaBytes by UserTable.storageQuotaBytes
    var storageUsedBytes by UserTable.storageUsedBytes
    val operations by OperationEntity referrersOn OperationTable.userId
    val syncState by UserSyncStateEntity optionalBackReferencedOn UserSyncStateTable.id
    val devices by SyncDeviceEntity referrersOn SyncDeviceTable.userId
    val passkeys by PasskeyEntity referrersOn PasskeyTable.userId
    val pendingPasskeyRegistrations by PendingPasskeyRegistrationEntity referrersOn PendingPasskeyRegistrationTable.userId
    val captureKey by CaptureKeyEntity optionalBackReferencedOn CaptureKeyTable.id
    val captureGrants by CaptureGrantEntity referrersOn CaptureGrantTable.userId
    val captureItems by CaptureItemEntity referrersOn CaptureItemTable.userId
    val captureConnectCode by CaptureConnectCodeEntity optionalBackReferencedOn CaptureConnectCodeTable.id
    val captureAuthorizations by CaptureAuthEntity optionalReferrersOn CaptureAuthTable.userId
    val connectRequests by ConnectRequestEntity optionalReferrersOn ConnectRequestTable.userId
}
