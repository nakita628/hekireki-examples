package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AppEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AppEntity>(AppTable)

    var dirName by AppTable.dirName
    var keys by AppTable.keys
    var categories by AppTable.categories
    var createdAt by AppTable.createdAt
    var updatedAt by AppTable.updatedAt
    val credentials by CredentialEntity optionalReferrersOn CredentialTable.appId
    val payments by PaymentEntity optionalReferrersOn PaymentTable.appId
    val webhook by WebhookEntity optionalReferrersOn WebhookTable.appId
    val apiKey by ApiKeyEntity optionalReferrersOn ApiKeyTable.appId
    var enabled by AppTable.enabled

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AppTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
