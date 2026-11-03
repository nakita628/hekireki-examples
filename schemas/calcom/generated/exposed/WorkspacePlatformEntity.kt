package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class WorkspacePlatformEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, WorkspacePlatformEntity>(WorkspacePlatformTable)

    var slug by WorkspacePlatformTable.slug
    var name by WorkspacePlatformTable.name
    var description by WorkspacePlatformTable.description
    var defaultServiceAccountKey by WorkspacePlatformTable.defaultServiceAccountKey
    var createdAt by WorkspacePlatformTable.createdAt
    var updatedAt by WorkspacePlatformTable.updatedAt
    var enabled by WorkspacePlatformTable.enabled
    val delegationCredentials by DelegationCredentialEntity referrersOn DelegationCredentialTable.workspacePlatformId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WorkspacePlatformTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
