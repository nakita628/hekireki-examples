package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class OptionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OptionEntity>(OptionTable)

    var startTime by OptionTable.startTime
    var duration by OptionTable.duration
    var pollId by OptionTable.pollId
    var createdAt by OptionTable.createdAt
    val votes by VoteEntity referrersOn VoteTable.optionId
    var poll by PollEntity referencedOn OptionTable.pollId
}
