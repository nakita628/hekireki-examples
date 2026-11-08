defmodule WorkoutCool.WorkoutSessionExercise do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          order: integer(),
          workout_session: WorkoutCool.WorkoutSession.t() | nil,
          exercise: WorkoutCool.Exercise.t() | nil,
          sets: [WorkoutCool.WorkoutSet.t()]
        }

  schema "workout_session_exercises" do
    field(:id, :string, primary_key: true)
    field(:order, :integer)
    field(:workout_session_id, :string, source: :workoutSessionId)
    field(:exercise_id, :string, source: :exerciseId)
    belongs_to(:workout_session, WorkoutCool.WorkoutSession, foreign_key: :workout_session_id, define_field: false, type: :string)
    belongs_to(:exercise, WorkoutCool.Exercise, foreign_key: :exercise_id, define_field: false, type: :string)
    has_many(:sets, WorkoutCool.WorkoutSet, foreign_key: :workout_session_exercise_id, references: :id)
  end
end