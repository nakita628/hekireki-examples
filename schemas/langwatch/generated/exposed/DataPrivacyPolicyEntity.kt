package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DataPrivacyPolicyEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DataPrivacyPolicyEntity>(DataPrivacyPolicyTable)

    var organizationId by DataPrivacyPolicyTable.organizationId
    var scopeType by DataPrivacyPolicyTable.scopeType
    var scopeId by DataPrivacyPolicyTable.scopeId
    var personalOnly by DataPrivacyPolicyTable.personalOnly
    var config by DataPrivacyPolicyTable.config
    var createdAt by DataPrivacyPolicyTable.createdAt
    var updatedAt by DataPrivacyPolicyTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DataPrivacyPolicyTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
