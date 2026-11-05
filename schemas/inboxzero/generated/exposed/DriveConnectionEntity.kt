package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DriveConnectionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DriveConnectionEntity>(DriveConnectionTable)

    var createdAt by DriveConnectionTable.createdAt
    var updatedAt by DriveConnectionTable.updatedAt
    var provider by DriveConnectionTable.provider
    var email by DriveConnectionTable.email
    var accessToken by DriveConnectionTable.accessToken
    var refreshToken by DriveConnectionTable.refreshToken
    var expiresAt by DriveConnectionTable.expiresAt
    var isConnected by DriveConnectionTable.isConnected
    var emailAccountId by DriveConnectionTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn DriveConnectionTable.emailAccountId
    val documentFilings by DocumentFilingEntity referrersOn DocumentFilingTable.driveConnectionId
    val filingFolders by FilingFolderEntity referrersOn FilingFolderTable.driveConnectionId
    val attachmentSources by AttachmentSourceEntity referrersOn AttachmentSourceTable.driveConnectionId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DriveConnectionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
