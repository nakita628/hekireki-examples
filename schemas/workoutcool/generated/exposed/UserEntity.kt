package workoutcool.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class UserEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, UserEntity>(UserTable)

    var firstName by UserTable.firstName
    var lastName by UserTable.lastName
    var name by UserTable.name
    var email by UserTable.email
    var emailVerified by UserTable.emailVerified
    var image by UserTable.image
    var locale by UserTable.locale
    var createdAt by UserTable.createdAt
    var updatedAt by UserTable.updatedAt
    val sessions by SessionEntity referrersOn SessionTable.userId
    val accounts by AccountEntity referrersOn AccountTable.userId
    val feedbacks by FeedbacksEntity optionalReferrersOn FeedbacksTable.userId
    var role by UserTable.role
    var banned by UserTable.banned
    var banReason by UserTable.banReason
    var banExpires by UserTable.banExpires
    val workoutSession by WorkoutSessionEntity referrersOn WorkoutSessionTable.userId
    val favoriteExercises by UserFavoriteExerciseEntity referrersOn UserFavoriteExerciseTable.userId
    val subscriptions by SubscriptionEntity referrersOn SubscriptionTable.userId
    val licenses by LicenseEntity optionalReferrersOn LicenseTable.userId
    var isPremium by UserTable.isPremium
    var onboardingPreferences by UserTable.onboardingPreferences
    val programEnrollments by UserProgramEnrollmentEntity referrersOn UserProgramEnrollmentTable.userId
}
