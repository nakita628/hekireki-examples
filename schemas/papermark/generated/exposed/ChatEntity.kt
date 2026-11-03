package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ChatEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ChatEntity>(ChatTable)

    var title by ChatTable.title
    var teamId by ChatTable.teamId
    var team by TeamEntity referencedOn ChatTable.teamId
    var documentId by ChatTable.documentId
    var document by DocumentEntity optionalReferencedOn ChatTable.documentId
    var dataroomId by ChatTable.dataroomId
    var dataroom by DataroomEntity optionalReferencedOn ChatTable.dataroomId
    var linkId by ChatTable.linkId
    var link by LinkEntity optionalReferencedOn ChatTable.linkId
    var viewId by ChatTable.viewId
    var view by ViewEntity optionalReferencedOn ChatTable.viewId
    var userId by ChatTable.userId
    var user by UserEntity optionalReferencedOn ChatTable.userId
    var viewerId by ChatTable.viewerId
    var viewer by ViewerEntity optionalReferencedOn ChatTable.viewerId
    var vectorStoreId by ChatTable.vectorStoreId
    val messages by ChatMessageEntity referrersOn ChatMessageTable.chatId
    var createdAt by ChatTable.createdAt
    var updatedAt by ChatTable.updatedAt
    var lastMessageAt by ChatTable.lastMessageAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ChatTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
