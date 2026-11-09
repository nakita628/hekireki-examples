package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class MailSplitEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MailSplitEntity>(MailSplitTable)

    var createdAt by MailSplitTable.createdAt
    var updatedAt by MailSplitTable.updatedAt
    var name by MailSplitTable.name
    var order by MailSplitTable.order
    var matchAll by MailSplitTable.matchAll
    var bundleInInbox by MailSplitTable.bundleInInbox
    var hideFromAll by MailSplitTable.hideFromAll
    var emailAccountId by MailSplitTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn MailSplitTable.emailAccountId
    val filters by MailSplitFilterEntity referrersOn MailSplitFilterTable.mailSplitId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(MailSplitTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
