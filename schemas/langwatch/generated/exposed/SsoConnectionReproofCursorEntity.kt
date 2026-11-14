package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class SsoConnectionReproofCursorEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SsoConnectionReproofCursorEntity>(SsoConnectionReproofCursorTable)

    var lastReproofAt by SsoConnectionReproofCursorTable.lastReproofAt
}
