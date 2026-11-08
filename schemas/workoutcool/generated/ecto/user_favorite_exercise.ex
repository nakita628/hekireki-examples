defmodule WorkoutCool.UserFavoriteExercise do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: WorkoutCool.PrismaDateTime, autogenerate: {WorkoutCool.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          updated_at: DateTime.t(),
          user: WorkoutCool.User.t() | nil,
          exercise: WorkoutCool.Exercise.t() | nil
        }

  schema "user_favorite_exercises" do
    field(:id, :string, primary_key: true)
    field(:user_id, :string, source: :userId)
    field(:exercise_id, :string, source: :exerciseId)
    belongs_to(:user, WorkoutCool.User, foreign_key: :user_id, define_field: false, type: :string)
    belongs_to(:exercise, WorkoutCool.Exercise, foreign_key: :exercise_id, define_field: false, type: :string)
    timestamps(inserted_at: false, updated_at_source: :updatedAt)
  end
end