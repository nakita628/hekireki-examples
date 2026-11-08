defmodule WorkoutCool.User do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          first_name: String.t(),
          last_name: String.t(),
          name: String.t(),
          email: String.t(),
          email_verified: boolean(),
          image: String.t() | nil,
          locale: String.t() | nil,
          created_at: DateTime.t(),
          updated_at: DateTime.t(),
          role: atom() | nil,
          banned: boolean() | nil,
          ban_reason: String.t() | nil,
          ban_expires: DateTime.t() | nil,
          is_premium: boolean() | nil,
          onboarding_preferences: map() | nil,
          sessions: [WorkoutCool.Session.t()],
          accounts: [WorkoutCool.Account.t()],
          feedbacks: [WorkoutCool.Feedbacks.t()],
          workout_session: [WorkoutCool.WorkoutSession.t()],
          favorite_exercises: [WorkoutCool.UserFavoriteExercise.t()],
          subscriptions: [WorkoutCool.Subscription.t()],
          licenses: [WorkoutCool.License.t()],
          program_enrollments: [WorkoutCool.UserProgramEnrollment.t()]
        }

  schema "user" do
    field(:id, :string, primary_key: true)
    field(:first_name, :string, default: "", source: :firstName)
    field(:last_name, :string, default: "", source: :lastName)
    field(:name, :string)
    field(:email, :string)
    field(:email_verified, :boolean, source: :emailVerified)
    field(:image, :string)
    field(:locale, :string, default: "fr")
    field(:created_at, WorkoutCool.PrismaDateTime, source: :createdAt)
    field(:updated_at, WorkoutCool.PrismaDateTime, source: :updatedAt)
    field(:role, Ecto.Enum, values: [:admin, :user], default: :user)
    field(:banned, :boolean, default: false)
    field(:ban_reason, :string, source: :banReason)
    field(:ban_expires, WorkoutCool.PrismaDateTime, source: :banExpires)
    field(:is_premium, :boolean, default: false, source: :isPremium)
    field(:onboarding_preferences, :map, source: :onboardingPreferences)
    has_many(:sessions, WorkoutCool.Session, foreign_key: :user_id, references: :id)
    has_many(:accounts, WorkoutCool.Account, foreign_key: :user_id, references: :id)
    has_many(:feedbacks, WorkoutCool.Feedbacks, foreign_key: :user_id, references: :id)
    has_many(:workout_session, WorkoutCool.WorkoutSession, foreign_key: :user_id, references: :id)
    has_many(:favorite_exercises, WorkoutCool.UserFavoriteExercise, foreign_key: :user_id, references: :id)
    has_many(:subscriptions, WorkoutCool.Subscription, foreign_key: :user_id, references: :id)
    has_many(:licenses, WorkoutCool.License, foreign_key: :user_id, references: :id)
    has_many(:program_enrollments, WorkoutCool.UserProgramEnrollment, foreign_key: :user_id, references: :id)
  end
end