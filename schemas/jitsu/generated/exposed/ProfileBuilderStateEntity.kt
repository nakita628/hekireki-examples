package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class ProfileBuilderStateEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<ProfileBuilderStateEntity>(ProfileBuilderStateTable)

    val profileBuilderId by ProfileBuilderStateTable.profileBuilderId
    val profileBuilderVersion by ProfileBuilderStateTable.profileBuilderVersion
    val instanceIndex by ProfileBuilderStateTable.instanceIndex
    val totalInstances by ProfileBuilderStateTable.totalInstances
    var startedAt by ProfileBuilderStateTable.startedAt
    var updatedAt by ProfileBuilderStateTable.updatedAt
    var lastTimestamp by ProfileBuilderStateTable.lastTimestamp
    var processedUsers by ProfileBuilderStateTable.processedUsers
    var totalUsers by ProfileBuilderStateTable.totalUsers
    var errorUsers by ProfileBuilderStateTable.errorUsers
    var speed by ProfileBuilderStateTable.speed
}
