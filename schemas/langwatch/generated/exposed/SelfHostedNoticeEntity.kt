package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SelfHostedNoticeEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SelfHostedNoticeEntity>(SelfHostedNoticeTable)

    var severity by SelfHostedNoticeTable.severity
    var title by SelfHostedNoticeTable.title
    var body by SelfHostedNoticeTable.body
    var url by SelfHostedNoticeTable.url
    var fixedIn by SelfHostedNoticeTable.fixedIn
    var status by SelfHostedNoticeTable.status
    var expiresAt by SelfHostedNoticeTable.expiresAt
    var publishedAt by SelfHostedNoticeTable.publishedAt
    var publishedBy by SelfHostedNoticeTable.publishedBy
    var createdAt by SelfHostedNoticeTable.createdAt
    var updatedAt by SelfHostedNoticeTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SelfHostedNoticeTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
