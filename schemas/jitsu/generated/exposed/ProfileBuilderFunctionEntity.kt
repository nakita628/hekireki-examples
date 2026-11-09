package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ProfileBuilderFunctionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ProfileBuilderFunctionEntity>(ProfileBuilderFunctionTable)

    var createdAt by ProfileBuilderFunctionTable.createdAt
    var updatedAt by ProfileBuilderFunctionTable.updatedAt
    var profileBuilderId by ProfileBuilderFunctionTable.profileBuilderId
    var profileBuilder by ProfileBuilderEntity referencedOn ProfileBuilderFunctionTable.profileBuilderId
    var functionId by ProfileBuilderFunctionTable.functionId
    var function by ConfigurationObjectEntity referencedOn ProfileBuilderFunctionTable.functionId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ProfileBuilderFunctionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
