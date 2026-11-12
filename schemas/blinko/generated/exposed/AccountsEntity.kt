package blinko.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AccountsEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, AccountsEntity>(AccountsTable)

    var name by AccountsTable.name
    var nickname by AccountsTable.nickname
    var password by AccountsTable.password
    var image by AccountsTable.image
    var apiToken by AccountsTable.apiToken
    var description by AccountsTable.description
    var note by AccountsTable.note
    var role by AccountsTable.role
    var loginType by AccountsTable.loginType
    var linkAccountId by AccountsTable.linkAccountId
    var createdAt by AccountsTable.createdAt
    var updatedAt by AccountsTable.updatedAt
    val notes by NotesEntity optionalReferrersOn NotesTable.accountId
    val configs by ConfigEntity optionalReferrersOn ConfigTable.userId
    val tags by TagEntity optionalReferrersOn TagTable.accountId
    val comments by CommentsEntity optionalReferrersOn CommentsTable.accountId
    val attachments by AttachmentsEntity optionalReferrersOn AttachmentsTable.accountId
    val follows by FollowsEntity referrersOn FollowsTable.accountId
    val notifications by NotificationsEntity referrersOn NotificationsTable.accountId
    val conversations by ConversationEntity referrersOn ConversationTable.accountId
    val sharedNotes by NoteInternalShareEntity referrersOn NoteInternalShareTable.accountId
    val aiScheduledTasks by AiScheduledTaskEntity referrersOn AiScheduledTaskTable.accountId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AccountsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
