package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ProfileBuilderState2Entity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ProfileBuilderState2Entity>(ProfileBuilderState2Table)

    var updatedAt by ProfileBuilderState2Table.updatedAt
    var fullRebuildInfo by ProfileBuilderState2Table.fullRebuildInfo
    var queuesInfo by ProfileBuilderState2Table.queuesInfo
    var metrics by ProfileBuilderState2Table.metrics
}
