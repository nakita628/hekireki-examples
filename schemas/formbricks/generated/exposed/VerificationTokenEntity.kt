package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class VerificationTokenEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<VerificationTokenEntity>(VerificationTokenTable)

    val identifier by VerificationTokenTable.identifier
    val token by VerificationTokenTable.token
    var expires by VerificationTokenTable.expires
}
